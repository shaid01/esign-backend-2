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
        private static object _locker = new object();
        private static CertificateCashHandler _certificateCash;

        public CertificatesService(AppDbContext context, ILogger logger)
        {
            _context = context;
            _logger = logger;
            _certificateCash = CertificateCashHandler.GetInstance();
        }

        private List<CertificateDetails> GenerateCertificateDetailsToCustomer(double customerId)
        {
            _logger.Debug("GenerateCertificateDetailsFromId");
            var certificateDeatailsList = new List<CertificateDetails>();

            var matchedCertificates = _context.Certificates
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
                .Where(cer => Convert.ToDouble(cer.RelatedCustomer.Idnumber).Equals(customerId)).ToList();

            foreach (var matchedCertificate in matchedCertificates)
            {
                var certificate = new CertificateDetails(matchedCertificate);
                certificateDeatailsList.Add(certificate);
            }
            return certificateDeatailsList;
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

        // NEED TO ADD LOCK
        private int GenerateCertificateId()
        {
            _logger.Debug("GenerateCertificateId");
            int maxId = _context.Certificates.OrderByDescending(cer => cer.Id).Take(1).ToList()[0].Id;
            return maxId + 1;
        }
        // NEED TO ADD LOCK
        private int GenerateHistoryCertificateId()
        {
            _logger.Debug("GenerateHistoryCertificateId");

            int maxId = 0;
            try
            {
                maxId = _context.Certificateshistories.OrderByDescending(item => item.Id).Take(1).ToList()[0].Id;
            }
            catch (Exception e)
            {

            }
            return maxId + 1;
        }

        // NEED TO REMOVE AND CHECK IF ITS IN USE IN FRONT
        public async Task<ServiceResponse<int>> GetAmountOfCertificates()
        {
            _logger.Debug("GetAmountOfCertificates");

            var serviceResponse = new ServiceResponse<int>();
            serviceResponse.Data = _context.Certificates.Count();
            return serviceResponse;
        }

        // NEED TO FIX. NOT WORKING.
        public async Task<ServiceResponse<int>> UpdateCertificate(Certificate updatedCertificate)
        {
            _logger.Debug("UpdateCertificate");

            var serviceResponse = new ServiceResponse<int>();
            //updatedCertificate.Securityansware = encryptSecurityAns(updatedCertificate.Securityansware);
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
            //serviceResponse.Amount = _certificateCash.Counter;
            serviceResponse.Amount = _context.Certificates.Count();
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
            //serviceResponse.Message = serviceResponse.Data.Count().ToString();
            return serviceResponse;
        }



        // NEED TO UPGRADE
        public async Task<ServiceResponse<int>> AddNewHistoryCertificate(Certificateshistory certificateshistory)
        {
            _logger.Debug("AddNewHistoryCertificate");

            var serviceRespone = new ServiceResponse<int>();

            lock (_locker)
            {
                certificateshistory.Id = GenerateHistoryCertificateId();
                //certificateshistory.Securityansware = encryptSecurityAns(certificateshistory.Securityansware);
                certificateshistory.Securityansware = EncryptDecryptHandler.encryptSecurityAns(certificateshistory.Securityansware);
                var newHistoryCertificateInDb = _context.Certificateshistories.Add(certificateshistory);
                try
                {
                    _context.SaveChangesAsync();
                    serviceRespone.Data = newHistoryCertificateInDb.Entity.Id;
                    serviceRespone.Message = "Certificateshistory addedd successfully!";
                    _logger.Debug("Certificate added successfully");
                    return serviceRespone;
                }
                catch (Exception exception)
                {
                    serviceRespone.Success = false;
                    serviceRespone.Message = $"Adding new Certificateshistory failed. {exception}";
                    serviceRespone.Data = -1;
                    _logger.Error("Exception detected: " + exception);
                    return serviceRespone;
                }
            }
        }

        // NEED TO UPGRADE
        public async Task<ServiceResponse<List<HistoryCertificateDetails>>> GetHistoryCertificates(double certificateId)
        {
            _logger.Debug("GetHistoryCertificates");

            var serviceResponse = new ServiceResponse<List<HistoryCertificateDetails>>();
            var certificateDeatailsList = new List<HistoryCertificateDetails>();

            var historyCertificateDeatails = (from historyCer in _context.Certificateshistories.Where
                                              (el => EF.Functions.Like(el.Certificateid.ToString(), certificateId.ToString()))

                                              join pro in _context.Projects
                                              on historyCer.Project equals pro.Id

                                              join subpro in _context.Subprojects
                                              on historyCer.Subproject equals subpro.Id

                                              join expire in _context.Expirationtypes
                                              on historyCer.Expire equals expire.Id

                                              join smartObject in _context.Smartobjects
                                              on historyCer.Smartobject equals smartObject.Id

                                              join cerStatus in _context.Certificatesstatuses
                                              on historyCer.Certificatestatus equals cerStatus.Id

                                              join customer in _context.Customers
                                              on historyCer.Customerid equals Convert.ToDouble(customer.Idnumber)

                                              join docType in _context.Docstypes
                                              on historyCer.Docstype equals docType.Id

                                              join users in _context.Buusers
                                              on historyCer.Updateduserid equals users.Id

                                              select new
                                              {
                                                  ID = historyCer.Id,
                                                  Company = historyCer.Company,
                                                  Hpnumber = historyCer.Hpnumber,
                                                  Email = historyCer.Email,
                                                  Passportid = historyCer.Passportid,
                                                  Licenseid = historyCer.Licenceid,
                                                  Signer = historyCer.Hotem,
                                                  Securityquestion = historyCer.Securityquestion,
                                                  Securityanswer = historyCer.Securityansware,
                                                  Remarks = historyCer.Remarks,
                                                  Remarkdesc = historyCer.Remarksdesc,
                                                  Issuedate = historyCer.Issuedate,
                                                  Expiredate = historyCer.Expiredate,
                                                  UpdatedDate = historyCer.Updateddate,
                                                  Project = pro,
                                                  SubProject = subpro,
                                                  Expire = expire,
                                                  SmartObject = smartObject,
                                                  CertificateStatus = cerStatus,
                                                  CustomerId = customer.Idnumber,
                                                  CustomerName = $"{customer.Firstname}" + $" {customer.Lastname}",
                                                  DocsType = docType,
                                                  Certificateissuer = historyCer.Certificateissuer,
                                                  CertificateLocation = historyCer.Issuerplace,
                                                  CustomerIdentifier = historyCer.Identify,
                                                  CustomerIdentifierId = historyCer.Customerid,
                                                  UpdatedUserName = users.Firstname + " " + users.Lastname
                                              }).ToList();

            foreach (var certificateDetail in historyCertificateDeatails)
            {
                var newCd = new HistoryCertificateDetails();

                newCd.Id = certificateDetail.ID;
                newCd.Company = certificateDetail.Company;
                newCd.Hpnumber = certificateDetail.Hpnumber;
                newCd.Email = certificateDetail.Email;
                newCd.Passportid = certificateDetail.Passportid;
                newCd.Licenseid = certificateDetail.Licenseid;
                newCd.Signer = certificateDetail.Signer;
                newCd.Securityquestion = (double)certificateDetail.Securityquestion;
                newCd.Securityanswer = certificateDetail.Securityanswer;
                newCd.Remarks = certificateDetail.Remarks;
                newCd.Remarkdesc = certificateDetail.Remarkdesc;
                newCd.Issuedate = (DateTime)certificateDetail.Issuedate;
                newCd.Expiredate = (DateTime)certificateDetail.Expiredate;
                newCd.Project = certificateDetail.Project;
                newCd.SubProject = certificateDetail.SubProject;
                newCd.Expire = certificateDetail.Expire;
                newCd.Smartobject = certificateDetail.SmartObject;
                newCd.Certificatesstatus = certificateDetail.CertificateStatus;
                newCd.CustomerId = certificateDetail.CustomerId;
                newCd.CustomerName = certificateDetail.CustomerName;
                newCd.Docstype = certificateDetail.DocsType;
                newCd.CertificateIssuer = certificateDetail.Certificateissuer;
                newCd.CertificateLocation = certificateDetail.CertificateLocation;
                newCd.CustomerIdentifier = certificateDetail.CustomerIdentifier;
                newCd.UpdatedUserName = certificateDetail.UpdatedUserName;
                newCd.UpdatedDate = (DateTime)certificateDetail.UpdatedDate;
                certificateDeatailsList.Add(newCd);
            }

            serviceResponse.Data = certificateDeatailsList;
            serviceResponse.Message = serviceResponse.Data.Count().ToString();
            return serviceResponse;
        }



        public async Task<ServiceResponse<List<CertificateDetails>>> GetCustomerCertificatesDetails(double customerId)
        {
            _logger.Debug("GetCustomerCertificatesDetails");
            var serviceResponse = new ServiceResponse<List<CertificateDetails>>();
            serviceResponse.Data = GenerateCertificateDetailsToCustomer(customerId).OrderBy(x => x.Id).ToList();
            serviceResponse.Amount = serviceResponse.Data.Count();
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
            && ((certificateAdvancedSearch.CustomerIdNumber.CompareTo(0) == 0) || cer.Customerid == certificateAdvancedSearch.CustomerIdNumber)
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
           //secAns = encryptSecurityAns(secAns);
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
            var serviceRespone = new ServiceResponse<int>();
            var now = DateTime.Now.ToLocalTime();
            var expiredCertificates = await _context.Certificates.OrderBy(x => x.Expiredate).Where(x => x.Expiredate < now).ToListAsync();
            foreach (Certificate c in expiredCertificates)
            {
                c.Certificatestatus = 3;
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
                certificate.Id = GenerateCertificateId();
                //certificate.Securityansware = encryptSecurityAns(certificate.Securityansware);
                certificate.Securityansware = EncryptDecryptHandler.encryptSecurityAns(certificate.Securityansware);
                
                var newCertificateInDb = _context.Certificates.Add(certificate);
                try
                {
                    _context.SaveChanges();
                    serviceRespone.Data = newCertificateInDb.Entity.Id;
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








        /// <summary>
        /// TO REMOVE
        /// </summary>
        /// <param name="plainSecurityAns"></param>
        /// <returns></returns>


        // NEED TO REMOVE AND REFERECNCE
        private static string encryptSecurityAns(String plainSecurityAns)
        {
            return EncryptDecryptHandler.encryptSecurityAns(plainSecurityAns);
        }

        // NEED TO REMOVE AND REFERECNCE
        public static string decryptSecurityAnswer(String encryptedSecurityAns)
        {
            return EncryptDecryptHandler.decryptSecurityAns(encryptedSecurityAns);
        }

        // NEED TO REMOVE. 
        private List<CertificateDetails> GenerateCertificateDetailsToCustomerUpdate(double customerId)
        {
            _logger.Debug("GenerateCertificateDetailsToCustomer");

            var customerIden = _context.Customers.Where(x => Convert.ToDouble(x.Idnumber).Equals(customerId)).Select(x => x.Id).FirstOrDefault();

            var certificateDeatailsList = new List<CertificateDetails>();
            var certificateDeatails = (from cer in _context.Certificates
                                       .Where(el => el.Customerid == customerIden)

                                       join pro in _context.Projects
                                       on cer.Project equals pro.Id

                                       join subpro in _context.Subprojects
                                       on cer.Subproject equals subpro.Id

                                       join expire in _context.Expirationtypes
                                       on cer.Expire equals expire.Id

                                       join smartObject in _context.Smartobjects
                                       on cer.Smartobject equals smartObject.Id

                                       join cerStatus in _context.Certificatesstatuses
                                       on cer.Certificatestatus equals cerStatus.Id

                                       join customer in _context.Customers
                                       on customerId equals Convert.ToDouble(customer.Idnumber)

                                       join docType in _context.Docstypes
                                       on cer.Docstype equals docType.Id

                                       join cerIssuer in _context.Isscerts
                                       on cer.Certificateissuer equals cerIssuer.Id

                                       join cerLocation in _context.Issplaces
                                       on cer.Issuerplace equals cerLocation.Id

                                       select new
                                       {
                                           ID = cer.Id,
                                           Company = cer.Company,
                                           Hpnumber = cer.Hpnumber,
                                           Email = cer.Email,
                                           Passportid = cer.Passportid,
                                           Licenseid = cer.Licenceid,
                                           Signer = cer.Hotem,
                                           Securityquestion = cer.Securityquestion,
                                           Securityanswer = cer.Securityansware,
                                           Remarks = cer.Remarks,
                                           Remarkdesc = cer.Remarksdesc,
                                           Job = cer.Job,
                                           Issuedate = cer.Issuedate,
                                           Expiredate = cer.Expiredate,
                                           Project = pro,
                                           SubProject = subpro,
                                           Expire = expire,
                                           SmartObject = smartObject,
                                           CertificateStatus = cerStatus,
                                           CustomerId = customer.Idnumber,
                                           CustomerName = $"{customer.Firstname}" + $" {customer.Lastname}",
                                           DocsType = docType,
                                           Certificateissuer = cerIssuer,
                                           CertificateLocation = cerLocation,
                                           CustomerIdentifierId = cer.Identify
                                       }).ToList();

            foreach (var certificateDetail in certificateDeatails)
            {
                // Fetching customer identifier.
                var identifier = _context.Custidents.Where(x => x.Id == certificateDetail.CustomerIdentifierId).FirstOrDefault();

                var newCd = new CertificateDetails();
                // Original fields.
                newCd.Id = certificateDetail.ID;
                newCd.Company = certificateDetail.Company;
                newCd.Hpnumber = certificateDetail.Hpnumber;
                newCd.Email = certificateDetail.Email;
                newCd.Passportid = certificateDetail.Passportid;
                newCd.Licenseid = certificateDetail.Licenseid;
                newCd.Signer = certificateDetail.Signer;
                newCd.Securityquestion = (double)certificateDetail.Securityquestion;
                //newCd.Securityanswer = certificateDetail.Securityanswer;
                //newCd.Securityanswer = decryptSecurityAnswer(certificateDetail.Securityanswer);
                newCd.Securityanswer = EncryptDecryptHandler.decryptSecurityAns(certificateDetail.Securityanswer);                
                newCd.Remarks = certificateDetail.Remarks;
                newCd.Remarkdesc = certificateDetail.Remarkdesc;
                newCd.Job = certificateDetail.Job;
                newCd.Issuedate = (DateTime)certificateDetail.Issuedate;
                newCd.Expiredate = (DateTime)certificateDetail.Expiredate;
                // Tables join fields.
                newCd.Project = certificateDetail.Project;
                newCd.SubProject = certificateDetail.SubProject;
                newCd.Expire = certificateDetail.Expire;
                newCd.Smartobject = certificateDetail.SmartObject;
                newCd.Certificatesstatus = certificateDetail.CertificateStatus;
                newCd.CustomerId = certificateDetail.CustomerId;
                newCd.CustomerName = certificateDetail.CustomerName;
                newCd.Docstype = certificateDetail.DocsType;
                newCd.CertificateIssuer = certificateDetail.Certificateissuer;
                newCd.CertificateLocation = certificateDetail.CertificateLocation;
                newCd.CustomerIdentifier = identifier.Title;
                newCd.CustomerIdentifierId = identifier.Id;

                certificateDeatailsList.Add(newCd);
            }
            return certificateDeatailsList;
        }

        // NEED TO REMOVE
        public void encryptSecurityAnswers()
        {
            foreach (Certificate cer in _context.Certificates)
            {
                Console.Out.WriteLine("Certificate id: " + cer.Securityansware + " ");
                //var encrypted = encryptSecurityAns(cer.Securityansware);
                var encrypted = EncryptDecryptHandler.encryptSecurityAns(cer.Securityansware);
                var len = encrypted.Length.ToString();
                // cer.Securityansware ="111111111122222222223333333333444444444455555555551111111111";
                cer.Securityansware = encrypted;
                _context.Certificates.Update(cer);

            }
            try
            {
                _context.SaveChanges();
            }
            catch (Exception e)
            {

            }

        }

        // NEED TO REMOVE
        public async Task<ServiceResponse<List<CertificateDetails>>> GetCertificatesDetailsUpdate(int skip, int take)
        {

            // encryptSecurityAnswers();

            _logger.Debug("GetCertificatesDetails");

            var serviceResponse = new ServiceResponse<List<CertificateDetails>>();
            var certificateDetailsList = new List<CertificateDetails>();
            var certificateDetails = (from cer in _context.Certificates.Skip(skip).Take(take)

                                      join pro in _context.Projects
                                      on cer.Project equals pro.Id

                                      join subpro in _context.Subprojects
                                      on cer.Subproject equals subpro.Id

                                      join expire in _context.Expirationtypes
                                      on cer.Expire equals expire.Id

                                      join smartObject in _context.Smartobjects
                                      on cer.Smartobject equals smartObject.Id

                                      join cerStatus in _context.Certificatesstatuses
                                      on cer.Certificatestatus equals cerStatus.Id

                                      join customer in _context.Customers
                                      on cer.Customerid equals customer.Id

                                      join docType in _context.Docstypes
                                      on cer.Docstype equals docType.Id

                                      join cerIssuer in _context.Isscerts
                                      on cer.Certificateissuer equals cerIssuer.Id

                                      join cerIssuer1 in _context.Isscerts
                                     on cer.Identify equals cerIssuer1.Id

                                      join cerLocation in _context.Issplaces
                                      on cer.Issuerplace equals cerLocation.Id

                                      select new
                                      {
                                          ID = cer.Id,
                                          Company = cer.Company,
                                          Hpnumber = cer.Hpnumber,
                                          Email = cer.Email,
                                          Passportid = cer.Passportid,
                                          Licenseid = cer.Licenceid,
                                          Signer = cer.Hotem,
                                          Securityquestion = cer.Securityquestion,
                                          Securityanswer = cer.Securityansware,
                                          Remarks = cer.Remarks,
                                          Remarkdesc = cer.Remarksdesc,
                                          Job = cer.Job,
                                          Issuedate = cer.Issuedate,
                                          Expiredate = cer.Expiredate,
                                          Project = pro,
                                          SubProject = subpro,
                                          Expire = expire,
                                          SmartObject = smartObject,
                                          CertificateStatus = cerStatus,
                                          CustomerId = customer.Idnumber,
                                          CustomerName = $"{customer.Firstname}" + $" {customer.Lastname}",
                                          DocsType = docType,
                                          Certificateissuer = cerIssuer,
                                          CertificateLocation = cerLocation,
                                          CustomerIdentifierId = cer.Identify,
                                          Identifier = cerIssuer1

                                      }).ToList();

            foreach (var certificateDetail in certificateDetails)
            {
                // Fetching customer identifier.


                var newCertificateDetail = new CertificateDetails();
                // Original fields.
                newCertificateDetail.Id = certificateDetail.ID;
                newCertificateDetail.Company = certificateDetail.Company;
                newCertificateDetail.Hpnumber = certificateDetail.Hpnumber;
                newCertificateDetail.Email = certificateDetail.Email;
                newCertificateDetail.Passportid = certificateDetail.Passportid;
                newCertificateDetail.Licenseid = certificateDetail.Licenseid;
                newCertificateDetail.Signer = certificateDetail.Signer;
                newCertificateDetail.Securityquestion = (double)certificateDetail.Securityquestion;
                //newCertificateDetail.Securityanswer = decryptSecurityAnswer(certificateDetail.Securityanswer);
                newCertificateDetail.Securityanswer = EncryptDecryptHandler.decryptSecurityAns(certificateDetail.Securityanswer);                
                //newCd.Securityanswer = certificateDetail.Securityanswer;
                newCertificateDetail.Remarks = certificateDetail.Remarks;
                newCertificateDetail.Remarkdesc = certificateDetail.Remarkdesc;
                newCertificateDetail.Job = certificateDetail.Job;
                newCertificateDetail.Issuedate = (DateTime)certificateDetail.Issuedate;
                newCertificateDetail.Expiredate = (DateTime)certificateDetail.Expiredate;

                // Tables join fields.
                newCertificateDetail.Project = certificateDetail.Project;
                newCertificateDetail.SubProject = certificateDetail.SubProject;
                newCertificateDetail.Expire = certificateDetail.Expire;
                newCertificateDetail.Smartobject = certificateDetail.SmartObject;
                newCertificateDetail.Certificatesstatus = certificateDetail.CertificateStatus;
                newCertificateDetail.CustomerId = certificateDetail.CustomerId;
                newCertificateDetail.CustomerName = certificateDetail.CustomerName;
                newCertificateDetail.Docstype = certificateDetail.DocsType;
                newCertificateDetail.CertificateIssuer = certificateDetail.Certificateissuer;
                newCertificateDetail.CertificateLocation = certificateDetail.CertificateLocation;
                newCertificateDetail.CustomerIdentifier = certificateDetail.Identifier.Title;
                newCertificateDetail.CustomerIdentifierId = certificateDetail.Identifier.Id;

                certificateDetailsList.Add(newCertificateDetail);
            }
            serviceResponse.Data = certificateDetailsList;
            serviceResponse.Message = serviceResponse.Data.Count().ToString();
            return serviceResponse;
        }
    }
}




