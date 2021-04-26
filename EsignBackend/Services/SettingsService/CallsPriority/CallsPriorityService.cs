using EsignBackend.Models;
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

        private int GenerateId()
        {
            _logger.Debug("GenerateId");
            int maxId = _context.Callpriorities.OrderByDescending(item => item.Id).Take(1).ToList()[0].Id;
            return maxId + 1;
        }
        private bool IsTheNameAlreadyInUse(string title)
        {
            _logger.Debug("IsTheNameAlreadyInUse");
            return _context.Callpriorities.Where(item => item.Title.Equals(title)).Count() != 0;
        }
        public async Task<ServiceResponse<int>> AddNewCallPriority(Callpriority callPriority)
        {
            _logger.Debug("AddNewCallPriority");
            var serviceRespone = new ServiceResponse<int>();
            var nameIsAlreadyInUse = IsTheNameAlreadyInUse(callPriority.Title);
            if (nameIsAlreadyInUse)
            {
                serviceRespone.Success = false;
                serviceRespone.Message = "Callpriority name is already taken";
                serviceRespone.Data = -1;
                return serviceRespone;
            }

            lock (_locker)
            {
                callPriority.Id = GenerateId();
                var newCallsPriorityInDb = _context.Callpriorities.Add(callPriority);
                try
                {
                    _context.SaveChanges();
                    serviceRespone.Data = newCallsPriorityInDb.Entity.Id;
                    return serviceRespone;
                }
                catch (Exception exception)
                {
                    _logger.Error("exception detected while trying to AddNewCallPriority: " + exception);
                    serviceRespone.Success = false;
                    serviceRespone.Message = $"Adding new callPriority failed. {exception}";
                    serviceRespone.Data = -1;
                    return serviceRespone;
                }
            }
        }         
        public async Task<ServiceResponse<int>> GetAmountOfCallsPriority()
        {
            _logger.Debug("GetAmountOfCallsPriority");
            var serviceResponse = new ServiceResponse<int>();
            serviceResponse.Data = _context.Callpriorities.Count();
            return serviceResponse;
        }
        public async Task<ServiceResponse<List<Callpriority>>> GetCallsPriority(int skip, int take)
        {
            _logger.Debug("GetCallsPriority");
            var serviceResponse = new ServiceResponse<List<Callpriority>>();
            serviceResponse.Data = _context.Callpriorities.Skip(skip).Take(take).ToList();
            serviceResponse.Message = serviceResponse.Data.Count.ToString();
            return serviceResponse;
        }
        public async Task<ServiceResponse<int>> UpdateCallPriority(Callpriority updatedCallPriority)
        {
            _logger.Debug("UpdateCallPriority");
            var serviceResponse = new ServiceResponse<int>();
            var updatedCallsPriorityInDb = _context.Callpriorities.Update(updatedCallPriority);
            try
            {
                await _context.SaveChangesAsync();
                serviceResponse.Success = true;
                serviceResponse.Data = updatedCallsPriorityInDb.Entity.Id;
                serviceResponse.Message = "CallPriority updated successfully.";
            }
            catch (Exception exception)
            {
                _logger.Debug("exception detected while trying to UpdateCallPriority: " + exception);
                serviceResponse.Success = false;
                serviceResponse.Message = $"Updated failed. {exception}";
                serviceResponse.Data = -1;
            }
            return serviceResponse;
        }
    }
}