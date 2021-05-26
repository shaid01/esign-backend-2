using EsignBackend.Models;
using EsignBackend.Models.DTOs;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EsignBackend.Services.SettingsService.CertificatesStatus
{
    public class CertificatesstatusService : ICertificatesstatusService
    {
        private readonly AppDbContext _context;
        private readonly ILogger _logger;
        private static object _locker = new object();

        public CertificatesstatusService(AppDbContext context, ILogger logger)
        {
            _context = context;
            _logger = logger;
        }

        private int GenerateId()
        {
            _logger.Debug("GenerateId");
            lock (_locker)
            {
                int maxId = _context.Certificatesstatuses.OrderByDescending(project => project.Id).Take(1).ToList()[0].Id;
                return maxId + 1;
            }
        }
        private bool IsTheNameAlreadyInUse(string title)
        {
            _logger.Debug("IsTheNameAlreadyInUse");
            return _context.Certificatesstatuses.Where(project => project.Title.Equals(title)).Count() != 0;
        }
        public async Task<ServiceResponse<List<CertificatesstatusDTO>>> GetCertificatesStatus(int skip, int take)
        {
            _logger.Debug("GetCertificatesStatus");
            var serviceResponse = new ServiceResponse<List<CertificatesstatusDTO>>();
            var outputList = new List<CertificatesstatusDTO>();
            var data = _context.Certificatesstatuses.Skip(skip).Take(take).ToList();
            foreach (var cs in data)
            {
                outputList.Add(new CertificatesstatusDTO(cs));
            }
            serviceResponse.Amount = _context.Certificatesstatuses.Count();
            serviceResponse.Data = outputList;
            return serviceResponse;
        }
        public async Task<ServiceResponse<int>> UpdateCertificatesStatus(Certificatesstatus updatedCertificatestatus)
        {
            _logger.Debug("UpdateCertificatesStatus");

            var serviceResponse = new ServiceResponse<int>();
            var updatedCertificatesStatusInDb = _context.Certificatesstatuses.Update(updatedCertificatestatus);
            try
            {
                await _context.SaveChangesAsync();
                serviceResponse.Success = true;
                serviceResponse.Data = updatedCertificatesStatusInDb.Entity.Id;
                serviceResponse.Message = "Certificate Status updated successfully.";
            }
            catch (Exception exception)
            {
                _logger.Error("exception detected while trying to UpdateCertificatesStatus: " + exception);
                serviceResponse.Success = false;
                serviceResponse.Message = $"Updated failed. {exception}";
                serviceResponse.Data = -1;
            }
            return serviceResponse;
        }
        public async Task<ServiceResponse<int>> AddNewCertificatesStatus(Certificatesstatus certificatestatus)
        {
            _logger.Debug("AddNewCertificatesStatus");
            var serviceRespone = new ServiceResponse<int>();
            var certificatesStatusNameIsAlreadyTaken = IsTheNameAlreadyInUse(certificatestatus.Title);
            if (certificatesStatusNameIsAlreadyTaken)
            {
                serviceRespone.Success = false;
                serviceRespone.Message = "certificatesStatus name is already taken";
                serviceRespone.Data = -1;
                return serviceRespone;
            }
            lock (_locker)
            {
                certificatestatus.Id = GenerateId();
                var newcertificatesStatusInDb = _context.Certificatesstatuses.Add(certificatestatus);
                try
                {
                    _context.SaveChanges();
                    serviceRespone.Amount = _context.Certificatesstatuses.Count();
                    serviceRespone.Data = newcertificatesStatusInDb.Entity.Id;
                    return serviceRespone;
                }
                catch (Exception exception)
                {
                    _logger.Debug("exception detected while trying to AddNewCertificatesStatus");
                    serviceRespone.Success = false;
                    serviceRespone.Message = $"Adding new certificatesStatus failed. {exception}";
                    serviceRespone.Data = -1;
                    return serviceRespone;
                }
            }
        }
        public async Task<ServiceResponse<List<CertificatesstatusDTO>>> GetAllCertificatesStatus()
        {
            _logger.Debug("GetAllCertificatesStatus");
            var serviceResponse = new ServiceResponse<List<CertificatesstatusDTO>>();
            var outputList = new List<CertificatesstatusDTO>();
            var data = _context.Certificatesstatuses.ToList();
            foreach (var cs in data)
            {
                outputList.Add(new CertificatesstatusDTO(cs));
            }
            serviceResponse.Amount = _context.Certificatesstatuses.Count();
            serviceResponse.Data = outputList;
            return serviceResponse;
        }
    }

}