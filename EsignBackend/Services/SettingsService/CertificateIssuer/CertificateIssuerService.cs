using EsignBackend.Models;
using EsignBackend.Models.DTOs;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EsignBackend.Services.SettingsService.CertificateIssuer
{
    public class CertificateIssuerService : ICertificateIssuerService
    {
        private readonly AppDbContext _context;
        private readonly ILogger _logger;
        private static object _locker = new object();

        public CertificateIssuerService(AppDbContext context, ILogger logger)
        {
            _context = context;
            _logger = logger;
        }
        private int GenerateId()
        {
            _logger.Debug("GenerateId");
            lock (_locker)
            {
                int maxId = _context.Isscerts.OrderByDescending(item => item.Id).Take(1).ToList()[0].Id;
                return maxId + 1;
            }
        }
        private bool IsTheNameAlreadyInUse(string title)
        {
            _logger.Debug("IsTheNameAlreadyInUse");
            return _context.Isscerts.Where(item => item.Title.Equals(title)).Count() != 0;
        }
        public async Task<ServiceResponse<int>> AddNewCertificateIssuer(Isscert certificateIssuer)
        {
            _logger.Debug("AddNewCertificatesStatus");
            var serviceRespone = new ServiceResponse<int>();
            var nameIsAlreadyTaken = IsTheNameAlreadyInUse(certificateIssuer.Title);
            if (nameIsAlreadyTaken)
            {
                serviceRespone.Success = false;
                serviceRespone.Message = "CertificateIssuer name is already taken";
                serviceRespone.Data = -1;
                return serviceRespone;
            }

            lock (_locker)
            {
                //certificateIssuer.Id = GenerateId();
                certificateIssuer.Id = 0;

                var newCertificateIssuerInDb = _context.Isscerts.Add(certificateIssuer);
                try
                {
                    _context.SaveChanges();
                    serviceRespone.Data = newCertificateIssuerInDb.Entity.Id;
                    serviceRespone.Amount = _context.Isscerts.Count();
                    return serviceRespone;
                }
                catch (Exception exception)
                {
                    _logger.Error("exception detected while trying to AddNewCertificatesStatus: " + exception);
                    serviceRespone.Success = false;
                    serviceRespone.Message = $"Adding new certificateIssuer failed. {exception}";
                    serviceRespone.Data = -1;
                    return serviceRespone;
                }
            }
        }
        public async Task<ServiceResponse<List<IsscertDTO>>> GetCertificateIssuers(int skip, int take)
        {
            _logger.Debug("GetCertificateIssuers");
            var serviceResponse = new ServiceResponse<List<IsscertDTO>>();
            var issuers = new List<IsscertDTO>();
            var data = _context.Isscerts.Skip(skip).Take(take).ToList();
            foreach (var issuer in data)
            {
                issuers.Add(new IsscertDTO(issuer));
            }
            serviceResponse.Amount = _context.Isscerts.Count();
            serviceResponse.Data = issuers;
            return serviceResponse;
        }
        public async Task<ServiceResponse<int>> UpdateCertificateIssuer(Isscert updatedCertificateIssuer)
        {
            _logger.Debug("UpdateCustomerIdentifer");
            var serviceResponse = new ServiceResponse<int>();
            var updatedCertificateIssuerInDb = _context.Isscerts.Update(updatedCertificateIssuer);
            try
            {
                await _context.SaveChangesAsync();
                serviceResponse.Success = true;
                serviceResponse.Data = updatedCertificateIssuerInDb.Entity.Id;
                serviceResponse.Message = "CertificateIssuer updated successfully.";
            }
            catch (Exception exception)
            {
                _logger.Error("exception detected while trying to UpdateCustomerIdentifer: " + exception);
                serviceResponse.Success = false;
                serviceResponse.Message = $"Updated failed. {exception}";
                serviceResponse.Data = -1;
            }
            return serviceResponse;
        }
        public async Task<ServiceResponse<List<IsscertDTO>>> GetAllCertificateIssuers()
        {
            _logger.Debug("GetAllCertificateIssuers");
            var serviceResponse = new ServiceResponse<List<IsscertDTO>>();
            var outputList = new List<IsscertDTO>();
            var data = _context.Isscerts.ToList();
            foreach (var ci in data)
            {
                outputList.Add(new IsscertDTO(ci));
            }
            serviceResponse.Amount = _context.Isscerts.Count();
            serviceResponse.Data = outputList;
            return serviceResponse;
        }
    }
}