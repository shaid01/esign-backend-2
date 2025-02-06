using EsignBackend.Models;
using EsignBackend.Models.DTOs;
using EsignBackend.Models.DTOs.Settings;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EsignBackend.Services.SettingsService.CallStatus
{
    public class CallStatusService : ICallStatusService
    {
        private readonly AppDbContext _context;
        private readonly ILogger _logger;
        private static object _locker = new object();

        public CallStatusService(AppDbContext context, ILogger logger)
        {
            _context = context;
            _logger = logger;
        }

        private bool isNameAlreadyInUse(int id, string title)
        {
            if (id >= 0)
            {
                return _context.Callstatuses.Where(x => x.Title.Equals(title) && x.Id != id).Count() != 0;
            }
            else
            {
                return _context.Callstatuses.Where(x => x.Title.Equals(title)).Count() != 0;
            }
        }

        public async Task<ServiceResponse<int>> AddNewCallStatus(Callstatus callstatus)
        {
            _logger.Debug("AddNewCallStatus");

            var serviceResponse = new ServiceResponse<int>();

            var nameIsAlreadyTaken = isNameAlreadyInUse(-1, callstatus.Title);

            if (nameIsAlreadyTaken)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Callstatus name is already taken";
                serviceResponse.Data = -1;
                return serviceResponse;
            }

            lock (_locker)
            {
                //callstatus.Id = GenerateId();
                callstatus.Id = 0;
                var newCallStatusInDb = _context.Callstatuses.Add(callstatus);
                try
                {
                    _context.SaveChanges();
                    serviceResponse.Data = newCallStatusInDb.Entity.Id;
                    serviceResponse.Amount = _context.Callstatuses.Where(x => !string.IsNullOrWhiteSpace(x.Title)).Count();
                    serviceResponse.Success = true;
                    return serviceResponse;
                }
                catch (Exception exception)
                {
                    _logger.Error("exception detected while trying to AddNewCallStatus: " + exception);
                    serviceResponse.Success = false;
                    serviceResponse.Message = $"Adding new expirationType failed. {exception}";
                    serviceResponse.Data = -1;
                    return serviceResponse;
                }
            }
        }

        public ServiceResponse<List<CallStatusDto>> GetCallsStatus(int skip, int take)
        {
            _logger.Debug("GetCallsStatus");

            //var serviceResponse = new ServiceResponse<List<CallStatusDto>>();
            //serviceResponse.Amount = _context.Callstatuses.Count();
            //var dbCallStatus = _context.Callstatuses.Skip(skipInt).Take(takeInt).ToList();
            //var CallStatusList = new List<CallStatusDto>();
            //foreach (var cs in dbCallStatus)
            //{
            //    CallStatusList.Add(new CallStatusDto(cs));
            //}
            //serviceResponse.Data = CallStatusList;
            //return serviceResponse;

            var serviceResponse = new ServiceResponse<List<CallStatusDto>>();
            serviceResponse.Amount = _context.Callstatuses.Where(x => !string.IsNullOrWhiteSpace(x.Title)).Count();

            List<Callstatus> callStatuses = null;

            try
            {
                callStatuses = _context.Callstatuses.Where(x => !string.IsNullOrWhiteSpace(x.Title))
                    .Skip(skip)
                    .Take(take)
                    .ToList();
            }
            catch (Exception ex)
            {
                _logger.Error($"Error while processing DB query in GetCallsStatus. {ex.Message}");

                serviceResponse.Data = null;
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;

                return serviceResponse;
            }

            var outputList = new List<CallStatusDto>();

            foreach (var callStatus in callStatuses)
            {
                outputList.Add(new CallStatusDto(callStatus));
            }

            serviceResponse.Success = true;
            serviceResponse.Data = outputList;

            return serviceResponse;
        }

        public async Task<ServiceResponse<int>> UpdateCallStatus(Callstatus updatedCallstatus)
        {
            _logger.Debug("UpdateCallStatus");

            var serviceResponse = new ServiceResponse<int>();

            var nameIsAlreadyTaken = isNameAlreadyInUse(updatedCallstatus.Id, updatedCallstatus.Title);

            if (nameIsAlreadyTaken)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Callstatus name is already taken";
                serviceResponse.Data = -1;
                return serviceResponse;
            }

            var updatedCallStatusInDb = _context.Callstatuses.Update(updatedCallstatus);

            try
            {
                await _context.SaveChangesAsync();
                serviceResponse.Data = updatedCallStatusInDb.Entity.Id;
                serviceResponse.Message = "CallStatus updated successfully.";
                serviceResponse.Success = true;
            }
            catch (Exception exception)
            {
                _logger.Error("exception detected while trying to UpdateCallStatus: " + exception);
                serviceResponse.Success = false;
                serviceResponse.Message = $"Updated failed. {exception}";
                serviceResponse.Data = -1;
            }
            return serviceResponse;
        }
    }
}