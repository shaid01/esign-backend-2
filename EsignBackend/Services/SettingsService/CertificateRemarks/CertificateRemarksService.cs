using EsignBackend.Models;
using EsignBackend.Models.DTOs.Settings;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EsignBackend.Services.SettingsService.CertificateRemarks
{
    public class CertificateRemarksService : ICertificateRemarksService
    {
        private readonly AppDbContext _context;
        private readonly ILogger _logger;
        private static object _locker = new object();

        public CertificateRemarksService(AppDbContext context, ILogger logger)
        {
            _context = context;
            _logger = logger;
        }

        private int GenerateId()
        {
            _logger.Debug("GenerateId");
            lock (_locker)
            {
                int maxId = _context.Certificatermearks.OrderByDescending(item => item.Id).Take(1).ToList()[0].Id;
                return maxId + 1;
            }
        }
        private bool IsTheNameAlreadyInUse(string title)
        {
            _logger.Debug("IsTheNameAlreadyInUse");
            return _context.Certificatermearks.Where(item => item.Title.Equals(title)).Count() != 0;
        }

        public async Task<ServiceResponse<int>> GetAmountOfCertificateRemarks()
        {
            _logger.Debug("GetAmountOfCertificateRemarks");
            var serviceResponse = new ServiceResponse<int>();
            serviceResponse.Data = _context.Certificatermearks.Count();
            return serviceResponse;
        }
        public async Task<ServiceResponse<List<CertificateremarkDTO>>> GetCertificateRemarks(int skip, int take)
        {
            _logger.Debug("GetCertificateRemarks");
            var serviceResponse = new ServiceResponse<List<CertificateremarkDTO>>();
            var data = _context.Certificatermearks.Skip(skip).Take(take).ToList();
            var outputData = new List<CertificateremarkDTO>();
            foreach (var cr in data)
            {
                outputData.Add(new CertificateremarkDTO(cr));
            }
            serviceResponse.Amount = _context.Certificatermearks.Count();
            serviceResponse.Data = outputData;
            return serviceResponse;
        }
        public async Task<ServiceResponse<int>> UpdateCertificateRemarks(Certificatermeark updatedCertificateRemarks)
        {
            _logger.Debug("UpdateCertificateRemarks");
            var serviceResponse = new ServiceResponse<int>();
            var updatedCertificateRemarksInDb = _context.Certificatermearks.Update(updatedCertificateRemarks);
            try
            {
                await _context.SaveChangesAsync();
                serviceResponse.Success = true;
                serviceResponse.Data = updatedCertificateRemarksInDb.Entity.Id;
                serviceResponse.Message = "CertificateRemarks updated successfully.";
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
        public async Task<ServiceResponse<int>> AddNewCertificateRemarks(Certificatermeark certificateRemark)
        {
            _logger.Debug("AddNewCertificateRemarks");
            var serviceRespone = new ServiceResponse<int>();
            var nameIsAlreadyTaken = IsTheNameAlreadyInUse(certificateRemark.Title);
            if (nameIsAlreadyTaken)
            {
                serviceRespone.Success = false;
                serviceRespone.Message = "ExpirationType name is already taken";
                serviceRespone.Data = -1;
                return serviceRespone;
            }
            lock (_locker)
            {
                //certificateRemark.Id = GenerateId();
                //certificateRemark.Id = 0;
               
                var newCertificateRemarksInDb = _context.Certificatermearks.Add(new Certificatermeark() { Title = certificateRemark.Title });
                try
                {
                    _context.SaveChanges();
                    serviceRespone.Amount = _context.Certificatermearks.Count();
                    serviceRespone.Data = newCertificateRemarksInDb.Entity.Id;
                    return serviceRespone;
                }
                catch (Exception exception)
                {
                    _logger.Error("exception detected while trying to AddNewCertificateRemarks: " + exception);
                    serviceRespone.Success = false;
                    serviceRespone.Message = $"Adding new expirationType failed. {exception}";
                    serviceRespone.Data = -1;
                    return serviceRespone;
                }
            }
        }
    }
}