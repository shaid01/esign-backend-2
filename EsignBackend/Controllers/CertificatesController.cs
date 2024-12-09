using EsignBackend.Models;
using EsignBackend.Models.Tools;
using EsignBackend.Services.MainServices.Certificates;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace EsignBackend.Controllers
{
    [Authorize(Roles = "מנהל,מנפיק,תומך")]
    [ApiController]
    [Route("[controller]")]
    public class CertificatesController : ControllerBase
    {
        private readonly ICertificatesService _certificatesService;
        private readonly ILogger _logger;

        public CertificatesController(ICertificatesService certificatesService, ILogger logger)
        {
            _certificatesService = certificatesService;
            _logger = logger;
        }

        [Authorize]
        [HttpGet("GetCertificateExtendedDetails")]
        public async Task<IActionResult> GetCertificateExtendedDetails(int id)
        {
            _logger.Debug("GetCertificateExtendedDetails");
            Response.Headers.Add("Access-Control-Allow-Origin", "*");
            return Ok(await _certificatesService.GetCertificateExtendedDetails(id));
        }

        [Authorize]
        [HttpGet("GetCertificatesDetails")]
        public async Task<IActionResult> GetCertificatesDetails(int skip, int take)
        {
            _logger.Debug("GetCertificatesDetails");
            return Ok(await _certificatesService.GetCertificatesDetails(skip, take));
        }
        [Authorize(Roles = "מנפיק,מנהל")]
        [HttpPut("UpdateCertificate")]
        public async Task<IActionResult> UpdateCertificate(Certificate updatedCertificate)
        {
            _logger.Debug("UpdateCertificate");
            return Ok(await _certificatesService.UpdateCertificate(updatedCertificate));
        }
        /*        [Authorize(Roles = "אדמין,מחדש,מנהל")]
        [HttpPost("AddNewHistoryCertificate")]
        public async Task<IActionResult> AddNewHistoryCertificate(Certificateshistory certificateshistory)
        {
            _logger.Debug("AddNewHistoryCertificate");
            return Ok(await _certificatesService.AddNewHistoryCertificate(certificateshistory));
        }*/
        [HttpGet("GetHistoryCertificates")]
        public async Task<IActionResult> GetHistoryCertificates(string certificateId)
        {
            _logger.Debug("GetHistoryCertificates");
            double certificateIdInDouble = -1;
            Double.TryParse(certificateId, out certificateIdInDouble);
            return Ok(await _certificatesService.GetHistoryCertificates(certificateIdInDouble));
        }

        [HttpGet("GetCustomerCertificates")]
        public async Task<IActionResult> GetCustomerCertificates(double customerId)
        {
            _logger.Debug("GetCustomerCertificates");
            return Ok(await _certificatesService.GetCustomerCertificates(customerId));
        }
        [HttpPost("SearchCertificates")]
        public async Task<IActionResult> SearchCertificates(CertificateAdvancedSearch certificateAdvancedSearch, int skip, int take)
        {
            _logger.Debug("SearchCertificates");
            var response = await _certificatesService.SearchCertificates(certificateAdvancedSearch, skip, take);

            return Ok(response);
            
        }

        [HttpPost("export")]
        public async Task<IActionResult> ExportCertificates(CertificateAdvancedSearch certificateAdvancedSearch, int skip, int take)
        {
            _logger.Debug("ExportCertificates");
            var response = await _certificatesService.SearchCertificates(certificateAdvancedSearch, skip, take);
            var fileContent = _certificatesService.GenerateXlsxFile(response.Data);
            Response.Headers.Add("x-file-name", WebUtility.UrlEncode($"certificates_{DateTime.Now.Date.ToString("dd-MM-yyyy")}.xlsx"));

            return File(
                fileContent,
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                $"certificates_{DateTime.Now.Date}.xlsx");
        }

        [HttpGet ("CheckSecurityAnswer")]
        public async Task<IActionResult> CheckSecurityAnswer(int cerId, string secAns, int question)
        {
            _logger.Debug("CheckSecurityAnswer");
            return Ok(await _certificatesService.CheckSecurityAnswer(cerId, secAns,question));
        }
        [Authorize(Roles = "מנהל,מנפיק")]
        [HttpPost("AddCertificate")]
        public async Task<IActionResult> AddCertificate(Certificate certificate)
        {
            _logger.Debug("AddCertificate");
            return Ok(await _certificatesService.AddCertificate(certificate));
        }
        [HttpPut("UpdateExpiredCertificates")]
        public async Task<IActionResult> UpdateExpiredCertificates()
        {
            return Ok(await _certificatesService.UpdateExpiredCertificates());
        }

    }
}