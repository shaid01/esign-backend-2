using EsignBackend.Extensions.CacheHandlers;
using EsignBackend.Extensions.EncryptDecrypt;
using EsignBackend.Models;
using EsignBackend.Models.DTOs;
using EsignBackend.Models.Tools;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using ClosedXML.Excel;
using System.IO;
using System.Collections;
using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.ExtendedProperties;
using DocumentFormat.OpenXml.InkML;
using System.Runtime.ConstrainedExecution;
using DocumentFormat.OpenXml.Bibliography;

namespace EsignBackend.Services.MainServices.Certificates
{
    public static class Extensions
    {
        public static IQueryable<Certificate> SearchCertificates(this IAppDbContext context)
        {
            return context.Certificates.AsNoTracking().Include(cer => cer.RelatedCertificateissuer)
                    .Include(cer => cer.RelatedCertificateissuer)
                    .Include(cer => cer.RelatedCertificatesstatus)
                    .Include(cer => cer.RelatedCustomer)
                    .Include(cer => cer.RelatedCustomerIdentifier)
                    .Include(cer => cer.RelatedDocsType)
                    .Include(cer => cer.RelatedExpiration)
                    .Include(cer => cer.RelatedIssuerPlace)
                    .Include(cer => cer.RelatedProject)
                    .Include(cer => cer.RelatedSmartObject)
                    .Include(cer => cer.RelatedSubProject);
        }
    }

    public class CertificatesService : ICertificatesService
    {
        private readonly AppDbContext _context;
        private readonly ILogger _logger;
        private readonly ICache _cash;
        private static object _locker = new object();
        private const int UNLIMITED = -1;
        private IServiceScopeFactory _scopeFactory;

        public CertificatesService(AppDbContext context, ILogger logger, ICache cash, IServiceScopeFactory scopeFactory)
        {
            _context = context;
            _logger = logger;
            _cash = cash;
            _scopeFactory = scopeFactory;
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

        public async Task<CertificateDetailsExtendedDTO> GetCertificateExtendedDetails(int id)
        {
            _logger.Debug("GetCertificatesDetails");

            var serviceResponse = new ServiceResponse<CertificateDetailsDTO>();
            serviceResponse.Amount = _cash.GetCounterByType(CacheType.Certificate);
            var certificate = _context.Certificates
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
                .Where(x => x.Id == id)
                .First();


            var newCertificateDetail = new CertificateDetailsExtendedDTO(new CertificateDetails(certificate));
            return newCertificateDetail;
        }

        public async Task<ServiceResponse<List<CertificateDetailsDTO>>> GetCertificatesDetails(int skip, int take)
        {
            _logger.Debug("GetCertificatesDetails");

            var serviceResponse = new ServiceResponse<List<CertificateDetailsDTO>>();

            //var query = _context.Certificates
            //    .Include(cer => cer.RelatedCertificateissuer)
            //    .Include(cer => cer.RelatedCertificatesstatus)
            //    .Include(cer => cer.RelatedCustomer)//.ThenInclude(cus => cus.RelatedSecurityquestion)
            //    .Include(cer => cer.RelatedCustomerIdentifier)
            //    .Include(cer => cer.RelatedDocsType)
            //    .Include(cer => cer.RelatedExpiration)
            //    .Include(cer => cer.RelatedIssuerPlace)
            //    .Include(cer => cer.RelatedProject)
            //    //  .Include(cer => cer.RelatedSecurityquestion)
            //    .Include(cer => cer.RelatedSmartObject)
            //    .Include(cer => cer.RelatedSubProject).AsNoTracking();

            //query = query.Skip(skip);
            //if (take != UNLIMITED)
            //{
            //    query = query.Take(take);
            //}
            //var certificates = await query.ToListAsync();

            List<Certificate> certificates = null;

            try
            {
                certificates = await _context.Certificates
                    .Include(cer => cer.RelatedCertificateissuer)
                    .Include(cer => cer.RelatedCertificatesstatus)
                    .Include(cer => cer.RelatedCustomer)//.ThenInclude(cus => cus.RelatedSecurityquestion)
                    .Include(cer => cer.RelatedCustomerIdentifier)
                    .Include(cer => cer.RelatedDocsType)
                    .Include(cer => cer.RelatedExpiration)
                    .Include(cer => cer.RelatedIssuerPlace)
                    .Include(cer => cer.RelatedProject)
                    //  .Include(cer => cer.RelatedSecurityquestion)
                    .Include(cer => cer.RelatedSmartObject)
                    .Include(cer => cer.RelatedSubProject)
                    .AsNoTracking()

                    .OrderByDescending(x => x.Issuedate)

                    .Skip(skip).Take(take).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.Error($"Error while processing DB query in GetCertificatesDetails. {ex.Message}");
                var errorData = new ServiceResponse<List<CertificateDetailsDTO>>();
                errorData.Data = null;
                errorData.Success = false;
                errorData.Message = ex.Message;

                return errorData;
            }

            var certificateDetailsList = new List<CertificateDetailsDTO>();

            foreach (var cer in certificates)
            {
                var newCertificateDetail = new CertificateDetailsDTO(new CertificateDetails(cer));
                certificateDetailsList.Add(newCertificateDetail);
            }

            serviceResponse.Data = certificateDetailsList;
            serviceResponse.Amount = _cash.GetCounterByType(CacheType.Certificate);

            return serviceResponse;
        }

        public async Task<ServiceResponse<IEnumerable<CertificateDetailsDTO>>> SearchCertificates(CertificateAdvancedSearch certificateAdvancedSearch, int skip, int take)
        {
            _logger.Debug("SearchCertificates");
            var serviceRespone = new ServiceResponse<IEnumerable<CertificateDetailsDTO>>();

            var searchQry = _context.SearchCertificates();

            var query = searchQry.AsNoTracking().Where(cer =>

                   ((certificateAdvancedSearch.Company == null) || cer.Company.Contains(certificateAdvancedSearch.Company))

                && ((certificateAdvancedSearch.HpNumber == null) || EF.Functions.Like(cer.Hpnumber, $"%{certificateAdvancedSearch.HpNumber}%"))

                && ((certificateAdvancedSearch.Project.CompareTo(-1) == 0) || (cer.RelatedProject != null && cer.RelatedProject.Id == certificateAdvancedSearch.Project))

                && ((certificateAdvancedSearch.SubProject.CompareTo(-1) == 0) || (cer.RelatedSubProject != null && cer.RelatedSubProject.Id == certificateAdvancedSearch.SubProject))

                && (string.IsNullOrWhiteSpace(certificateAdvancedSearch.CustomerIdNumber) || (cer.RelatedCustomer != null && cer.RelatedCustomer.Idnumber.Trim() == certificateAdvancedSearch.CustomerIdNumber.ToString().Trim()))

                && ((certificateAdvancedSearch.CertificateStatus.CompareTo(-1) == 0) || (cer.RelatedCertificatesstatus != null && cer.RelatedCertificatesstatus.Id == certificateAdvancedSearch.CertificateStatus))

                ////no RelatedCertIssuer model was found?
                && ((certificateAdvancedSearch.CertificateIssuer.CompareTo(-1) == 0) || cer.Certificateissuer == certificateAdvancedSearch.CertificateIssuer)

                && ((certificateAdvancedSearch.CustomerIdentifier.CompareTo(-1) == 0) || (cer.RelatedCustomerIdentifier != null && cer.RelatedCustomerIdentifier.Id == certificateAdvancedSearch.CustomerIdentifier))

                && (certificateAdvancedSearch.StartExpDate == null || cer.Expiredate.Value >= certificateAdvancedSearch.StartExpDate.Value)

                && (certificateAdvancedSearch.EndExpDate == null || cer.Expiredate.Value <= certificateAdvancedSearch.EndExpDate.Value)

                && (certificateAdvancedSearch.StartIssueDate == null || cer.Issuedate.Value >= certificateAdvancedSearch.StartIssueDate.Value)

                && (certificateAdvancedSearch.EndIssueDate == null || cer.Issuedate.Value <= certificateAdvancedSearch.EndIssueDate.Value)

                && (string.IsNullOrWhiteSpace(certificateAdvancedSearch.CustomerName) || EF.Functions.Like(cer.RelatedCustomer != null ? cer.RelatedCustomer.Firstname : string.Empty, $"%{certificateAdvancedSearch.CustomerName}%"))

                && (string.IsNullOrWhiteSpace(certificateAdvancedSearch.CustomerLastName) || EF.Functions.Like(cer.RelatedCustomer != null ? cer.RelatedCustomer.Lastname : string.Empty, $"%{certificateAdvancedSearch.CustomerLastName}%"))

                && (certificateAdvancedSearch.IssuerPlace.CompareTo(-1) == 0 || (cer.RelatedIssuerPlace != null && cer.RelatedIssuerPlace.Id == certificateAdvancedSearch.IssuerPlace)));

            //.AsNoTracking().Skip(skip).Take(take); //right place

            List<Certificate> certificatesList = null;

            //if (skip == 0)
            {
                //https://stackoverflow.com/questions/63071963/ef-core-queryablet-count-returns-different-number-than-queryablet-tolist

                /*
                If you create your database on your own however (using a custom crafted SQL script) and leave out the foreign key constraint,
                but still let EF Core believe that there is one in place, and then violate the referential integrity by using a non existing ID
                in a foreign key column, you can get different results for database-side (here 291) and client-side (here 287) count operations
                */

                /*
                 * The reason of fewer records was this row "INNER JOIN [projects] AS [p] ON [t].[project] = [p].[id]" instead of "LEFT JOIN"
                 * Fixed in Certificate class -> public int? Project { get; set; } (? sign of nullable value was added)
                 */

                //server side count - 291, e.g.
                /*
                    SELECT COUNT(*)
                          FROM [certificates] AS [c]
                          LEFT JOIN [customers] AS [c0] ON [c].[customerid] = [c0].[id]
                          WHERE CASE
                              WHEN [c0].[id] IS NOT NULL THEN [c0].[firstname]
                              ELSE N''
                          END LIKE '%נועה%'

                Why not?:

                     SELECT COUNT(*)
                      FROM [certificates] AS [c]
                      INNER JOIN [customers] AS [c0] ON [c].[customerid] = [c0].[id]
                      WHERE [c0].[firstname] LIKE '%נועה%'
                */

                try
                {
                    serviceRespone.Amount = await query.CountAsync(); //291
                    //serviceRespone.Amount = (await query.ToListAsync()).Count(); //287

                    //client side count - 287 - some records with not existed values in referenced tables were eliminated
                    /*
                     * full select query
                     */
                    //serviceRespone.Amount = (await query.AsNoTracking().ToListAsync()).Count();

                    query = query.Skip(skip);

                    if (take != UNLIMITED)
                    {
                        query = query.Take(take);
                    }

                    query = query.OrderByDescending(cer => cer.Issuedate);

                    certificatesList = await query.ToListAsync();
                }
                catch (Exception ex)
                {
                    _logger.Error($"Error while processing DB query in SearchCertificates. {ex.Message}");
                    var errorData = new ServiceResponse<IEnumerable<CertificateDetailsDTO>>();
                    errorData.Data = null;
                    errorData.Success = false;
                    errorData.Message = ex.Message;

                    return errorData;
                }
            }
            //else
            //{
            //    query.AsNoTracking().Skip(skip).Take(take);

            //    // clients will be responsible for keeping count value
            //    //serviceRespone.Amount = -1;

            //    certificatesList = await query.ToListAsync();
            //    serviceRespone.Amount = certificatesList.Count;
            //}

            //certificatesList = await query.ToListAsync();

            var certificateDetailsDtoList = new List<CertificateDetailsDTO>();

            foreach (var certificate in certificatesList)
            {
                try
                {
                    certificateDetailsDtoList.Add(new CertificateDetailsDTO(new CertificateDetails(certificate)));
                }
                catch (Exception ex)
                {
                    _logger.Error($"Error in SearchCertificates. Cert id - {certificate.Id} - {ex.Message}");
                }
            }

            serviceRespone.Data = certificateDetailsDtoList;

            return serviceRespone;
        }

        public async Task<ServiceResponse<List<HistoryCertificateDTO>>> GetHistoryCertificates(double certificateId)
        {
            _logger.Debug("GetHistoryCertificates");

            var serviceResponse = new ServiceResponse<List<HistoryCertificateDTO>>();
            var historyCertificateDeatailsList = new List<HistoryCertificateDTO>();

            try
            {
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
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }

        public async Task<ServiceResponse<List<CertificateDetailsDTO>>> GetCustomerCertificates(double customerId)
        {
            _logger.Debug("GetCustomerCertificates");
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

        public async Task<ServiceResponse<bool>> CheckSecurityAnswer(int cerId, string secAns, int question)
        {
            _logger.Debug("CheckSecurityAnswer");
            var ServiceResponse = new ServiceResponse<bool>();
            secAns = EncryptDecryptHandler.encryptSecurityAns(secAns);

            var certificate = _context.Certificates.Include(cer => cer.RelatedCustomer)
                .Where(cer => cer.Id == cerId).ToListAsync().Result.FirstOrDefault();

            var certificateSecurityAnswerMatches = certificate.Securityansware.Equals(secAns)
                && certificate.Securityquestion == question;
            bool customerSecurityAnswerMatches = false;
            if (certificate.RelatedCustomer != null)
            {
                customerSecurityAnswerMatches = certificate.RelatedCustomer.Securityansware != null &&
                                                    certificate.RelatedCustomer.Securityansware.Equals(secAns) &&
                                                    certificate.RelatedCustomer.Securityquestion == question;
            }

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
            _logger.Debug("Hangfire Job - *************** UpdateExpiredCertificates ***************");
            var cert = _context.Certificatesstatuses.FirstOrDefault(cer => cer.Title.Equals("פג תוקף"));
            var serviceRespone = new ServiceResponse<int>();
            if (cert != null)
            {
                var certificateStatus = cert.Id;
                var now = DateTime.Now.ToLocalTime();
                var expiredCertificates = await _context.Certificates.OrderBy(x => x.Expiredate).Where(x => x.Expiredate < now && x.Certificatestatus != certificateStatus).ToListAsync();
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
                    serviceRespone.Success = false;
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
                    _cash.Increment(CacheType.Certificate);
                    return serviceRespone;
                }
                catch (Exception ex)
                {
                    _logger.Debug("exception detected while trying to AddCertificate - " + ex);
                    serviceRespone.Success = false;
                    serviceRespone.Message = $"Adding certificate failed. {ex}";
                    serviceRespone.Data = -1;
                    return serviceRespone;
                }
            }
        }

        public byte[] GenerateXlsxFile(IEnumerable<CertificateDetailsDTO> certificateDetails)
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Certificates");
                var currentRow = 1;
                worksheet.Cell(currentRow, 1).Value = "מספר מזהה";
                worksheet.Cell(currentRow, 2).Value = "סוג תעודה";
                worksheet.Cell(currentRow, 3).Value = "פרויקט";
                worksheet.Cell(currentRow, 4).Value = "תת פרויקט";
                worksheet.Cell(currentRow, 5).Value = "רכיב חכם";
                worksheet.Cell(currentRow, 6).Value = "סטאטוס תעודה";
                worksheet.Cell(currentRow, 7).Value = "מזהה לקוח";
                worksheet.Cell(currentRow, 8).Value = "שם לקוח";
                worksheet.Cell(currentRow, 9).Value = "מנפיק תעודה";
                worksheet.Cell(currentRow, 10).Value = "מיקום הנפקה";
                worksheet.Cell(currentRow, 11).Value = "מזהה לקוח ייחודי";
                worksheet.Cell(currentRow, 12).Value = "חברה";
                worksheet.Cell(currentRow, 13).Value = "מספר ח.פ";
                worksheet.Cell(currentRow, 14).Value = "אימייל";
                worksheet.Cell(currentRow, 15).Value = "מספר דרכון";
                worksheet.Cell(currentRow, 16).Value = "מספר רישיון";
                worksheet.Cell(currentRow, 17).Value = "שאלת אבטחה";
                worksheet.Cell(currentRow, 18).Value = "תשובת אבטחה";
                worksheet.Cell(currentRow, 19).Value = "הערות";
                worksheet.Cell(currentRow, 20).Value = "עבודה";
                worksheet.Cell(currentRow, 21).Value = "תאריך הנפקה";
                worksheet.Cell(currentRow, 22).Value = "תאריך תפוגה";
                foreach (var certificate in certificateDetails)
                {
                    if (!isCertificateValid(certificate))
                    {
                        throw new Exception("Suspected CSV injection");
                    }
                    currentRow++;
                    worksheet.Cell(currentRow, 1).Value = certificate.Id;
                    worksheet.Cell(currentRow, 2).Value = certificate.Docstype?.Title;
                    worksheet.Cell(currentRow, 3).Value = certificate.Project?.Title;
                    worksheet.Cell(currentRow, 4).Value = certificate.SubProject?.Title;
                    worksheet.Cell(currentRow, 5).Value = certificate.Smartobject?.Title;
                    worksheet.Cell(currentRow, 6).Value = certificate.Certificatesstatus?.Title;
                    worksheet.Cell(currentRow, 7).Value = certificate.RelatedCustomerIdentifier?.Title;
                    worksheet.Cell(currentRow, 8).Value = certificate.CustomerName;
                    worksheet.Cell(currentRow, 9).Value = certificate.CertificateIssuer?.Title;
                    worksheet.Cell(currentRow, 10).Value = certificate.CertificateLocation?.Title;
                    worksheet.Cell(currentRow, 11).Value = certificate.RelatedCustomerIdentifier?.Id;
                    worksheet.Cell(currentRow, 12).Value = certificate.Company;
                    worksheet.Cell(currentRow, 13).Value = certificate.Hpnumber;
                    worksheet.Cell(currentRow, 14).Value = certificate.Email;
                    worksheet.Cell(currentRow, 15).Value = certificate.Passportid;
                    worksheet.Cell(currentRow, 16).Value = certificate.Licenseid;
                    worksheet.Cell(currentRow, 17).Value = certificate.RelatedSecurityQuestion.Title;
                    worksheet.Cell(currentRow, 18).Value = certificate.Securityanswer;
                    worksheet.Cell(currentRow, 19).Value = certificate.Remarks;
                    worksheet.Cell(currentRow, 20).Value = certificate.Job;
                    worksheet.Cell(currentRow, 21).Value = certificate.Issuedate;
                    worksheet.Cell(currentRow, 22).Value = certificate.Expiredate;
                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return content;
                }
            }
        }

        #region Private Functions

        private List<Certificate> getCertificateDbList(int offset, int limit)
        {
            using var scope = _scopeFactory.CreateScope();
            {
                var dependencyService = scope.ServiceProvider.GetService<IAppDbContext>();
                var certificates = dependencyService.Certificates
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
                .Include(cer => cer.RelatedSubProject).Skip(offset).Take(limit).ToList();

                return certificates;
            }
        }

        private CertificateDetails generateCertificateDetailsFromId(double cerId)
        {
            _logger.Debug("GenerateCertificateDetailsFromId");

            //var chosenCertificate = _context.Certificates.AsNoTracking()
            //               .Include(cer => cer.RelatedCertificateissuer)
            //               .Include(cer => cer.RelatedCertificatesstatus)
            //               .Include(cer => cer.RelatedCustomer)//.ThenInclude(cus => cus.RelatedSecurityquestion)
            //               .Include(cer => cer.RelatedCustomerIdentifier)
            //               .Include(cer => cer.RelatedDocsType)
            //               .Include(cer => cer.RelatedExpiration)
            //               .Include(cer => cer.RelatedIssuerPlace)
            //               .Include(cer => cer.RelatedProject)
            //               //  .Include(cer => cer.RelatedSecurityquestion)
            //               .Include(cer => cer.RelatedSmartObject)
            //               .Include(cer => cer.RelatedSubProject)
            //               .FirstOrDefault(cer => cer.Id.Equals(Convert.ToInt32(cerId)));
            //var certificate = new CertificateDetails(chosenCertificate);
            //return certificate;

            using var scope = _scopeFactory.CreateScope();
            {
                var dependencyService = scope.ServiceProvider.GetService<IAppDbContext>();
                var chosenCertificate = dependencyService.Certificates
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
                    .FirstOrDefault(cer => cer.Id.Equals(Convert.ToInt32(cerId)));

                var certificate = new CertificateDetails(chosenCertificate);
                return certificate;
            }
        }

        private int generateCertificateId()
        {
            _logger.Debug("GenerateCertificateId");
            lock (_locker)
            {
                int maxId = _context.Certificates.OrderByDescending(cer => cer.Id).Take(1).ToList()[0].Id;
                return maxId + 1;
            }
        }

        private bool isCertificateValid(CertificateDetailsDTO certificate)
        {
            if (isFieldValueHasInjectionPotencial(certificate.Docstype?.Title) ||
                isFieldValueHasInjectionPotencial(certificate.Project?.Title) ||
                isFieldValueHasInjectionPotencial(certificate.SubProject?.Title) ||
                isFieldValueHasInjectionPotencial(certificate.Smartobject?.Title) ||
                isFieldValueHasInjectionPotencial(certificate.Certificatesstatus?.Title) ||
                isFieldValueHasInjectionPotencial(certificate.RelatedCustomerIdentifier?.Title) ||
                isFieldValueHasInjectionPotencial(certificate.CustomerName) ||
                isFieldValueHasInjectionPotencial(certificate.CertificateIssuer?.Title) ||
                isFieldValueHasInjectionPotencial(certificate.CertificateLocation?.Title) ||
                isFieldValueHasInjectionPotencial(certificate.Company) ||
                isFieldValueHasInjectionPotencial(certificate.Hpnumber) ||
                isFieldValueHasInjectionPotencial(certificate.Email) ||
                isFieldValueHasInjectionPotencial(certificate.Passportid) ||
                isFieldValueHasInjectionPotencial(certificate.Licenseid) ||
                isFieldValueHasInjectionPotencial(certificate.RelatedSecurityQuestion.Title) ||
                isFieldValueHasInjectionPotencial(certificate.Securityanswer) ||
                isFieldValueHasInjectionPotencial(certificate.Remarks) ||
                isFieldValueHasInjectionPotencial(certificate.Job))

                return false;

            return true;
        }

        private bool isFieldValueHasInjectionPotencial(string input)
        {
            char doubleQuate = '"';
            char singleQuate = '\'';
            char comaQuate = ',';
            char semicolonQuate = ';';

            if (!Char.IsLetterOrDigit(input[0]))
                return true;
            else if (input.StartsWith('-') || input.StartsWith('+') || input.StartsWith('=') || input.StartsWith('@'))
                return true;
            else if (input.Count(c => c == doubleQuate) % 2 != 0 ||
                input.Count(c => c == singleQuate) % 2 != 0 ||
                input.Count(c => c == comaQuate) % 2 != 0 ||
                input.Count(c => c == semicolonQuate) % 2 != 0)
                return true;

            return false;
        }

        #endregion
    }
}