using EsignBackend.Migrations;
using EsignBackend.Models;
using EsignBackend.Models.DTOs;
using EsignBackend.Models.DTOs.Settings;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
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
            _logger.Information($"Add new call status: {callstatus.Title}");

            var serviceResponse = new ServiceResponse<int>();

            var nameIsAlreadyTaken = isNameAlreadyInUse(-1, callstatus.Title);

            if (nameIsAlreadyTaken)
            {
                _logger.Warning($"Add new call status: {callstatus.Title}. Call status name is already taken");

                serviceResponse.Success = false;
                serviceResponse.Message = "Call status name is already taken";
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
                    _logger.Error($"Add new call status: {callstatus.Title} exception: {exception}");

                    serviceResponse.Success = false;
                    serviceResponse.Message = $"Adding new call status failed. {exception}";
                    serviceResponse.Data = -1;
                    return serviceResponse;
                }
            }
        }

        public ServiceResponse<List<CallStatusDto>> GetCallsStatus(int skip, int take)
        {
            _logger.Debug($"Get calls status: skip - {skip}, take - {take}");

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
                _logger.Error($"Get calls status error: {ex}");

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
            _logger.Information($"Update call status ID {updatedCallstatus.Id} to {updatedCallstatus.Title}");

            var serviceResponse = new ServiceResponse<int>();

            var nameIsAlreadyTaken = isNameAlreadyInUse(updatedCallstatus.Id, updatedCallstatus.Title);

            if (nameIsAlreadyTaken)
            {
                _logger.Warning($"Update call status ID {updatedCallstatus.Id} to {updatedCallstatus.Title}. Call status name is already taken.");

                serviceResponse.Success = false;
                serviceResponse.Message = "Call status name is already taken";
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
                _logger.Error("Exception detected while trying to UpdateCallStatus: " + exception);

                serviceResponse.Success = false;
                serviceResponse.Message = $"Updated failed. {exception}";
                serviceResponse.Data = -1;
            }
            return serviceResponse;
        }
    }
}