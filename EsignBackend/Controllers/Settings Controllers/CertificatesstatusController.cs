using EsignBackend.Models;
using EsignBackend.Services.SettingsService.CertificatesStatus;
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
    public class CertificatesstatusController : ControllerBase
    {
        private readonly ICertificatesstatusService _certificatesstatusService;

        public CertificatesstatusController(ICertificatesstatusService certificatesstatusService)
        {
            _certificatesstatusService = certificatesstatusService;
        }

        [Authorize(Roles = "מנהל")]
        [HttpGet("GetCertificatesStatus")]
        public IActionResult GetCertificatesStatus(int skip, int take)
        {
            return Ok(_certificatesstatusService.GetCertificatesStatus(skip, take));
        }

        [Authorize(Roles = "מנהל")]
        [HttpPut("UpdateCertificatesStatus")]
        public async Task<IActionResult> UpdateCertificatesStatus(Certificatesstatus updatedCertificatestatus)
        {
            return Ok(await _certificatesstatusService.UpdateCertificatesStatus(updatedCertificatestatus));
        }

        [Authorize(Roles = "מנהל")]
        [HttpPost("AddNewCertificatesStatus")]
        public async Task<IActionResult> AddNewCertificatesStatus(Certificatesstatus certificatestatus)
        {
            return Ok(await _certificatesstatusService.AddNewCertificatesStatus(certificatestatus));
        }

        [Authorize(Roles = "מנהל,מנפיק,תומך")]
        [HttpGet("GetAllCertificatesStatus")]
        public IActionResult GetAllCertificatesStatus()
        {         
            return Ok(_certificatesstatusService.GetAllCertificatesStatus());
        }
    }
}