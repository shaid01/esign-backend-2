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

        private bool isNameAlreadyInUse(int id, string title)
        {
            // case-insensitive (collation "Hebrew_CI_AS" case-insensitive) IQueryable<T>
            if (id >= 0)
            {
                return _context.Issplaces.Where(x => x.Title.Equals(title) && x.Id != id).Count() != 0;
            }
            else
            {
                return _context.Issplaces.Where(x => x.Title.Equals(title)).Count() != 0;
            }
        }

        public async Task<ServiceResponse<int>> AddNewIssueLocation(Issplace issueLocation)
        {
            _logger.Information($"Add new Issue Location: {issueLocation.Title}");

            var serviceResponse = new ServiceResponse<int>();

            var nameIsAlreadyTaken = isNameAlreadyInUse(-1, issueLocation.Title);

            if (nameIsAlreadyTaken)
            {
                _logger.Warning($"Add new issue location: {issueLocation.Title}. Issue location name is already taken");

                serviceResponse.Success = false;
                serviceResponse.Message = "IssueLocation name is already taken";
                serviceResponse.Data = -1;
                return serviceResponse;
            }

            lock (_locker)
            {
                //issueLocation.Id = GenerateId();
                issueLocation.Id = 0;

                var newIssueLocationInDb = _context.Issplaces.Add(issueLocation);

                try
                {
                    _context.SaveChanges();
                    serviceResponse.Data = newIssueLocationInDb.Entity.Id;
                    serviceResponse.Amount = _context.Issplaces.Count();
                    serviceResponse.Success = true;
                    return serviceResponse;
                }
                catch (Exception exception)
                {
                    _logger.Error($"Add new issue location: {issueLocation.Title} exception: {exception}");

                    serviceResponse.Success = false;
                    serviceResponse.Message = $"Adding new issueLocation failed. {exception}";
                    serviceResponse.Data = -1;
                    return serviceResponse;
                }
            }
        }

        public ServiceResponse<List<IssplaceDTO>> GetIssueLocations(int skip, int take)
        {
            _logger.Debug($"Get issue locations: skip - {skip}, take - {take}");

            //var serviceResponse = new ServiceResponse<List<IssplaceDTO>>();
            //var outputList = new List<IssplaceDTO>();
            //var data = _context.Issplaces.Skip(skip).Take(take).ToList();
            //foreach (var il in data)
            //{
            //    outputList.Add(new IssplaceDTO(il));
            //}

            //serviceResponse.Amount = _context.Issplaces.Count();
            //serviceResponse.Data = outputList;
            //return serviceResponse;

            var serviceResponse = new ServiceResponse<List<IssplaceDTO>>();
            serviceResponse.Amount = _context.Issplaces.Where(x => !string.IsNullOrWhiteSpace(x.Title)).Count();

            List<Issplace> issPlaces = null;

            try
            {
                issPlaces = _context.Issplaces
                    .Where(x => !string.IsNullOrWhiteSpace(x.Title))
                    .Skip(skip)
                    .Take(take)
                    .ToList();
            }
            catch (Exception ex)
            {
                _logger.Error($"Get issue locations error: {ex}");

                serviceResponse.Data = null;
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;

                return serviceResponse;
            }

            var outputList = new List<IssplaceDTO>();

            foreach (var issPlace in issPlaces)
            {
                outputList.Add(new IssplaceDTO(issPlace));
            }

            serviceResponse.Success = true;
            serviceResponse.Data = outputList;

            return serviceResponse;
        }

        public async Task<ServiceResponse<int>> UpdateIssueLocation(Issplace updatedIssueLocation)
        {
            _logger.Information($"Update issue location ID {updatedIssueLocation.Id} to {updatedIssueLocation.Title}");

            var serviceResponse = new ServiceResponse<int>();

            var nameIsAlreadyTaken = isNameAlreadyInUse(updatedIssueLocation.Id, updatedIssueLocation.Title);

            if (nameIsAlreadyTaken)
            {
                _logger.Warning($"Update issue location ID {updatedIssueLocation.Id} to {updatedIssueLocation.Title}. Issue location name is already taken.");

                serviceResponse.Success = false;
                serviceResponse.Message = "IssueLocation name is already taken";
                serviceResponse.Data = -1;
                return serviceResponse;
            }

            var updatedIssueLocationInDb = _context.Issplaces.Update(updatedIssueLocation);

            try
            {
                await _context.SaveChangesAsync();
                serviceResponse.Data = updatedIssueLocationInDb.Entity.Id;
                serviceResponse.Message = "IssueLocation updated successfully.";
                serviceResponse.Success = true;
            }
            catch (Exception exception)
            {
                _logger.Error("Exception detected while trying to UpdateIssueLocation: " + exception);

                serviceResponse.Success = false;
                serviceResponse.Message = $"Updated failed. {exception}";
                serviceResponse.Data = -1;
            }
            return serviceResponse;
        }

        public ServiceResponse<List<IssplaceDTO>> GetAllIssueLocations()
        {
            _logger.Debug("Get all issue locations");

            var serviceResponse = new ServiceResponse<List<IssplaceDTO>>();

            var outputList = new List<IssplaceDTO>();

            var data = _context.Issplaces.Where(x => !string.IsNullOrWhiteSpace(x.Title)).ToList();

            foreach (var il in data)
            {
                outputList.Add(new IssplaceDTO(il));
            }

            serviceResponse.Amount = _context.Issplaces.Where(x => !string.IsNullOrWhiteSpace(x.Title)).Count();
            serviceResponse.Data = outputList;

            return serviceResponse;
        }
    }
}