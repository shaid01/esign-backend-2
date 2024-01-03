using EsignBackend.Models;
using EsignBackend.Models.DTOs;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EsignBackend.Services.SettingsService.Expirationtype
{
    public class ExpirationTypeService : IExpirationTypeService
    {
        private readonly AppDbContext _context;
        private readonly ILogger _logger;
        private static object _locker = new object();

        public ExpirationTypeService(AppDbContext context, ILogger logger)
        {
            _context = context;
            _logger = logger;
        }
        private int GenerateId()
        {
            _logger.Debug("GenerateId");
            lock (_locker)
            {
                int maxId = _context.Expirationtypes.OrderByDescending(item => item.Id).Take(1).ToList()[0].Id;
                return maxId + 1;
            }

        }
        private bool isTheNameAlreadyInUse(string title)
        {
            _logger.Debug("isTheNameAlreadyInUse");
            return _context.Expirationtypes.Where(item => item.Title.Equals(title)).Count() != 0;
        }
        /*        public async Task<ServiceResponse<int>> GetAmountOfExpirationTypes()
                {
                    _logger.Debug("GetAmountOfExpirationTypes");
                    var serviceResponse = new ServiceResponse<int>();
                    serviceResponse.Data = _context.Expirationtypes.Count();
                    return serviceResponse;
                }*/
        public async Task<ServiceResponse<List<ExpirationtypeDTO>>> GetExpirationTypes(int skip, int take)
        {
            _logger.Debug("GetExpirationTypes");
            var serviceResponse = new ServiceResponse<List<ExpirationtypeDTO>>();
            var outputList = new List<ExpirationtypeDTO>();
            var data = _context.Expirationtypes.Skip(skip).Take(take).ToList();
            foreach (var et in data)
            {
                outputList.Add(new ExpirationtypeDTO(et));
            }
            serviceResponse.Data = outputList;
            serviceResponse.Amount = _context.Expirationtypes.Count();
            return serviceResponse;
        }
        public async Task<ServiceResponse<int>> UpdateExpirationType(Models.ExpirationType updatedExpirationType)
        {
            _logger.Debug("UpdateExpirationType");
            var serviceResponse = new ServiceResponse<int>();
            var updatedExpirationTypeInDb = _context.Expirationtypes.Update(updatedExpirationType);
            try
            {
                await _context.SaveChangesAsync();
                serviceResponse.Success = true;
                serviceResponse.Data = updatedExpirationTypeInDb.Entity.Id;
                serviceResponse.Message = "ExpirationType updated successfully.";
            }
            catch (Exception exception)
            {
                _logger.Error("exception detected while trying to UpdateExpirationType: " + exception);
                serviceResponse.Success = false;
                serviceResponse.Message = $"Updated failed. {exception}";
                serviceResponse.Data = -1;
            }
            return serviceResponse;
        }
        public async Task<ServiceResponse<int>> AddNewExpirationType(Models.ExpirationType expirationType)
        {
            _logger.Debug("AddNewExpirationType");
            var serviceRespone = new ServiceResponse<int>();
            var nameIsAlreadyTaken = isTheNameAlreadyInUse(expirationType.Title);
            if (nameIsAlreadyTaken)
            {
                serviceRespone.Success = false;
                serviceRespone.Message = "ExpirationType name is already taken";
                serviceRespone.Data = -1;
                return serviceRespone;
            }
            lock (_locker)
            {
                /*expirationType.Id = GenerateId();*/
                expirationType.Id = 0;
                var newExpirationTypeInDb = _context.Expirationtypes.Add(expirationType);
                try
                {
                    _context.SaveChanges();
                    serviceRespone.Amount = _context.Expirationtypes.Count();
                    serviceRespone.Data = newExpirationTypeInDb.Entity.Id;
                    return serviceRespone;
                }
                catch (Exception exception)
                {
                    _logger.Debug("exception detected while trying to AddNewExpirationType: " + exception);
                    serviceRespone.Success = false;
                    serviceRespone.Message = $"Adding new expirationType failed. {exception}";
                    serviceRespone.Data = -1;
                    return serviceRespone;
                }
            }
        }
        public async Task<ServiceResponse<List<ExpirationtypeDTO>>> GetAllExpirationTypes()
        {
            _logger.Debug("GetAllExpirationTypes");
            var serviceResponse = new ServiceResponse<List<ExpirationtypeDTO>>();
            var outputList = new List<ExpirationtypeDTO>();
            var data = _context.Expirationtypes.ToList();
            foreach (var et in data)
            {
                outputList.Add(new ExpirationtypeDTO(et));
            }
            serviceResponse.Amount = _context.Expirationtypes.Count();
            serviceResponse.Data = outputList;
            return serviceResponse;
        }
    }
}