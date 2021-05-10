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
    [Authorize(Roles = "אדמין,מחדש,מנהל,מנפיק,תומך")]
    [ApiController]
    [Route("[controller]")]
    public class CertificatesstatusController : ControllerBase
    {
        private readonly ICertificatesstatusService _certificatesstatusService;

        public CertificatesstatusController(ICertificatesstatusService certificatesstatusService)
        {
            _certificatesstatusService = certificatesstatusService;
        }

        [HttpGet("GetCertificatesStatus")]
        public async Task<IActionResult> GetCertificatesStatus(int skip, int take)
        {
            return Ok(await _certificatesstatusService.GetCertificatesStatus(skip, take));
        }

        [Authorize(Roles = "אדמין,מנהל")]
        [HttpPut("UpdateCertificatesStatus")]
        public async Task<IActionResult> UpdateCertificatesStatus(Certificatesstatus updatedCertificatestatus)
        {
            return Ok(await _certificatesstatusService.UpdateCertificatesStatus(updatedCertificatestatus));
        }
        [Authorize(Roles = "אדמין,מנהל")]
        [HttpPost("AddNewCertificatesStatus")]
        public async Task<IActionResult> AddNewCertificatesStatus(Certificatesstatus certificatestatus)
        {
            return Ok(await _certificatesstatusService.AddNewCertificatesStatus(certificatestatus));
        }
        [HttpGet("GetAllCertificatesStatus")]
        public async Task<IActionResult> GetAllCertificatesStatus()
        {         
            return Ok(await _certificatesstatusService.GetAllCertificatesStatus());
        }
    }
}