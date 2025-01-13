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

        private bool isNameAlreadyInUse(string title)
        {
            return _context.Isscerts.Where(item => item.Title.Equals(title)).Count() != 0;
        }

        public async Task<ServiceResponse<int>> AddNewCertificateIssuer(Isscert certificateIssuer)
        {
            _logger.Debug("AddNewCertificatesStatus");

            var serviceResponse = new ServiceResponse<int>();

            var nameIsAlreadyTaken = isNameAlreadyInUse(certificateIssuer.Title);

            if (nameIsAlreadyTaken)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "CertificateIssuer name is already taken";
                serviceResponse.Data = -1;
                return serviceResponse;
            }

            lock (_locker)
            {
                //certificateIssuer.Id = GenerateId();
                certificateIssuer.Id = 0;

                var newCertificateIssuerInDb = _context.Isscerts.Add(certificateIssuer);

                try
                {
                    _context.SaveChanges();
                    serviceResponse.Success = true;
                    serviceResponse.Data = newCertificateIssuerInDb.Entity.Id;
                    serviceResponse.Amount = _context.Isscerts.Where(x => !string.IsNullOrWhiteSpace(x.Title)).Count();
                    serviceResponse.Success = true;
                    return serviceResponse;
                }
                catch (Exception exception)
                {
                    _logger.Error("exception detected while trying to AddNewCertificatesStatus: " + exception);
                    serviceResponse.Success = false;
                    serviceResponse.Message = $"Adding new certificateIssuer failed. {exception}";
                    serviceResponse.Data = -1;
                    return serviceResponse;
                }
            }
        }

        public ServiceResponse<List<IsscertDTO>> GetCertificateIssuers(int skip, int take)
        {
            _logger.Debug("GetCertificateIssuers");

            //var serviceResponse = new ServiceResponse<List<IsscertDTO>>();
            //var issuers = new List<IsscertDTO>();
            //var data = _context.Isscerts.Skip(skip).Take(take).ToList();
            //foreach (var issuer in data)
            //{
            //    issuers.Add(new IsscertDTO(issuer));
            //}
            //serviceResponse.Amount = _context.Isscerts.Count();
            //serviceResponse.Data = issuers;
            //return serviceResponse;

            var serviceResponse = new ServiceResponse<List<IsscertDTO>>();
            serviceResponse.Amount = _context.Isscerts.Where(x => !string.IsNullOrWhiteSpace(x.Title)).Count();

            List<Isscert> issCerts = null;

            try
            {
                issCerts = _context.Isscerts.Where(x => !string.IsNullOrWhiteSpace(x.Title))
                    .Skip(skip)
                    .Take(take)
                    .ToList();
            }
            catch (Exception ex)
            {
                _logger.Error($"Error while processing DB query in GetCertificateIssuers. {ex.Message}");

                serviceResponse.Data = null;
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;

                return serviceResponse;
            }

            var outputList = new List<IsscertDTO>();

            foreach (var iss in issCerts)
            {
                outputList.Add(new IsscertDTO(iss));
            }

            serviceResponse.Success = true;
            serviceResponse.Data = outputList;

            return serviceResponse;
        }

        public async Task<ServiceResponse<int>> UpdateCertificateIssuer(Isscert updatedCertificateIssuer)
        {
            _logger.Debug("UpdateCertificateIssuer");

            var serviceResponse = new ServiceResponse<int>();

            var nameIsAlreadyTaken = isNameAlreadyInUse(updatedCertificateIssuer.Title);

            if (nameIsAlreadyTaken)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "CertificateIssuer name is already taken";
                serviceResponse.Data = -1;
                return serviceResponse;
            }

            var updatedCertificateIssuerInDb = _context.Isscerts.Update(updatedCertificateIssuer);

            try
            {
                await _context.SaveChangesAsync();
                serviceResponse.Data = updatedCertificateIssuerInDb.Entity.Id;
                serviceResponse.Message = "CertificateIssuer updated successfully.";
                serviceResponse.Success = true;
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

        public ServiceResponse<List<IsscertDTO>> GetAllCertificateIssuers()
        {
            _logger.Debug("GetAllCertificateIssuers");

            var serviceResponse = new ServiceResponse<List<IsscertDTO>>();

            var outputList = new List<IsscertDTO>();

            var data = _context.Isscerts.Where(x => !string.IsNullOrWhiteSpace(x.Title)).ToList();

            foreach (var ci in data)
            {
                outputList.Add(new IsscertDTO(ci));
            }

            serviceResponse.Amount = outputList.Count();//_context.Isscerts.Where(x => !string.IsNullOrWhiteSpace(x.Title)).Count();
            serviceResponse.Data = outputList;
            return serviceResponse;
        }
    }
}