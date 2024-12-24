using EsignBackend.Models;
using EsignBackend.Services.SettingsService.CallStatus;
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
    public class CallStatusController : ControllerBase
    {
        private readonly ICallStatusService _callStatusService;

        public CallStatusController(ICallStatusService callStatusService)
        {
            _callStatusService = callStatusService;
        }

        [HttpGet("GetCallsStatus")]
        public IActionResult GetCallsStatus(int skip, int take)
        {
            return Ok(_callStatusService.GetCallsStatus(skip, take));
        }

        [Authorize(Roles = "מנהל")]
        [HttpPut("UpdateCallStatus")]
        public async Task<IActionResult> UpdateCallStatus(Callstatus updatedCallstatus)
        {
            return Ok(await _callStatusService.UpdateCallStatus(updatedCallstatus));
        }
        [Authorize(Roles = "מנהל")]
        [HttpPost("AddNewCallStatus")]
        public async Task<IActionResult> AddNewCallStatus(Callstatus callstatus)
        {
            return Ok(await _callStatusService.AddNewCallStatus(callstatus));
        }
    }
}