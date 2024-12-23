using EsignBackend.Models;
using EsignBackend.Services.SettingsService.ExpirationType;
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
    [Route("[Controller]")]
    public class ExpirationtypeController : ControllerBase
    {
        private readonly IExpirationTypeService _expirationTypeService;

        public ExpirationtypeController(IExpirationTypeService expirationTypeService)
        {
            this._expirationTypeService = expirationTypeService;
        }

        [HttpGet("GetExpirationTypes")]
        public IActionResult GetExpirationTypes(int skip, int take)
        {
            return Ok(_expirationTypeService.GetExpirationTypes(skip, take));
        }

        [Authorize(Roles = "מנהל")]
        [HttpPut("UpdateExpirationType")]
        public async Task<IActionResult> UpdateCertificatesStatus(Expirationtype updatedExpirationType)
        {
            return Ok(await _expirationTypeService.UpdateExpirationType(updatedExpirationType));
        }

        [Authorize(Roles = "מנהל")]
        [HttpPost("AddNewExpirationType")]
        public async Task<IActionResult> AddNewExpirationType(Expirationtype expirationType)
        {
            return Ok(await _expirationTypeService.AddNewExpirationType(expirationType));
        }

        [HttpGet("GetAllExpirationTypes")]
        public IActionResult GetAllExpirationTypes()
        {
            return Ok(_expirationTypeService.GetAllExpirationTypes());
        }
    }
}
