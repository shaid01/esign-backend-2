using EsignBackend.Models;
using EsignBackend.Services.SettingsService.CertificateRemarks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EsignBackend.Controllers.Settings_Controllers
{
    [Authorize(Roles = "מנהל,מנפיק,תומך")]
    [ApiController]
    [Route("[controller]")]
    public class CertificateRemarksController : ControllerBase
    {
        private readonly ICertificateRemarksService _certificateRemarksService;

        public CertificateRemarksController(ICertificateRemarksService certificateRemarksService)
        {
            this._certificateRemarksService = certificateRemarksService;
        }

        [HttpGet("GetCertificateRemarks")]
        public async Task<IActionResult> GetCertificateRemarks(int skip, int take)
        {
            return Ok(await _certificateRemarksService.GetCertificateRemarks(skip, take));
        }
        [Authorize(Roles = "מנהל")]
        [HttpPut("UpdateCertificateRemarks")]
        public async Task<IActionResult> UpdateCertificatesStatus(Certificatermeark updatedCertificateRemarks)
        {
            return Ok(await _certificateRemarksService.UpdateCertificateRemarks(updatedCertificateRemarks));
        }
        [Authorize(Roles = "מנהל")]
        [HttpPost("AddNewCertificateRemarks")]
        public async Task<IActionResult> AddNewCertificateRemarks(Certificatermeark certificateRemark)
        {
            return Ok(await _certificateRemarksService.AddNewCertificateRemarks(certificateRemark));
        }
    }
}