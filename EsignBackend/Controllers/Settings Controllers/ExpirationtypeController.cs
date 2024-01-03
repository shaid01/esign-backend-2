using EsignBackend.Models;
using EsignBackend.Services.SettingsService.Expirationtype;
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
    [Route("[Controller]")]
    public class ExpirationtypeController : ControllerBase
    {
        private readonly IExpirationTypeService _expirationTypeService;

        public ExpirationtypeController(IExpirationTypeService expirationTypeService)
        {
            this._expirationTypeService = expirationTypeService;
        }

        [HttpGet("GetExpirationTypes")]
        public async Task<IActionResult> GetExpirationTypes(int skip, int take)
        {
            return Ok(await _expirationTypeService.GetExpirationTypes(skip, take));
        }
        [Authorize(Roles = "אדמין,מנהל")]
        [HttpPut("UpdateExpirationType")]
        public async Task<IActionResult> UpdateCertificatesStatus(ExpirationType updatedExpirationType)
        {
            return Ok(await _expirationTypeService.UpdateExpirationType(updatedExpirationType));
        }
        [Authorize(Roles = "אדמין,מנהל")]
        [HttpPost("AddNewExpirationType")]
        public async Task<IActionResult> AddNewExpirationType(ExpirationType expirationType)
        {
            return Ok(await _expirationTypeService.AddNewExpirationType(expirationType));
        }
        [HttpGet("GetAllExpirationTypes")]
        public async Task<IActionResult> GetAllExpirationTypes()
        {
            return Ok(await _expirationTypeService.GetAllExpirationTypes());
        }
    }
}
