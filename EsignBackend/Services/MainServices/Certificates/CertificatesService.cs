using EsignBackend.Extensions.CashHandlers;
using EsignBackend.Extensions.EncryptDecrypt;
using EsignBackend.Models;
using EsignBackend.Models.DTOs;
using EsignBackend.Models.Tools;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EsignBackend.Services.MainServices.Certificates
{
    public class CertificatesService : ICertificatesService
    {
        private readonly AppDbContext _context;
        private readonly ILogger _logger;
        private readonly ICash _cash;
        private static object _locker = new object();

        public CertificatesService(AppDbContext context, ILogger logger, ICash cash)
        {
            _context = context;
            _logger = logger;
            _cash = cash;
        }

        private CertificateDetails GenerateCertificateDetailsFromId(double cerId)
        {
            _logger.Debug("GenerateCertificateDetailsFromId");
            var chosenCertificate = _context.Certificates
                .Include(cer => cer.RelatedCertificateissuer)
                .Include(cer => cer.RelatedCertificatesstatus)
                .Include(cer => cer.RelatedCustomer)
                .Include(cer => cer.RelatedCustomerIdentifier)
                .Include(cer => cer.RelatedDocsType)
                .Include(cer => cer.RelatedExpiration)
                .Include(cer => cer.RelatedIssuerPlace)
                .Include(cer => cer.RelatedProject)
                .Include(cer => cer.RelatedSecurityquestion)
                .Include(cer => cer.RelatedSmartObject)
                .Include(cer => cer.RelatedSubProject)
                .Where(cer => cer.Id.Equals(Convert.ToInt32(cerId))).FirstOrDefault();

            var certificate = new CertificateDetails(chosenCertificate);
            return certificate;
        }

        private int GenerateCertificateId()
        {
            _logger.Debug("GenerateCertificateId");
            lock (_locker)
            {
                int maxId = _context.Certificates.OrderByDescending(cer => cer.Id).Take(1).ToList()[0].Id;
                return maxId + 1;
            }
        }

        public async Task<ServiceResponse<int>> UpdateCertificate(Certificate updatedCertificate)
        {
            _logger.Debug("UpdateCertificate");

            var serviceResponse = new ServiceResponse<int>();
            updatedCertificate.Securityansware = EncryptDecryptHandler.encryptSecurityAns(updatedCertificate.Securityansware);
            var updatedCertificateInDb = _context.Certificates.Update(updatedCertificate);
            try
            {
                _context.SaveChanges();
                serviceResponse.Success = true;
                serviceResponse.Data = updatedCertificateInDb.Entity.Id;
                serviceResponse.Message = "Certificate updated successfully.";
                _logger.Debug("Changes have been saved");
            }
            catch (Exception exception)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = $"Updated failed. {exception}";
                serviceResponse.Data = -1;
                _logger.Debug("UpdateCertificate failed " + exception);
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<CertificateDetailsDTO>>> GetCertificatesDetails(int skip, int take)
        {
            _logger.Debug("GetCertificatesDetailsUpdate");

            var serviceResponse = new ServiceResponse<List<CertificateDetailsDTO>>();
            serviceResponse.Amount = _cash.GetCounterByType(CashType.Certificate);
            var certificateDetailsList = new List<CertificateDetailsDTO>();
            var certificates = _context.Certificates
                .Include(cer => cer.RelatedCertificateissuer)
                .Include(cer => cer.RelatedCertificatesstatus)
                .Include(cer => cer.RelatedCustomer).ThenInclude(cus => cus.RelatedSecurityquestion)
                .Include(cer => cer.RelatedCustomerIdentifier)
                .Include(cer => cer.RelatedDocsType)
                .Include(cer => cer.RelatedExpiration)
                .Include(cer => cer.RelatedIssuerPlace)
                .Include(cer => cer.RelatedProject)
                .Include(cer => cer.RelatedSecurityquestion)
                .Include(cer => cer.RelatedSmartObject)
                .Include(cer => cer.RelatedSubProject)
                .Skip(skip).Take(take).ToList();

            foreach (var cer in certificates)
            {
                var newCertificateDetail = new CertificateDetailsDTO(new CertificateDetails(cer));
                certificateDetailsList.Add(newCertificateDetail);
            }
            serviceResponse.Data = certificateDetailsList;
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<HistoryCertificateDTO>>> GetHistoryCertificates(double certificateId)
        {
            _logger.Debug("GetHistoryCertificates");

            var serviceResponse = new ServiceResponse<List<HistoryCertificateDTO>>();
            var historyCertificateDeatailsList = new List<HistoryCertificateDTO>();

            var historyCertificateDeatailsUpdate = await _context.Certificateshistories
                .Include(hc => hc.RelatedCertificate)
                .Include(hc => hc.RelatedCertificateStatus)
                .Include(hc => hc.RelatedCustomer)
                .Include(hc => hc.RelatedDocsType)
                .Include(hc => hc.RelatedExpiration)
                .Include(hc => hc.RelatedProject)
                .Include(hc => hc.RelatedSecurityQuestion)
                .Include(hc => hc.RelatedSmartObject)
                .Include(hc => hc.RelatedSubProject)
                .Include(hc => hc.RelatedUser)
                .Where(hc => hc.Certificateid == certificateId).ToListAsync();


            foreach (var historyCer in historyCertificateDeatailsUpdate)
            {
                var newHistoryCertificateDetail = new HistoryCertificateDTO(new HistoryCertificateDetails(historyCer));
                historyCertificateDeatailsList.Add(newHistoryCertificateDetail);
            }

            serviceResponse.Data = historyCertificateDeatailsList;
            serviceResponse.Amount = historyCertificateDeatailsUpdate.Count();
            return serviceResponse;
        }
        public async Task<ServiceResponse<List<CertificateDetailsDTO>>> GetCustomerCertificates(double customerId)
        {
            _logger.Debug("GetCustomerCertificatesDetailsById");
            var serviceResponse = new ServiceResponse<List<CertificateDetailsDTO>>();
            var customerCertificateList = new List<CertificateDetailsDTO>();
            var certificates = _context.Certificates
                .Include(cer => cer.RelatedCertificateissuer)
                .Include(cer => cer.RelatedCertificatesstatus)
                .Include(cer => cer.RelatedCustomer).ThenInclude(cus => cus.RelatedSecurityquestion)
                .Include(cer => cer.RelatedCustomerIdentifier)
                .Include(cer => cer.RelatedDocsType)
                .Include(cer => cer.RelatedExpiration)
                .Include(cer => cer.RelatedIssuerPlace)
                .Include(cer => cer.RelatedProject)
                .Include(cer => cer.RelatedSecurityquestion)
                .Include(cer => cer.RelatedSmartObject)
                .Include(cer => cer.RelatedSubProject)
                .Where(cer => cer.Customerid == customerId).ToList();

            foreach (var cer in certificates)
            {
                var newCertificateDetail = new CertificateDetailsDTO(new CertificateDetails(cer));
                customerCertificateList.Add(newCertificateDetail);
            }
            serviceResponse.Data = customerCertificateList;
            return serviceResponse;
        }
        public async Task<ServiceResponse<List<CertificateDetailsDTO>>> SearchCertificates(CertificateAdvancedSearch certificateAdvancedSearch,
            int skip, int take)
        {
            _logger.Debug("SearchCertificates");
            var serviceRespone = new ServiceResponse<List<CertificateDetailsDTO>>();
            var certificatesIds = _context.Certificates.Include(cer => cer.RelatedCertificateissuer)
                .Include(cer => cer.RelatedCustomer)
                .Where(cer =>
            ((certificateAdvancedSearch.Company == null) || cer.Company.Contains(certificateAdvancedSearch.Company))
            && ((certificateAdvancedSearch.HpNumber == null) || EF.Functions.Like(cer.Hpnumber, $"%{certificateAdvancedSearch.HpNumber}%"))
            && ((certificateAdvancedSearch.Project == null) || cer.Project == certificateAdvancedSearch.Project)
            && ((certificateAdvancedSearch.SubProject == null) || cer.Subproject == certificateAdvancedSearch.SubProject)
            && ((certificateAdvancedSearch.CustomerIdNumber.CompareTo(0) == 0) || Convert.ToDouble(cer.RelatedCustomer.Idnumber) == certificateAdvancedSearch.CustomerIdNumber)
            && ((certificateAdvancedSearch.CertificateStatus.CompareTo(-1) == 0) || cer.Certificatestatus == certificateAdvancedSearch.CertificateStatus)
            && ((certificateAdvancedSearch.CertificateIssuer.CompareTo(-1) == 0) || cer.Certificateissuer == certificateAdvancedSearch.CertificateIssuer)
            && ((certificateAdvancedSearch.CustomerIdentifier.CompareTo(-1) == 0) || cer.Identify == certificateAdvancedSearch.CustomerIdentifier)
            && (certificateAdvancedSearch.StartExpDate == null || cer.Expiredate.Value >= certificateAdvancedSearch.StartExpDate.Value)
            && (certificateAdvancedSearch.EndExpDate == null || cer.Expiredate.Value <= certificateAdvancedSearch.EndExpDate.Value)
            && (certificateAdvancedSearch.StartIssueDate == null || cer.Issuedate.Value >= certificateAdvancedSearch.StartIssueDate.Value)
            && (certificateAdvancedSearch.EndIssueDate == null || cer.Issuedate.Value <= certificateAdvancedSearch.EndIssueDate.Value)
            && (certificateAdvancedSearch.CustomerName == null || EF.Functions.Like(cer.RelatedCustomer.Firstname, $"%{certificateAdvancedSearch.CustomerName}%"))
            && (certificateAdvancedSearch.CustomerLastName == null || EF.Functions.Like(cer.RelatedCustomer.Lastname, $"%{certificateAdvancedSearch.CustomerLastName}%"))
            ).Select(x => x.Id).ToHashSet();

            serviceRespone.Amount = certificatesIds.Count();
            var certificatesIdList = certificatesIds.Skip(skip).Take(take).ToList();
            _logger.Debug("Start converting the certificates to certificateDetails object");
            var certificateDetailsList = new List<CertificateDetailsDTO>();
            foreach (Double cerId in certificatesIdList)
            {
                certificateDetailsList.Add(new CertificateDetailsDTO(GenerateCertificateDetailsFromId(cerId)));
            }
            serviceRespone.Data = certificateDetailsList;
            return serviceRespone;
        }
        public async Task<ServiceResponse<bool>> CheckSecurityAnswer(int cerId, string secAns, int question)
        {
            _logger.Debug("CheckSecurityAnswer");
            var ServiceResponse = new ServiceResponse<bool>();
            secAns = EncryptDecryptHandler.encryptSecurityAns(secAns);

            var certificate = _context.Certificates.Include(cer => cer.RelatedCustomer)
                .Where(cer => cer.Id == cerId).ToListAsync().Result.FirstOrDefault();

            var certificateSecurityAnswerMatches = certificate.Securityansware.Equals(secAns)
                && certificate.Securityquestion == question;
            var customerSecurityAnswerMatches = certificate.RelatedCustomer.Securityansware.Equals(secAns)
                && certificate.RelatedCustomer.Securityquestion == question;

            var foundMatch = certificateSecurityAnswerMatches || customerSecurityAnswerMatches;

            if (!foundMatch)
            {
                // Checking in all other customer's certificates.
                _logger.Debug("Checking security answer in all customer's certificates");
                var customersCertificates = await _context.Certificates.Where(cer => (cer.Customerid == certificate.Customerid)).ToListAsync();
                foreach (Certificate cer in customersCertificates)
                {
                    if (cer.Securityansware.Equals(secAns) && cer.Securityquestion == question)
                    {
                        foundMatch = true;
                        break;
                    }
                }
            }
            ServiceResponse.Data = foundMatch;
            return ServiceResponse;
        }
        public async Task<ServiceResponse<int>> UpdateExpiredCertificates()
        {
            _logger.Debug("UpdateExpiredCertificates");
            var cert = _context.Certificatesstatuses.FirstOrDefault(cer => cer.Title.Equals("פג תוקף"));
            var serviceRespone = new ServiceResponse<int>();
            if (cert != null)
            {
                var certificateStatus = cert.Id;
                var now = DateTime.Now.ToLocalTime();
                var expiredCertificates = await _context.Certificates.OrderBy(x => x.Expiredate).Where(x => x.Expiredate < now).ToListAsync();
                foreach (Certificate c in expiredCertificates)
                {
                    c.Certificatestatus = certificateStatus;
                    _context.Certificates.Update(c);
                }
                try
                {
                    _context.SaveChanges();
                    serviceRespone.Data = expiredCertificates.Count();
                    serviceRespone.Message = "Updated expired certificates successfully";
                }
                catch (Exception ex)
                {
                    _logger.Error("UpdateExpiredCertificates error, " + ex.Message);
                    serviceRespone.Data = -1;
                    serviceRespone.Message = "Updating expired certificates failed. " + ex;
                }
            }
            return serviceRespone;
        }
        public async Task<ServiceResponse<int>> AddCertificate(Certificate certificate)
        {
            _logger.Debug("AddCertificate");
            var serviceRespone = new ServiceResponse<int>();
            certificate.Issuedate = certificate.Issuedate.Value.ToLocalTime();
            certificate.Expiredate = certificate.Expiredate.Value.ToLocalTime();
            lock (_locker)
            {
                //certificate.Id = GenerateCertificateId();
                certificate.Id = 0;
                certificate.Securityansware = EncryptDecryptHandler.encryptSecurityAns(certificate.Securityansware);

                var newCertificateInDb = _context.Certificates.Add(certificate);
                try
                {
                    _context.SaveChanges();
                    serviceRespone.Data = newCertificateInDb.Entity.Id;
                    _cash.Increment(CashType.Certificate);
                    return serviceRespone;
                }
                catch (Exception exception)
                {
                    _logger.Debug("exception detected while trying to AddCertificate - " + exception);
                    serviceRespone.Success = false;
                    serviceRespone.Message = $"Registration failed. {exception}";
                    serviceRespone.Data = -1;
                    return serviceRespone;
                }
            }
        }

    }
}