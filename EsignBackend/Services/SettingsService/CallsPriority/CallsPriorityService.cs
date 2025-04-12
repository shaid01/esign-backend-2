using EsignBackend.Migrations;
using EsignBackend.Models;
using EsignBackend.Models.DTOs;
using EsignBackend.Models.DTOs.Settings;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EsignBackend.Services.SettingsService.CallsPriority
{
    public class CallsPriorityService : ICallsPriorityService
    {
        private readonly AppDbContext _context;
        private readonly ILogger _logger;
        private static object _locker = new object();

        public CallsPriorityService(AppDbContext context, ILogger logger)
        {
            _context = context;
            _logger = logger;
        }

        private bool isNameAlreadyInUse(int id, string title)
        {
            if (id >= 0)
            {
                return _context.Callpriorities.Where(x => x.Title.Equals(title) && x.Id != id).Count() != 0;
            }
            else
            {
                return _context.Callpriorities.Where(x => x.Title.Equals(title)).Count() != 0;
            }
        }

        public async Task<ServiceResponse<int>> AddNewCallPriority(Callpriority callPriority)
        {
            _logger.Information($"Add new call priority: {callPriority.Title}");

            var serviceResponse = new ServiceResponse<int>();

            var nameIsAlreadyInUse = isNameAlreadyInUse(-1, callPriority.Title);

            if (nameIsAlreadyInUse)
            {
                _logger.Warning($"Add new call priority: {callPriority.Title}. Call priority name is already taken");

                serviceResponse.Success = false;
                serviceResponse.Message = "Call priority name is already taken";
                serviceResponse.Data = -1;
                return serviceResponse;
            }

            lock (_locker)
            {
                //callPriority.Id = GenerateId();
                callPriority.Id = 0;

                var newCallsPriorityInDb = _context.Callpriorities.Add(callPriority);

                try
                {
                    _context.SaveChanges();
                    serviceResponse.Data = newCallsPriorityInDb.Entity.Id;
                    serviceResponse.Amount = _context.Callpriorities.Where(x => !string.IsNullOrWhiteSpace(x.Title)).Count();
                    serviceResponse.Success = true;
                    return serviceResponse;
                }
                catch (Exception exception)
                {
                    _logger.Error($"Add new call priority: {callPriority.Title} exception: {exception}");

                    serviceResponse.Success = false;
                    serviceResponse.Message = $"Adding new callPriority failed. {exception}";
                    serviceResponse.Data = -1;
                    return serviceResponse;
                }
            }
        }

        public ServiceResponse<List<CallpriorityDTO>> GetCallsPriority(int skip, int take)
        {
            _logger.Debug($"Get calls priority: skip - {skip}, take - {take}");

            //var serviceResponse = new ServiceResponse<List<CallpriorityDTO>>();
            //var data = _context.Callpriorities.Skip(skip).Take(take).ToList();
            //var callpriorityDTOList = new List<CallpriorityDTO>();
            //foreach (var cp in data)
            //{
            //    callpriorityDTOList.Add(new CallpriorityDTO(cp));
            //}
            //serviceResponse.Amount = _context.Callpriorities.Count();
            //serviceResponse.Data = callpriorityDTOList;
            //return serviceResponse;

            var serviceResponse = new ServiceResponse<List<CallpriorityDTO>>();
            serviceResponse.Amount = _context.Callpriorities.Where(x => !string.IsNullOrWhiteSpace(x.Title)).Count();

            List<Callpriority> callPriorities = null;

            try
            {
                callPriorities = _context.Callpriorities.Where(x => !string.IsNullOrWhiteSpace(x.Title))
                    .Skip(skip)
                    .Take(take)
                    .ToList();
            }
            catch (Exception ex)
            {
                _logger.Error($"Get calls priority error: {ex}");

                serviceResponse.Data = null;
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;

                return serviceResponse;
            }

            var outputList = new List<CallpriorityDTO>();

            foreach (var callP in callPriorities)
            {
                outputList.Add(new CallpriorityDTO(callP));
            }

            serviceResponse.Success = true;
            serviceResponse.Data = outputList;

            return serviceResponse;
        }

        public async Task<ServiceResponse<int>> UpdateCallPriority(Callpriority updatedCallPriority)
        {
            _logger.Information($"Update call priority ID {updatedCallPriority.Id} to {updatedCallPriority.Title}");

            var serviceResponse = new ServiceResponse<int>();

            var nameIsAlreadyInUse = isNameAlreadyInUse(updatedCallPriority.Id, updatedCallPriority.Title);

            if (nameIsAlreadyInUse)
            {
                _logger.Warning($"Update call priority ID {updatedCallPriority.Id} to {updatedCallPriority.Title}. Call priority name is already taken.");

                serviceResponse.Success = false;
                serviceResponse.Message = "Call priority name is already taken";
                serviceResponse.Data = -1;
                return serviceResponse;
            }

            var updatedCallsPriorityInDb = _context.Callpriorities.Update(updatedCallPriority);

            try
            {
                await _context.SaveChangesAsync();
                serviceResponse.Data = updatedCallsPriorityInDb.Entity.Id;
                serviceResponse.Message = "CallPriority updated successfully.";
                serviceResponse.Success = true;
            }
            catch (Exception exception)
            {
                _logger.Error("Exception detected while trying to UpdateCallPriority: " + exception);

                serviceResponse.Success = false;
                serviceResponse.Message = $"Updated failed. {exception}";
                serviceResponse.Data = -1;
            }
            return serviceResponse;
        }
    }
}