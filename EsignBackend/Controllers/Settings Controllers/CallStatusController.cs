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
    [Authorize(Roles = "אדמין,מחדש,מנהל,מנפיק,תומך")]
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
        public async Task<IActionResult> GetCallsStatus(int skip, int take)
        {
            return Ok(await _callStatusService.GetCallsStatus(skip, take));
        }
        [HttpGet("GetAmountOfCallsStatus")]
        public async Task<IActionResult> GetAmountOfCallsStatus()
        {
           // Response.Headers.Add("aaaa", "bbbb");
            return Ok(await _callStatusService.GetAmountOfCallsStatus());
        }
        [Authorize(Roles = "אדמין,מנהל")]
        [HttpPut("UpdateCallStatus")]
        public async Task<IActionResult> UpdateCallStatus(Callstatus updatedCallstatus)
        {
            return Ok(await _callStatusService.UpdateCallStatus(updatedCallstatus));
        }
        [Authorize(Roles = "אדמין,מנהל")]
        [HttpPost("AddNewCallStatus")]
        public async Task<IActionResult> AddNewCallStatus(Callstatus callstatus)
        {
            return Ok(await _callStatusService.AddNewCallStatus(callstatus));
        }
    }
}