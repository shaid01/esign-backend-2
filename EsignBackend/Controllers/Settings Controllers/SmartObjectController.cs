using EsignBackend.Models;
using EsignBackend.Services.SettingsService.SmartObject;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace EsignBackend.Controllers.Settings_Controllers
{
    [Authorize(Roles = "מנהל,מנפיק,תומך")]
    [ApiController]
    [Route("[controller]")]
    public class SmartObjectController : ControllerBase
    {
        private readonly ISmartObjectService _smartObjectService;
        public SmartObjectController(ISmartObjectService smartObjectService)
        {
            _smartObjectService = smartObjectService;
        }

        [HttpGet("GetSmartObjects")]
        public IActionResult GetSmartObjects(int skip, int take)
        {
            return Ok(_smartObjectService.GetSmartObjects(skip, take));
        }

        [Authorize(Roles = "מנהל")]
        [HttpPut("UpdateSmartObject")]
        public async Task<IActionResult> UpdateCertificatesStatus(Smartobject updatedSmartobject)
        {
            return Ok(await _smartObjectService.UpdateSmartObject(updatedSmartobject));
        }
        [Authorize(Roles = "מנהל")]
        [HttpPost("AddNewSmartObject")]
        public async Task<IActionResult> AddNewCertificatesStatus(Smartobject smartobject)
        {
            return Ok(await _smartObjectService.AddNewSmartObject(smartobject));
        }

        [HttpGet("GetAllSmartObjects")]
        public IActionResult GetAllSmartObjects()
        {
            return Ok(_smartObjectService.GetAllSmartObjects());
        }
    }
}