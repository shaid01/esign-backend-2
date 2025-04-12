using EsignBackend.Migrations;
using EsignBackend.Models;
using EsignBackend.Models.DTOs;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace EsignBackend.Services.SettingsService.ExpirationType
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

        private bool isNameAlreadyInUse(int id, string title)
        {
            if (id >= 0)
            {
                return _context.Expirationtypes.Where(x => x.Title.Equals(title) && x.Id != id).Count() != 0;
            }
            else
            {
                return _context.Expirationtypes.Where(x => x.Title.Equals(title)).Count() != 0;
            }
        }

        /*        public async Task<ServiceResponse<int>> GetAmountOfExpirationTypes()
                {
                    _logger.Debug("GetAmountOfExpirationTypes");
                    var serviceResponse = new ServiceResponse<int>();
                    serviceResponse.Data = _context.Expirationtypes.Count();
                    return serviceResponse;
                }*/

        public ServiceResponse<List<ExpirationtypeDTO>> GetExpirationTypes(int skip, int take)
        {
            _logger.Debug($"Get expiration types: skip - {skip}, take - {take}");

            //var serviceResponse = new ServiceResponse<List<ExpirationtypeDTO>>();
            //var outputList = new List<ExpirationtypeDTO>();
            //var data = _context.Expirationtypes.Skip(skip).Take(take).ToList();
            //foreach (var et in data)
            //{
            //    outputList.Add(new ExpirationtypeDTO(et));
            //}
            //serviceResponse.Data = outputList;
            //serviceResponse.Amount = _context.Expirationtypes.Count();
            //return serviceResponse;

            var serviceResponse = new ServiceResponse<List<ExpirationtypeDTO>>();
            serviceResponse.Amount = _context.Expirationtypes.Where(x => !string.IsNullOrWhiteSpace(x.Title)).Count();

            List<Expirationtype> expTypes = null;

            try
            {
                expTypes = _context.Expirationtypes.Where(x => !string.IsNullOrWhiteSpace(x.Title))
                    .Skip(skip)
                    .Take(take)
                    .ToList();
            }
            catch (Exception ex)
            {
                _logger.Error($"Get expiration types error: {ex}");

                serviceResponse.Data = null;
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;

                return serviceResponse;
            }

            var outputList = new List<ExpirationtypeDTO>();

            foreach (var expType in expTypes)
            {
                outputList.Add(new ExpirationtypeDTO(expType));
            }

            serviceResponse.Success = true;
            serviceResponse.Data = outputList;

            return serviceResponse;
        }

        public async Task<ServiceResponse<int>> UpdateExpirationType(Models.Expirationtype updatedExpirationType)
        {
            _logger.Information($"Update expiration type ID {updatedExpirationType.Id} to {updatedExpirationType.Title}");

            var serviceResponse = new ServiceResponse<int>();

            var nameIsAlreadyTaken = isNameAlreadyInUse(updatedExpirationType.Id, updatedExpirationType.Title);

            if (nameIsAlreadyTaken)
            {
                _logger.Warning($"Update expiration type ID {updatedExpirationType.Id} to {updatedExpirationType.Title}. Expiration type name is already taken.");

                serviceResponse.Success = false;
                serviceResponse.Message = "ExpirationType name is already taken";
                serviceResponse.Data = -1;
                return serviceResponse;
            }

            var updatedExpirationTypeInDb = _context.Expirationtypes.Update(updatedExpirationType);

            try
            {
                await _context.SaveChangesAsync();
                serviceResponse.Data = updatedExpirationTypeInDb.Entity.Id;
                serviceResponse.Message = "ExpirationType updated successfully.";
                serviceResponse.Success = true;
            }
            catch (Exception exception)
            {
                _logger.Error("Exception detected while trying to UpdateExpirationType: " + exception);


                serviceResponse.Success = false;
                serviceResponse.Message = $"Updated failed. {exception}";
                serviceResponse.Data = -1;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<int>> AddNewExpirationType(Models.Expirationtype expirationType)
        {
            _logger.Information($"Add new expiration type: {expirationType.Title}");

            var serviceResponse = new ServiceResponse<int>();

            var nameIsAlreadyTaken = isNameAlreadyInUse(-1, expirationType.Title);

            if (nameIsAlreadyTaken)
            {
                _logger.Warning($"Add new expiration type: {expirationType.Title}. Expiration type name is already taken");

                serviceResponse.Success = false;
                serviceResponse.Message = "ExpirationType name is already taken";
                serviceResponse.Data = -1;
                return serviceResponse;
            }

            lock (_locker)
            {
                /*expirationType.Id = GenerateId();*/
                expirationType.Id = 0;

                var newExpirationTypeInDb = _context.Expirationtypes.Add(expirationType);

                try
                {
                    _context.SaveChanges();
                    serviceResponse.Amount = _context.Expirationtypes.Where(x => !string.IsNullOrWhiteSpace(x.Title)).Count();
                    serviceResponse.Data = newExpirationTypeInDb.Entity.Id;
                    serviceResponse.Success = true;
                    return serviceResponse;
                }
                catch (Exception exception)
                {
                    _logger.Error($"Add new expiration type: {expirationType.Title} exception: {exception}");

                    serviceResponse.Success = false;
                    serviceResponse.Message = $"Adding new expirationType failed. {exception}";
                    serviceResponse.Data = -1;
                    return serviceResponse;
                }
            }
        }

        public ServiceResponse<List<ExpirationtypeDTO>> GetAllExpirationTypes()
        {
            _logger.Debug("Get all expiration types");

            var serviceResponse = new ServiceResponse<List<ExpirationtypeDTO>>();

            var outputList = new List<ExpirationtypeDTO>();

            var data = _context.Expirationtypes.Where(x => !string.IsNullOrWhiteSpace(x.Title)).ToList();

            foreach (var et in data)
            {
                outputList.Add(new ExpirationtypeDTO(et));
            }

            serviceResponse.Amount = outputList.Count();//_context.Expirationtypes.Where(x => !string.IsNullOrWhiteSpace(x.Title)).Count();
            serviceResponse.Data = outputList;

            return serviceResponse;
        }
    }
}