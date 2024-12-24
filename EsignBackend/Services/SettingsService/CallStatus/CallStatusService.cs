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

        private int GenerateId()
        {
            _logger.Debug("GenerateId");
            lock (_locker)
            {
                int maxId = _context.Callstatuses.OrderByDescending(item => item.Id).Take(1).ToList()[0].Id;
                return maxId + 1;
            }

        }

        private bool IsTheNameAlreadyInUse(string title)
        {
            _logger.Debug("IsTheNameAlreadyInUse");
            return _context.Callstatuses.Where(item => item.Title.Equals(title)).Count() != 0;
        }

        public async Task<ServiceResponse<int>> AddNewCallStatus(Callstatus callstatus)
        {
            _logger.Debug("AddNewCallStatus");

            var serviceRespone = new ServiceResponse<int>();
            var nameIsAlreadyTaken = IsTheNameAlreadyInUse(callstatus.Title);
            if (nameIsAlreadyTaken)
            {
                serviceRespone.Success = false;
                serviceRespone.Message = "Callstatus name is already taken";
                serviceRespone.Data = -1;
                return serviceRespone;
            }
            lock (_locker)
            {
                //callstatus.Id = GenerateId();
                callstatus.Id = 0;
                var newCallStatusInDb = _context.Callstatuses.Add(callstatus);
                try
                {
                    _context.SaveChanges();
                    serviceRespone.Data = newCallStatusInDb.Entity.Id;
                    serviceRespone.Amount = _context.Callstatuses.Count();
                    return serviceRespone;
                }
                catch (Exception exception)
                {
                    _logger.Error("exception detected while trying to AddNewCallStatus: " + exception);
                    serviceRespone.Success = false;
                    serviceRespone.Message = $"Adding new expirationType failed. {exception}";
                    serviceRespone.Data = -1;
                    return serviceRespone;
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
            serviceResponse.Amount = _context.Callstatuses.Count();

            List<Callstatus> callStatuses = null;

            try
            {
                callStatuses = _context.Callstatuses
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
            var updatedCallStatusInDb = _context.Callstatuses.Update(updatedCallstatus);
            try
            {
                await _context.SaveChangesAsync();
                serviceResponse.Success = true;
                serviceResponse.Data = updatedCallStatusInDb.Entity.Id;
                serviceResponse.Message = "CallStatus updated successfully.";
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