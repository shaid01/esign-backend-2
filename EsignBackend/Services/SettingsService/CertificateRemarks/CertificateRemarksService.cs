using EsignBackend.Models;
using EsignBackend.Models.DTOs;
using EsignBackend.Models.DTOs.Settings;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EsignBackend.Services.SettingsService.CertificateRemarks
{
    public class CertificateRemarksService: ICertificateRemarksService
    {
        private readonly AppDbContext _context;
        private readonly ILogger _logger;
        private static object _locker = new object();

        public CertificateRemarksService(AppDbContext context, ILogger logger)
        {
            _context = context;
            _logger = logger;
        }

        private bool isNameAlreadyInUse(int id, string title)
        {
            if (id >= 0)
            {
                return _context.Certificatermearks.Where(x => x.Title.Equals(title) && x.Id != id).Count() != 0;
            }
            else
            {
                return _context.Certificatermearks.Where(x => x.Title.Equals(title)).Count() != 0;
            }
        }

        public async Task<ServiceResponse<int>> GetAmountOfCertificateRemarks()
        {
            _logger.Debug("GetAmountOfCertificateRemarks");
            var serviceResponse = new ServiceResponse<int>();
            serviceResponse.Data = _context.Certificatermearks.Where(x => !string.IsNullOrWhiteSpace(x.Title)).Count();
            return serviceResponse;
        }

        public ServiceResponse<List<CertificateremarkDTO>> GetCertificateRemarks(int skip, int take)
        {
            _logger.Debug("GetCertificateRemarks");

            //var serviceResponse = new ServiceResponse<List<CertificateremarkDTO>>();
            //var data = _context.Certificatermearks.Skip(skip).Take(take).ToList();
            //var outputData = new List<CertificateremarkDTO>();
            //foreach (var cr in data)
            //{
            //    outputData.Add(new CertificateremarkDTO(cr));
            //}
            //serviceResponse.Amount = _context.Certificatermearks.Count();
            //serviceResponse.Data = outputData;
            //return serviceResponse;

            var serviceResponse = new ServiceResponse<List<CertificateremarkDTO>>();
            serviceResponse.Amount = _context.Certificatermearks.Where(x => !string.IsNullOrWhiteSpace(x.Title)).Count();

            List<CertificateRemark> cerRemarks = null;

            try
            {
                cerRemarks = _context.Certificatermearks.Where(x => !string.IsNullOrWhiteSpace(x.Title))
                    .Skip(skip)
                    .Take(take)
                    .ToList();
            }
            catch (Exception ex)
            {
                _logger.Error($"Error while processing DB query in GetCertificateRemarks. {ex.Message}");

                serviceResponse.Data = null;
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;

                return serviceResponse;
            }

            var outputList = new List<CertificateremarkDTO>();

            foreach (var cerRemark in cerRemarks)
            {
                outputList.Add(new CertificateremarkDTO(cerRemark));
            }

            serviceResponse.Success = true;
            serviceResponse.Data = outputList;

            return serviceResponse;
        }

        public async Task<ServiceResponse<int>> UpdateCertificateRemarks(CertificateRemark updatedCertificateRemarks)
        {
            _logger.Debug("UpdateCertificateRemarks");

            var serviceResponse = new ServiceResponse<int>();

            var nameIsAlreadyTaken = isNameAlreadyInUse(updatedCertificateRemarks.Id,  updatedCertificateRemarks.Title);

            if (nameIsAlreadyTaken)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "ExpirationType name is already taken";
                serviceResponse.Data = -1;
                return serviceResponse;
            }

            var updatedCertificateRemarksInDb = _context.Certificatermearks.Update(updatedCertificateRemarks);

            try
            {
                await _context.SaveChangesAsync();
                serviceResponse.Data = updatedCertificateRemarksInDb.Entity.Id;
                serviceResponse.Message = "CertificateRemarks updated successfully.";
                serviceResponse.Success = true;
            }
            catch (Exception exception)
            {
                _logger.Error("exception detected while trying to UpdateCertificateRemarks: " + exception);
                serviceResponse.Success = false;
                serviceResponse.Message = $"Updated failed. {exception}";
                serviceResponse.Data = -1;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<int>> AddNewCertificateRemarks(CertificateRemark certificateRemark)
        {
            _logger.Debug("AddNewCertificateRemarks");

            var serviceResponse = new ServiceResponse<int>();

            var nameIsAlreadyTaken = isNameAlreadyInUse(-1, certificateRemark.Title);

            if (nameIsAlreadyTaken)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "ExpirationType name is already taken";
                serviceResponse.Data = -1;
                return serviceResponse;
            }

            lock (_locker)
            {
                //certificateRemark.Id = GenerateId();
                //certificateRemark.Id = 0;
               
                var newCertificateRemarksInDb = _context.Certificatermearks.Add(new CertificateRemark() { Title = certificateRemark.Title });

                try
                {
                    _context.SaveChanges();
                    serviceResponse.Amount = _context.Certificatermearks.Where(x => !string.IsNullOrWhiteSpace(x.Title)).Count();
                    serviceResponse.Data = newCertificateRemarksInDb.Entity.Id;
                    serviceResponse.Success = true;
                    return serviceResponse;
                }
                catch (Exception exception)
                {
                    _logger.Error("exception detected while trying to AddNewCertificateRemarks: " + exception);
                    serviceResponse.Success = false;
                    serviceResponse.Message = $"Adding new expirationType failed. {exception}";
                    serviceResponse.Data = -1;
                    return serviceResponse;
                }
            }
        }
    }
}