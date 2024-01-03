using EsignBackend.Models;
using EsignBackend.Models.DTOs;
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
            lock (_locker)
            {
                int maxId = _context.Issplaces.OrderByDescending(item => item.Id).Take(1).ToList()[0].Id;
                return maxId + 1;
            }
        }
        private bool isTheNameAlreadyInUse(string title)
        {
            _logger.Debug("isTheNameAlreadyInUse");
            return _context.Issplaces.Where(item => item.Title.Equals(title)).Count() != 0;
        }
        public async Task<ServiceResponse<int>> AddNewIssueLocation(IssPlace issueLocation)
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
                //issueLocation.Id = GenerateId();
                issueLocation.Id = 0;
                var newIssueLocationInDb = _context.Issplaces.Add(issueLocation);
                try
                {
                    _context.SaveChanges();
                    serviceRespone.Data = newIssueLocationInDb.Entity.Id;
                    serviceRespone.Amount = _context.Issplaces.Count();
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
        public async Task<ServiceResponse<List<IssplaceDTO>>> GetIssueLocations(int skip, int take)
        {
            _logger.Debug("GetIssueLocations");
            var serviceResponse = new ServiceResponse<List<IssplaceDTO>>();
            var outputList = new List<IssplaceDTO>();
            var data = _context.Issplaces.Skip(skip).Take(take).ToList();
            foreach (var il in data)
            {
                outputList.Add(new IssplaceDTO(il));
            }

            serviceResponse.Amount = _context.Issplaces.Count();
            serviceResponse.Data = outputList;
            return serviceResponse;
        }
        public async Task<ServiceResponse<int>> UpdateIssueLocation(IssPlace updatedIssueLocation)
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
        public async Task<ServiceResponse<List<IssplaceDTO>>> GetAllIssueLocations()
        {
            _logger.Debug("GetAllIssueLocations");
            var serviceResponse = new ServiceResponse<List<IssplaceDTO>>();
            var outputList = new List<IssplaceDTO>();
            var data = _context.Issplaces.ToList();
            foreach (var il in data)
            {
                outputList.Add(new IssplaceDTO(il));
            }
            serviceResponse.Amount = _context.Issplaces.Count();
            serviceResponse.Data = outputList;
            return serviceResponse;
        }
    }
}