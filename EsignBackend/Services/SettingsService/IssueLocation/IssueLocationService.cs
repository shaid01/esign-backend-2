using EsignBackend.Models;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EsignBackend.Services.SettingsService.IssueLocation
{
    public class IssueLocationService : IIssueLocationService
    {
        private readonly AppDbContext _context;
        private readonly ILogger _logger;
        private static object _locker = new object();

        public IssueLocationService(AppDbContext context, ILogger logger)
        {
            _context = context;
            _logger = logger;
        }
        private int GenerateId()
        {
            _logger.Debug("GenerateId");
            int maxId = _context.Issplaces.OrderByDescending(item => item.Id).Take(1).ToList()[0].Id;
            return maxId + 1;
        }
        private bool isTheNameAlreadyInUse(string title)
        {
            _logger.Debug("isTheNameAlreadyInUse");
            return _context.Issplaces.Where(item => item.Title.Equals(title)).Count() != 0;
        }
        public async Task<ServiceResponse<int>> AddNewIssueLocation(Issplace issueLocation)
        {
            _logger.Debug("AddNewIssueLocation");

            var serviceRespone = new ServiceResponse<int>();
            var nameIsAlreadyTaken = isTheNameAlreadyInUse(issueLocation.Title);
            if (nameIsAlreadyTaken)
            {
                serviceRespone.Success = false;
                serviceRespone.Message = "IssueLocation name is already taken";
                serviceRespone.Data = -1;
                return serviceRespone;
            }
            lock (_locker)
            {
                issueLocation.Id = GenerateId();
                var newIssueLocationInDb = _context.Issplaces.Add(issueLocation);
                try
                {
                     _context.SaveChanges();
                    serviceRespone.Data = newIssueLocationInDb.Entity.Id;
                    return serviceRespone;
                }
                catch (Exception exception)
                {
                    _logger.Error("exception detected while trying to AddNewIssueLocation: " + exception);
                    serviceRespone.Success = false;
                    serviceRespone.Message = $"Adding new issueLocation failed. {exception}";
                    serviceRespone.Data = -1;
                    return serviceRespone;
                }
            }
        }
        public async Task<ServiceResponse<int>> GetAmountOfIssueLocations()
        {
            _logger.Debug("GetAmountOfIssueLocations");
            var serviceResponse = new ServiceResponse<int>();
            serviceResponse.Data = _context.Issplaces.Count();
            return serviceResponse;
        }
        public async Task<ServiceResponse<List<Issplace>>> GetIssueLocations(int skip, int take)
        {
            _logger.Debug("GetIssueLocations");
            var serviceResponse = new ServiceResponse<List<Issplace>>();
            serviceResponse.Data = _context.Issplaces.Skip(skip).Take(take).ToList();
            serviceResponse.Message = serviceResponse.Data.Count().ToString();
            return serviceResponse;
        }
        public async Task<ServiceResponse<int>> UpdateIssueLocation(Issplace updatedIssueLocation)
        {
            _logger.Debug("UpdateIssueLocation");
            var serviceResponse = new ServiceResponse<int>();
            var updatedIssueLocationInDb = _context.Issplaces.Update(updatedIssueLocation);
            try
            {
                await _context.SaveChangesAsync();
                serviceResponse.Success = true;
                serviceResponse.Data = updatedIssueLocationInDb.Entity.Id;
                serviceResponse.Message = "IssueLocation updated successfully.";
            }
            catch (Exception exception)
            {
                _logger.Error("exception detected while trying to UpdateIssueLocation: " + exception);
                serviceResponse.Success = false;
                serviceResponse.Message = $"Updated failed. {exception}";
                serviceResponse.Data = -1;
            }
            return serviceResponse;
        }
        public async Task<ServiceResponse<List<Issplace>>> GetAllIssueLocations()
        {
            _logger.Debug("GetAllIssueLocations");
            var serviceResponse = new ServiceResponse<List<Issplace>>();
            var dbCustomersIdentifiers = 
            serviceResponse.Data = _context.Issplaces.ToList();
            serviceResponse.Message = serviceResponse.Data.Count().ToString();
            return serviceResponse;
        }
    }
}