using EsignBackend.Models;
using EsignBackend.Services.SettingsService.CallsPriority;
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
    public class CallsPriorityController : ControllerBase
    {
        private readonly ICallsPriorityService _callsPriorityService; 
        public CallsPriorityController(ICallsPriorityService callsPriorityService)
        {
            _callsPriorityService = callsPriorityService;
        }
        [HttpGet("GetAmountOfCallsPriority")]
        public async Task<IActionResult> GetAmountOfCallsPriority()
        {
            return Ok(await _callsPriorityService.GetAmountOfCallsPriority());
        }
        [HttpGet("GetCallsPriority")]
        public async Task<IActionResult> GetCallsPriority(int skip, int take)
        {
            return Ok(await _callsPriorityService.GetCallsPriority(skip, take));
        }
        [Authorize(Roles = "אדמין,מנהל")]
        [HttpPut("UpdateCallPriority")]
        public async Task<IActionResult> UpdateCallPriority(Callpriority UpdatedCallPriority)
        {
            return Ok(await _callsPriorityService.UpdateCallPriority(UpdatedCallPriority));
        }
        [Authorize(Roles = "אדמין,מנהל")]
        [HttpPost("AddNewCallpriority")]
        public async Task<IActionResult> AddNewCallpriority(Callpriority callPriority)
        {
            return Ok(await _callsPriorityService.AddNewCallPriority(callPriority));
        }
    }
}