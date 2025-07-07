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
    [ApiController]
    [Route("[controller]")]
    public class IssueLocationController : ControllerBase
    {
        private readonly IIssueLocationService _issueLocationService;
        public IssueLocationController(IIssueLocationService issueLocationService)
        {
            _issueLocationService = issueLocationService;
        }

        [Authorize(Roles = "מנהל")]
        [HttpGet("GetIssueLocations")]
        public IActionResult GetIssueLocations(int skip, int take)
        {
            return Ok(_issueLocationService.GetIssueLocations(skip, take));
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

        [Authorize(Roles = "מנהל,מנפיק,תומך")]
        [HttpGet("GetAllIssueLocations")]
        public IActionResult GetAllIssueLocations()
        {
            return Ok(_issueLocationService.GetAllIssueLocations());
        }
    }
}