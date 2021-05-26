using EsignBackend.Models;
using EsignBackend.Models.DTOs;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EsignBackend.Services.SettingsService.SmartObject
{
    public class SmartObjectService : ISmartObjectService
    {
        private readonly AppDbContext _context;
        private readonly ILogger _logger;
        private static object _locker = new object();

        public SmartObjectService(AppDbContext context, ILogger logger)
        {
            _context = context;
            _logger = logger;
        }
        private int GenerateId()
        {
            _logger.Debug("GenerateId");
            lock (_locker)
            {
                int maxId = _context.Smartobjects.OrderByDescending(item => item.Id).Take(1).ToList()[0].Id;
                return maxId + 1;
            }

        }
        private bool IsNameAlreadyInUse(string title)
        {
            _logger.Debug("IsNameAlreadyInUse");
            return _context.Smartobjects.Where(item => item.Title.Equals(title)).Count() != 0;
        }
        public async Task<ServiceResponse<List<SmartobjectDTO>>> GetSmartObjects(int skip, int take)
        {
            _logger.Debug("GetSmartObjects");
            var serviceResponse = new ServiceResponse<List<SmartobjectDTO>>();
            var outputList = new List<SmartobjectDTO>();
            var data = _context.Smartobjects.Skip(skip).Take(take).ToList();
            foreach (var so in data)
            {
                outputList.Add(new SmartobjectDTO(so));
            }
            serviceResponse.Amount = _context.Smartobjects.Count();
            serviceResponse.Data = outputList;
            return serviceResponse;
        }
        public async Task<ServiceResponse<int>> UpdateSmartObject(Smartobject updatedSmartObject)
        {
            _logger.Debug("UpdateSmartObject");
            var serviceResponse = new ServiceResponse<int>();
            var updatedSmartObjectInDb = _context.Smartobjects.Update(updatedSmartObject);
            try
            {
                await _context.SaveChangesAsync();
                serviceResponse.Success = true;
                serviceResponse.Data = updatedSmartObjectInDb.Entity.Id;
                serviceResponse.Message = "SmartObject updated successfully.";
            }
            catch (Exception exception)
            {
                _logger.Error("exception detected while trying to UpdateSmartObject: " + exception);
                serviceResponse.Success = false;
                serviceResponse.Message = $"Updated failed. {exception}";
                serviceResponse.Data = -1;
            }
            return serviceResponse;
        }
        public async Task<ServiceResponse<int>> AddNewSmartObject(Smartobject smartobject)
        {
            _logger.Debug("AddNewSmartObject");
            var serviceRespone = new ServiceResponse<int>();
            var smartObjectNameIsAlreadyTaken = IsNameAlreadyInUse(smartobject.Title);
            if (smartObjectNameIsAlreadyTaken)
            {
                serviceRespone.Success = false;
                serviceRespone.Message = "SmartObject name is already taken";
                serviceRespone.Data = -1;
                return serviceRespone;
            }
            lock (_locker)
            {
                smartobject.Id = GenerateId();
                var newcertificatesStatusInDb = _context.Smartobjects.Add(smartobject);
                try
                {
                    _context.SaveChanges();
                    serviceRespone.Amount = _context.Smartobjects.Count();
                    serviceRespone.Data = newcertificatesStatusInDb.Entity.Id;
                    return serviceRespone;
                }
                catch (Exception exception)
                {
                    _logger.Debug("exception detected while trying to AddNewSmartObject: " + exception);
                    serviceRespone.Success = false;
                    serviceRespone.Message = $"Adding new smartObject failed. {exception}";
                    serviceRespone.Data = -1;
                    return serviceRespone;
                }
            }
        }
        public async Task<ServiceResponse<List<SmartobjectDTO>>> GetAllSmartObjects()
        {
            _logger.Debug("GetAllSmartObjects");
            var serviceResponse = new ServiceResponse<List<SmartobjectDTO>>();
            var outputList = new List<SmartobjectDTO>();
            var data = _context.Smartobjects.ToList();
            foreach (var so in data)
            {
                outputList.Add(new SmartobjectDTO(so));
            }
            serviceResponse.Data = outputList;
            serviceResponse.Amount = serviceResponse.Data.Count();
            return serviceResponse;
        }
    }
}
