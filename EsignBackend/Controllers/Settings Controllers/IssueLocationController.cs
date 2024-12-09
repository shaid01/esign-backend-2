using EsignBackend.Models;
using EsignBackend.Services.SettingsService.IssueLocation;
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
    public class IssueLocationController : ControllerBase
    {
        private readonly IIssueLocationService _issueLocationService;
        public IssueLocationController(IIssueLocationService issueLocationService)
        {
            _issueLocationService = issueLocationService;
        }

        [HttpGet("GetIssueLocations")]
        public async Task<IActionResult> GetIssueLocations(int skip, int take)
        {
            return Ok(await _issueLocationService.GetIssueLocations(skip, take));
        }
        [Authorize(Roles = "מנהל")]
        [HttpPut("UpdateIssueLocation")]
        public async Task<IActionResult> UpdateIssueLocation(Issplace updatedIssueLocation)
        {
            return Ok(await _issueLocationService.UpdateIssueLocation(updatedIssueLocation));
        }
        [Authorize(Roles = "מנהל")]
        [HttpPost("AddNewIssueLocation")]
        public async Task<IActionResult> AddNewIssueLocation(Issplace issueLocation)
        {
            return Ok(await _issueLocationService.AddNewIssueLocation(issueLocation));
        }
        [HttpGet("GetAllIssueLocations")]
        public async Task<IActionResult> GetAllIssueLocations()
        {
            return Ok(await _issueLocationService.GetAllIssueLocations());
        }
    }
}