using EsignBackend.Models;
using EsignBackend.Services.SettingsService.CertificateIssuer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EsignBackend.Controllers.Settings_Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CertificateIssuerController : ControllerBase
    {
        private readonly ICertificateIssuerService _certificateIssuerService;
        public CertificateIssuerController(ICertificateIssuerService certificateIssuerService)
        {
            _certificateIssuerService = certificateIssuerService;
        }

        [Authorize(Roles = "מנהל,מנפיק,תומך")]
        [HttpGet("GetCertificateIssuers")]
        public IActionResult GetCertificateIssuers(int skip, int take)
        {
            return Ok(_certificateIssuerService.GetCertificateIssuers(skip, take));
        }

        [Authorize(Roles = "מנהל")]
        [HttpPut("UpdateCertificateIssuer")]
        public async Task<IActionResult> UpdateCustomerIdentifer(Isscert updatedCertificateIssuer)
        {
            return Ok(await _certificateIssuerService.UpdateCertificateIssuer(updatedCertificateIssuer));
        }
        [Authorize(Roles = "מנהל")]
        [HttpPost("AddNewCertificateIssuer")]
        public async Task<IActionResult> AddNewCertificatesStatus(Isscert certificateIssuer)
        {
            return Ok(await _certificateIssuerService.AddNewCertificateIssuer(certificateIssuer));
        }

        [Authorize(Roles = "מנהל,מנפיק,תומך")]
        [HttpGet("GetAllCertificateIssuers")]
        public IActionResult GetAllCertificateIssuers()
        {
            return Ok(_certificateIssuerService.GetAllCertificateIssuers());
        }
    }
}