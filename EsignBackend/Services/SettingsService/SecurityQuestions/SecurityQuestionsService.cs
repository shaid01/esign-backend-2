using EsignBackend.Models;
using EsignBackend.Models.DTOs;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EsignBackend.Services.SettingsService.SecurityQuestions
{
    public class SecurityQuestionsService : ISecurityQuestionsService
    {
        private readonly AppDbContext _context;
        private readonly ILogger _logger;
        private static object _locker = new object();

        public SecurityQuestionsService(AppDbContext context, ILogger logger)
        {
            _context = context;
            _logger = logger;
        }
        private int GenerateId()
        {
            _logger.Debug("GenerateId");
            lock (_locker)
            {
                int maxId = _context.Securityquestions.OrderByDescending(item => item.Id).Take(1).ToList()[0].Id;
                return maxId + 1;
            }
        }
        private bool isNameAlreadyInUse(string title)
        {
            return _context.Securityquestions.Where(item => item.Title.Equals(title)).Count() != 0;
        }

        public async Task<ServiceResponse<int>> AddNewSecurityQuestion(Securityquestion securityQuestion)
        {
            _logger.Debug("AddNewSecurityQuestion");

            var serviceResponse = new ServiceResponse<int>();

            var nameIsAlreadyTaken = isNameAlreadyInUse(securityQuestion.Title);

            if (nameIsAlreadyTaken)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "SecurityQuestion name is already taken";
                serviceResponse.Data = -1;
                return serviceResponse;
            }

            lock (_locker)
            {
                //securityQuestion.Id = GenerateId();
                securityQuestion.Id = 0;
                var newSecurityQuestionInDb = _context.Securityquestions.Add(securityQuestion);
                try
                {
                    _context.SaveChanges();
                    serviceResponse.Amount = _context.Securityquestions.Count();
                    serviceResponse.Data = newSecurityQuestionInDb.Entity.Id;
                    serviceResponse.Success = true;
                    return serviceResponse;
                }
                catch (Exception exception)
                {
                    _logger.Error("exception detected while trying to AddNewSecurityQuestion: " + exception);
                    serviceResponse.Success = false;
                    serviceResponse.Message = $"Adding new expirationType failed. {exception}";
                    serviceResponse.Data = -1;
                    return serviceResponse;
                }
            }
        }

        public ServiceResponse<List<SecurityquestionDTO>> GetSecurityQuestions(int skip, int take)
        {
            _logger.Debug("GetSecurityQuestions");

            //var serviceResponse = new ServiceResponse<List<SecurityquestionDTO>>();
            //var outputList = new List<SecurityquestionDTO>();
            //var data = _context.Securityquestions.Skip(skip).Take(take).ToList();
            //foreach (var sq in data)
            //{
            //    outputList.Add(new SecurityquestionDTO(sq));
            //}
            //serviceResponse.Data = outputList;
            //serviceResponse.Amount = _context.Securityquestions.Count();
            //return serviceResponse;

            var serviceResponse = new ServiceResponse<List<SecurityquestionDTO>>();
            serviceResponse.Amount = _context.Securityquestions.Count();

            List<Securityquestion> secQuestions = null;

            try
            {
                secQuestions = _context.Securityquestions
                    .Skip(skip)
                    .Take(take)
                    .ToList();
            }
            catch (Exception ex)
            {
                _logger.Error($"Error while processing DB query in GetSecurityQuestions. {ex.Message}");

                serviceResponse.Data = null;
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;

                return serviceResponse;
            }

            var outputList = new List<SecurityquestionDTO>();

            foreach (var secQuest in secQuestions)
            {
                outputList.Add(new SecurityquestionDTO(secQuest));
            }

            serviceResponse.Success = true;
            serviceResponse.Data = outputList;

            return serviceResponse;
        }

        public async Task<ServiceResponse<int>> UpdateSecurityQuestion(Securityquestion updatedSecurityQuestion)
        {
            _logger.Debug("UpdateSecurityQuestion");

            var serviceResponse = new ServiceResponse<int>();

            var nameIsAlreadyTaken = isNameAlreadyInUse(updatedSecurityQuestion.Title);

            if (nameIsAlreadyTaken)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "SecurityQuestion name is already taken";
                serviceResponse.Data = -1;
                return serviceResponse;
            }

            var updatedSecurityQuestionInDb = _context.Securityquestions.Update(updatedSecurityQuestion);

            try
            {
                await _context.SaveChangesAsync();
                serviceResponse.Data = updatedSecurityQuestionInDb.Entity.Id;
                serviceResponse.Message = "Securityquestion updated successfully.";
                serviceResponse.Success = true;
            }
            catch (Exception exception)
            {
                _logger.Error("exception detected while trying to UpdateSecurityQuestion: " + exception);
                serviceResponse.Success = false;
                serviceResponse.Message = $"Updated failed. {exception}";
                serviceResponse.Data = -1;
            }
            return serviceResponse;
        }

        public ServiceResponse<List<SecurityquestionDTO>> GetAllSecurityQuestions()
        {
            _logger.Debug("GetAllSecurityQuestions");
            var serviceResponse = new ServiceResponse<List<SecurityquestionDTO>>();
            var outputList = new List<SecurityquestionDTO>();
            var data = _context.Securityquestions.ToList();
            foreach (var sq in data)
            {
                outputList.Add(new SecurityquestionDTO(sq));
            }
            serviceResponse.Data = outputList;
            serviceResponse.Amount = serviceResponse.Data.Count();
            return serviceResponse;
        }
    }
}