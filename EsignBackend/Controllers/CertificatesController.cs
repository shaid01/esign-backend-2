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
using System.Security.Claims;
using System.Threading.Tasks;

namespace EsignBackend.Controllers
{
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

        [Authorize(Roles = "מנהל,מנפיק,תומך")]
        [HttpGet("GetCertificateExtendedDetails")]
        public IActionResult GetCertificateExtendedDetails(int id)
        {
            //commented 20-FEB-2025
            //Response.Headers.Add("Access-Control-Allow-Origin", "*");
            return Ok(_certificatesService.GetCertificateExtendedDetails(id));
        }

        [Authorize(Roles = "מנהל,מנפיק,תומך")]
        [HttpGet("GetCertificatesDetails")]
        public async Task<IActionResult> GetCertificatesDetails(int skip, int take)
        {
            return Ok(await _certificatesService.GetCertificatesDetails(skip, take));
        }

        [Authorize(Roles = "מנפיק,מנהל")]
        [HttpPut("UpdateCertificate")]
        public async Task<IActionResult> UpdateCertificate(Certificate updatedCertificate)
        {
            return Ok(await _certificatesService.UpdateCertificate(updatedCertificate));
        }

        [Authorize(Roles = "מנפיק,מנהל")]
        [HttpPut("DeleteCertificates")]
        public async Task<IActionResult> DeleteCertificates(List<Certificate> certsToDelete)
        {
            return Ok(await _certificatesService.DeleteCertificates(certsToDelete));
        }

        /*        [Authorize(Roles = "אדמין,מחדש,מנהל")]
        [HttpPost("AddNewHistoryCertificate")]
        public async Task<IActionResult> AddNewHistoryCertificate(Certificateshistory certificateshistory)
        {
            _logger.Debug("AddNewHistoryCertificate");
            return Ok(await _certificatesService.AddNewHistoryCertificate(certificateshistory));
        }*/

        [Authorize(Roles = "מנהל,מנפיק,תומך")]
        [HttpGet("GetHistoryCertificates")]
        public async Task<IActionResult> GetHistoryCertificates(string certificateId)
        {
            double certificateIdInDouble = -1;
            Double.TryParse(certificateId, out certificateIdInDouble);
            return Ok(await _certificatesService.GetHistoryCertificates(certificateIdInDouble));
        }

        [Authorize(Roles = "מנהל,מנפיק,תומך")]
        [HttpGet("GetCustomerCertificates")]
        public async Task<IActionResult> GetCustomerCertificates(double customerId)
        {
            return Ok(await _certificatesService.GetCustomerCertificates(customerId));
        }

        [Authorize(Roles = "מנהל,מנפיק,תומך")]
        [HttpPost("SearchCertificates")]
        public async Task<IActionResult> SearchCertificates(CertificateAdvancedSearch certificateAdvancedSearch, int skip, int take)
        {
            var response = await _certificatesService.SearchCertificates(certificateAdvancedSearch, skip, take);

            return Ok(response);
        }

        [Authorize(Roles = "מנהל,מנפיק,תומך")]
        [HttpPost("export")]
        public async Task<IActionResult> ExportCertificates(CertificateAdvancedSearch certificateAdvancedSearch, int skip, int take)
        {
            _logger.Information($"Certificates export request: User - [{HttpContext.User.FindFirst(ClaimTypes.Name).Value}]");

            var response = await _certificatesService.SearchCertificates(certificateAdvancedSearch, skip, take);

            var fileContent = _certificatesService.GenerateXlsxFile(response.Data);

            Response.Headers.Add("x-file-name", WebUtility.UrlEncode($"certificates_{DateTime.Now.Date.ToString("dd-MM-yyyy")}.xlsx"));

            return File(
                fileContent,
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                $"certificates_{DateTime.Now.Date}.xlsx");
        }

        [Authorize(Roles = "מנהל,מנפיק,תומך")]
        [HttpGet ("CheckSecurityAnswer")]
        public async Task<IActionResult> CheckSecurityAnswer(int cerId, string secAns, int question)
        {
            return Ok(await _certificatesService.CheckSecurityAnswer(cerId, secAns,question));
        }

        [Authorize(Roles = "מנהל,מנפיק")]
        [HttpPost("AddCertificate")]
        public async Task<IActionResult> AddCertificate(Certificate certificate)
        {
            return Ok(await _certificatesService.AddCertificate(certificate));
        }

        [Authorize(Roles = "מנהל,מנפיק")]
        [HttpPut("UpdateExpiredCertificates")]
        public async Task<IActionResult> UpdateExpiredCertificates()
        {
            return Ok(await _certificatesService.UpdateExpiredCertificates());
        }

    }
}