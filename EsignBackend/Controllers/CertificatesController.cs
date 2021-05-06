using EsignBackend.Models;
using EsignBackend.Models.Tools;
using EsignBackend.Services.MainServices.Certificates;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EsignBackend.Controllers
{
    [Authorize(Roles = "אדמין,מחדש,מנהל,מנפיק,תומך")]
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

        [HttpGet("GetAmountOfCertificates")]
        public async Task<IActionResult> GetAmountOfCertificates()
        {
            _logger.Debug("GetAmountOfCertificates");
            return Ok(await _certificatesService.GetAmountOfCertificates());
        }

        [HttpGet("GetCertificatesDetails")]
        [AllowAnonymous]
        public async Task<IActionResult> GetCertificatesDetails(int skip, int take)
        {
            _logger.Debug("GetCertificatesDetails");
            return Ok(await _certificatesService.GetCertificatesDetails(skip, take));
        }
        [Authorize(Roles = "אדמין,מחדש,מנהל")]
        [HttpPut("UpdateCertificate")]
        public async Task<IActionResult> UpdateCertificate(Certificate updatedCertificate)
        {
            _logger.Debug("UpdateCertificate");
            return Ok(await _certificatesService.UpdateCertificate(updatedCertificate));
        }
        [Authorize(Roles = "אדמין,מחדש,מנהל")]
        [HttpPost("AddNewHistoryCertificate")]
        public async Task<IActionResult> AddNewHistoryCertificate(Certificateshistory certificateshistory)
        {
            _logger.Debug("AddNewHistoryCertificate");
            return Ok(await _certificatesService.AddNewHistoryCertificate(certificateshistory));
        }
        [HttpGet("GetHistoryCertificates")]
        public async Task<IActionResult> GetHistoryCertificates(string certificateId)
        {
            _logger.Debug("GetHistoryCertificates");
            double certificateIdInDouble = -1;
            Double.TryParse(certificateId, out certificateIdInDouble);
            return Ok(await _certificatesService.GetHistoryCertificates(certificateIdInDouble));
        }
        [HttpGet("GetCustomerCertificatesDetails")]
        public async Task<IActionResult> GetCustomerCertificatesDetails(string customerId)
        {
            _logger.Debug("GetCustomerCertificatesDetails");
            double customerIdDouble = -1;
            Double.TryParse(customerId, out customerIdDouble);
            return Ok(await _certificatesService.GetCustomerCertificatesDetails(customerIdDouble));
        }
        [HttpPost("SearchCertificates")]
        public async Task<IActionResult> SearchCertificates(CertificateAdvancedSearch certificateAdvancedSearch, int skip, int take)
        {
            _logger.Debug("SearchCertificates");
            return Ok(await _certificatesService.SearchCertificates(certificateAdvancedSearch,skip, take));
        }
        [HttpGet ("CheckSecurityAnswer")]
        public async Task<IActionResult> CheckSecurityAnswer(int cerId, string secAns, int question)
        {
            _logger.Debug("CheckSecurityAnswer");
            return Ok(await _certificatesService.CheckSecurityAnswer(cerId, secAns,question));
        }
        [Authorize(Roles = "אדמין,מחדש,מנהל")]
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


        /*   [HttpGet("GetCertificates")]
   public async Task<IActionResult> GetCertificates(int skip, int take)
   {
       return Ok(await _certificatesService.GetCertificates(skip, take));
   }
*/
    }
}