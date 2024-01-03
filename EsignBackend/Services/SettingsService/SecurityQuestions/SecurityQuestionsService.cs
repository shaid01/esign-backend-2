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
            _logger.Debug("isNameAlreadyInUse");
            return _context.Securityquestions.Where(item => item.Title.Equals(title)).Count() != 0;
        }

        public async Task<ServiceResponse<int>> AddNewSecurityQuestion(SecurityGuestion securityQuestion)
        {
            _logger.Debug("AddNewSecurityQuestion");
            var serviceRespone = new ServiceResponse<int>();
            var nameIsAlreadyTaken = isNameAlreadyInUse(securityQuestion.Title);
            if (nameIsAlreadyTaken)
            {
                serviceRespone.Success = false;
                serviceRespone.Message = "SecurityQuestion name is already taken";
                serviceRespone.Data = -1;
                return serviceRespone;
            }
            lock (_locker)
            {
                //securityQuestion.Id = GenerateId();
                securityQuestion.Id = 0;
                var newSecurityQuestionInDb = _context.Securityquestions.Add(securityQuestion);
                try
                {
                    _context.SaveChanges();
                    serviceRespone.Amount = _context.Securityquestions.Count();
                    serviceRespone.Data = newSecurityQuestionInDb.Entity.Id;
                    return serviceRespone;
                }
                catch (Exception exception)
                {
                    _logger.Error("exception detected while trying to AddNewSecurityQuestion: " + exception);
                    serviceRespone.Success = false;
                    serviceRespone.Message = $"Adding new expirationType failed. {exception}";
                    serviceRespone.Data = -1;
                    return serviceRespone;
                }
            }
        }

        public async Task<ServiceResponse<List<SecurityquestionDTO>>> GetSecurityQuestions(int skip, int take)
        {
            _logger.Debug("GetSecurityQuestions");
            var serviceResponse = new ServiceResponse<List<SecurityquestionDTO>>();
            var outputList = new List<SecurityquestionDTO>();
            var data = _context.Securityquestions.Skip(skip).Take(take).ToList();
            foreach (var sq in data)
            {
                outputList.Add(new SecurityquestionDTO(sq));
            }
            serviceResponse.Data = outputList;
            serviceResponse.Amount = _context.Securityquestions.Count();
            return serviceResponse;
        }
        public async Task<ServiceResponse<int>> UpdateSecurityQuestion(SecurityGuestion updatedSecurityQuestion)
        {
            _logger.Debug("UpdateSecurityQuestion");
            var serviceResponse = new ServiceResponse<int>();
            var updatedSecurityQuestionInDb = _context.Securityquestions.Update(updatedSecurityQuestion);
            try
            {
                await _context.SaveChangesAsync();
                serviceResponse.Success = true;
                serviceResponse.Data = updatedSecurityQuestionInDb.Entity.Id;
                serviceResponse.Message = "Securityquestion updated successfully.";
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
        public async Task<ServiceResponse<List<SecurityquestionDTO>>> GetAllSecurityQuestions()
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