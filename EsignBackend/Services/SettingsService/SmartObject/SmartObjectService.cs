using DocumentFormat.OpenXml.Drawing.Charts;
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

        private bool isNameAlreadyInUse(string title)
        {
            return _context.Smartobjects.Where(item => item.Title.Equals(title)).Count() != 0;
        }

        public ServiceResponse<List<SmartobjectDTO>> GetSmartObjects(int skip, int take)
        {
            _logger.Debug("GetSmartObjects");

            var serviceResponse = new ServiceResponse<List<SmartobjectDTO>>();
            serviceResponse.Amount = _context.Smartobjects.Where(x => !string.IsNullOrWhiteSpace(x.Title)).Count();

            List<Smartobject> smartObjects = null;

            try
            {
                smartObjects = _context.Smartobjects.Where(x => !string.IsNullOrWhiteSpace(x.Title))
                    .Skip(skip)
                    .Take(take)
                    .ToList();
            }
            catch (Exception ex)
            {
                _logger.Error($"Error while processing DB query in GetSmartObjects. {ex.Message}");

                serviceResponse.Data = null;
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;

                return serviceResponse;
            }

            var outputList = new List<SmartobjectDTO>();

            foreach (var so in smartObjects)
            {
                outputList.Add(new SmartobjectDTO(so));
            }

            serviceResponse.Success = true;
            serviceResponse.Data = outputList;

            return serviceResponse;
        }

        //public ServiceResponse<List<SmartobjectDTO>> GetAllSmartObjects()
        //{
        //    _logger.Debug("GetAllSmartObjects");

        //    var serviceResponse = new ServiceResponse<List<SmartobjectDTO>>();

        //    List<Smartobject> smartObjects = null;

        //    try
        //    {
        //        smartObjects = _context.Smartobjects.ToList();
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.Error($"Error while processing DB query in GetAllSmartObjects. {ex.Message}");
        //        var errorData = new ServiceResponse<List<SmartobjectDTO>>();
        //        errorData.Data = null;
        //        errorData.Success = false;
        //        errorData.Message = ex.Message;

        //        return errorData;
        //    }

        //    var outputList = new List<SmartobjectDTO>();

        //    foreach (var so in smartObjects)
        //    {
        //        outputList.Add(new SmartobjectDTO(so));
        //    }

        //    serviceResponse.Data = outputList;
        //    serviceResponse.Amount = serviceResponse.Data.Count();

        //    return serviceResponse;
        //}

        public ServiceResponse<List<SmartobjectDTO>> GetAllSmartObjects()
        {
            _logger.Debug("GetAllSmartObjects");

            var serviceResponse = new ServiceResponse<List<SmartobjectDTO>>();

            var outputList = new List<SmartobjectDTO>();

            var data = _context.Smartobjects.Where(x => !string.IsNullOrWhiteSpace(x.Title)).ToList();

            foreach (var so in data)
            {
                outputList.Add(new SmartobjectDTO(so));
            }

            serviceResponse.Data = outputList;
            serviceResponse.Amount = outputList.Count();//serviceResponse.Data.Count();
            return serviceResponse;
        }

        public async Task<ServiceResponse<int>> UpdateSmartObject(Smartobject updatedSmartObject)
        {
            _logger.Debug("UpdateSmartObject");

            var serviceResponse = new ServiceResponse<int>();

            var smartObjectNameIsAlreadyTaken = isNameAlreadyInUse(updatedSmartObject.Title);

            if (smartObjectNameIsAlreadyTaken)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "SmartObject name is already taken";
                serviceResponse.Data = -1;
                return serviceResponse;
            }

            var updatedSmartObjectInDb = _context.Smartobjects.Update(updatedSmartObject);

            try
            {
                await _context.SaveChangesAsync();
                serviceResponse.Data = updatedSmartObjectInDb.Entity.Id;
                serviceResponse.Message = "SmartObject updated successfully.";
                serviceResponse.Success = true;
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
            var serviceResponse = new ServiceResponse<int>();

            var smartObjectNameIsAlreadyTaken = isNameAlreadyInUse(smartobject.Title);

            if (smartObjectNameIsAlreadyTaken)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "SmartObject name is already taken";
                serviceResponse.Data = -1;
                return serviceResponse;
            }

            lock (_locker)
            {
                //smartobject.Id = GenerateId();
                smartobject.Id = 0;
                var newcertificatesStatusInDb = _context.Smartobjects.Add(smartobject);
                try
                {
                    _context.SaveChanges();
                    serviceResponse.Amount = _context.Smartobjects.Where(x => !string.IsNullOrWhiteSpace(x.Title)).Count();
                    serviceResponse.Data = newcertificatesStatusInDb.Entity.Id;
                    serviceResponse.Success = true;
                    return serviceResponse;
                }
                catch (Exception exception)
                {
                    _logger.Debug("exception detected while trying to AddNewSmartObject: " + exception);
                    serviceResponse.Success = false;
                    serviceResponse.Message = $"Adding new smartObject failed. {exception}";
                    serviceResponse.Data = -1;
                    return serviceResponse;
                }
            }
        }

    }
}
