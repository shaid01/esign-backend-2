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

        private bool isNameAlreadyInUse(int id, string title)
        {
            if (id >= 0)
            {
                return _context.Docstypes.Where(x => x.Title.Equals(title) && x.Id != id).Count() != 0;
            }
            else
            {
                return _context.Docstypes.Where(x => x.Title.Equals(title)).Count() != 0;
            }
        }

        public async Task<ServiceResponse<int>> AddNewIdentificationDocument(Docstype identificationDocument)
        {
            _logger.Information($"Add new Identification Document: {identificationDocument.Title}");

            var serviceResponse = new ServiceResponse<int>();

            var nameIsAlreadyTaken = isNameAlreadyInUse(-1, identificationDocument.Title);

            if (nameIsAlreadyTaken)
            {
                _logger.Warning($"Add new Identification Document: {identificationDocument.Title}. Identification Document name is already taken");

                serviceResponse.Success = false;
                serviceResponse.Message = "IdentificationDocument name is already taken";
                serviceResponse.Data = -1;
                return serviceResponse;
            }

            lock (_locker)
            {
                /* identificationDocument.Id = GenerateId();*/
                identificationDocument.Id = 0;

                var newIdentificationDocumentInDb = _context.Docstypes.Add(identificationDocument);

                try
                {
                    _context.SaveChanges();
                    serviceResponse.Amount = _context.Docstypes.Where(x => !string.IsNullOrWhiteSpace(x.Title)).Count();
                    serviceResponse.Data = newIdentificationDocumentInDb.Entity.Id;
                    serviceResponse.Success = true;
                    return serviceResponse;
                }
                catch (Exception exception)
                {
                    _logger.Error($"Add new Identification Document: {identificationDocument.Title} exception: {exception}");

                    serviceResponse.Success = false;
                    serviceResponse.Message = $"Adding new identificationDocument failed. {exception}";
                    serviceResponse.Data = -1;
                    return serviceResponse;
                }
            }
        }

        public ServiceResponse<List<DocstypeDTO>> GetIdentificationDocuments(int skip, int take)
        {
            _logger.Debug($"Get Identification Documents: skip - {skip}, take - {take}");

            //var serviceResponse = new ServiceResponse<List<DocstypeDTO>>();
            //var outputList = new List<DocstypeDTO>();
            //var data = _context.Docstypes.Skip(skip).Take(take).ToList();
            //foreach (var id in data)
            //{
            //    outputList.Add(new DocstypeDTO(id));
            //}
            //serviceResponse.Data = outputList;
            //serviceResponse.Amount = _context.Docstypes.Count();
            //return serviceResponse;

            var serviceResponse = new ServiceResponse<List<DocstypeDTO>>();
            serviceResponse.Amount = _context.Docstypes.Where(x => !string.IsNullOrWhiteSpace(x.Title)).Count();

            List<Docstype> docTypes = null;

            try
            {
                docTypes = _context.Docstypes.Where(x => !string.IsNullOrWhiteSpace(x.Title))
                    .Skip(skip)
                    .Take(take)
                    .ToList();
            }
            catch (Exception ex)
            {
                _logger.Error($"Get Identification Documents error: {ex}");

                serviceResponse.Data = null;
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;

                return serviceResponse;
            }

            var outputList = new List<DocstypeDTO>();

            foreach (var docType in docTypes)
            {
                outputList.Add(new DocstypeDTO(docType));
            }

            serviceResponse.Success = true;
            serviceResponse.Data = outputList;

            return serviceResponse;
        }

        public async Task<ServiceResponse<int>> UpdateIdentificationDocument(Docstype updatedIdentificationDocument)
        {
            _logger.Information($"Update Identification Document ID {updatedIdentificationDocument.Id} to {updatedIdentificationDocument.Title}");

            var serviceResponse = new ServiceResponse<int>();

            var nameIsAlreadyTaken = isNameAlreadyInUse(updatedIdentificationDocument.Id, updatedIdentificationDocument.Title);

            if (nameIsAlreadyTaken)
            {
                _logger.Warning($"Update Identification Document ID {updatedIdentificationDocument.Id} to {updatedIdentificationDocument.Title}. Identification Document name is already taken.");

                serviceResponse.Success = false;
                serviceResponse.Message = "IdentificationDocument name is already taken";
                serviceResponse.Data = -1;
                return serviceResponse;
            }

            var updatedIdentificationDocumentInDb = _context.Docstypes.Update(updatedIdentificationDocument);

            try
            {
                await _context.SaveChangesAsync();
                serviceResponse.Data = updatedIdentificationDocumentInDb.Entity.Id;
                serviceResponse.Message = "IdentificationDocument updated successfully.";
                serviceResponse.Success = true;
            }
            catch (Exception exception)
            {
                _logger.Error("Exception detected while trying to UpdateIdentificationDocument: " + exception);

                serviceResponse.Success = false;
                serviceResponse.Message = $"Updated failed. {exception}";
                serviceResponse.Data = -1;
            }
            return serviceResponse;
        }

        public ServiceResponse<List<DocstypeDTO>> GetAllIdentificationDocuments()
        {
            _logger.Debug("Get all identification documents");

            var serviceResponse = new ServiceResponse<List<DocstypeDTO>>();

            var outputList = new List<DocstypeDTO>();

            var data = _context.Docstypes.Where(x => !string.IsNullOrWhiteSpace(x.Title)).ToList();

            foreach (var id in data)
            {
                outputList.Add(new DocstypeDTO(id));
            }

            serviceResponse.Amount = outputList.Count();//_context.Docstypes.Where(x => !string.IsNullOrWhiteSpace(x.Title)).Count();
            serviceResponse.Data = outputList;

            return serviceResponse;
        }
    }
}
