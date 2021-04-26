using EsignBackend.Models;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EsignBackend.Services.SettingsService.IdentificationDocument
{
    public class IdentificationDocumentService : IIdentificationDocumentService
    {
        private readonly AppDbContext _context;
        private readonly ILogger _logger;
        private static object _locker = new object();

        public IdentificationDocumentService(AppDbContext context, ILogger logger)
        {
            _context = context;
            _logger = logger;
        }

        private int GenerateId()
        {
            _logger.Debug("GenerateId");
            int maxId = _context.Docstypes.OrderByDescending(item => item.Id).Take(1).ToList()[0].Id;
            return maxId + 1;
        }
        private bool isTheNameAlreadyInUse(string title)
        {
            _logger.Debug("isTheNameAlreadyInUse");
            return _context.Docstypes.Where(item => item.Title.Equals(title)).Count() != 0;
        }
        public async Task<ServiceResponse<int>> AddNewIdentificationDocument(Docstype identificationDocument)
        {
            _logger.Debug("AddNewIdentificationDocument");
            var serviceRespone = new ServiceResponse<int>();
            var nameIsAlreadyTaken = isTheNameAlreadyInUse(identificationDocument.Title);
            if (nameIsAlreadyTaken)
            {
                serviceRespone.Success = false;
                serviceRespone.Message = "IdentificationDocument name is already taken";
                serviceRespone.Data = -1;
                return serviceRespone;
            }
            lock (_locker)
            {
                identificationDocument.Id = GenerateId();
                var newIdentificationDocumentInDb = _context.Docstypes.Add(identificationDocument);
                try
                {
                    _context.SaveChanges();
                    serviceRespone.Data = newIdentificationDocumentInDb.Entity.Id;
                    return serviceRespone;
                }
                catch (Exception exception)
                {
                    _logger.Error("exception detected while trying to AddNewIdentificationDocument: " + exception);
                    serviceRespone.Success = false;
                    serviceRespone.Message = $"Adding new identificationDocument failed. {exception}";
                    serviceRespone.Data = -1;
                    return serviceRespone;
                }
            }
        }
        public async Task<ServiceResponse<int>> GetAmountOfIdentificationDocuments()
        {
            _logger.Debug("GetAmountOfIdentificationDocuments");
            var serviceResponse = new ServiceResponse<int>();
            serviceResponse.Data = _context.Docstypes.Count();
            return serviceResponse;
        }
        public async Task<ServiceResponse<List<Docstype>>> GetIdentificationDocuments(int skip, int take)
        {
            _logger.Debug("GetIdentificationDocuments");
            var serviceResponse = new ServiceResponse<List<Docstype>>();
            serviceResponse.Data = _context.Docstypes.Skip(skip).Take(take).ToList();
            serviceResponse.Message = serviceResponse.Data.Count.ToString();
            return serviceResponse;
        }
        public async Task<ServiceResponse<int>> UpdateIdentificationDocument(Docstype updatedIdentificationDocument)
        {
            _logger.Debug("UpdateIdentificationDocument");
            var serviceResponse = new ServiceResponse<int>();
            var updatedIdentificationDocumentInDb = _context.Docstypes.Update(updatedIdentificationDocument);
            try
            {
                await _context.SaveChangesAsync();
                serviceResponse.Success = true;
                serviceResponse.Data = updatedIdentificationDocumentInDb.Entity.Id;
                serviceResponse.Message = "IdentificationDocument updated successfully.";
            }
            catch (Exception exception)
            {
                _logger.Error("exception detected while trying to UpdateIdentificationDocument: " + exception);
                serviceResponse.Success = false;
                serviceResponse.Message = $"Updated failed. {exception}";
                serviceResponse.Data = -1;
            }
            return serviceResponse;
        }
        public async Task<ServiceResponse<List<Docstype>>> GetAllIdentificationDocuments()
        {
            _logger.Debug("GetAllIdentificationDocuments");
            var serviceResponse = new ServiceResponse<List<Docstype>>();
            serviceResponse.Data = _context.Docstypes.ToList();
            serviceResponse.Message = serviceResponse.Data.Count.ToString();
            return serviceResponse;
        }
    }
}