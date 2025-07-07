using EsignBackend.Models;
using EsignBackend.Services.SettingsService.SecurityQuestions;
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
    public class SecurityQuestionsController : ControllerBase
    {
        private readonly ISecurityQuestionsService _securityQuestionsService;
        public SecurityQuestionsController(ISecurityQuestionsService securityQuestionsService)
        {
            this._securityQuestionsService = securityQuestionsService;
        }

        [Authorize(Roles = "מנהל")]
        [HttpGet("GetSecurityQuestions")]
        public IActionResult GetSecurityQuestions(int skip, int take)
        {
            return Ok(_securityQuestionsService.GetSecurityQuestions(skip, take));
        }

        [Authorize(Roles = "מנהל")]
        [HttpPut("UpdateSecurityQuestion")]
        public async Task<IActionResult> UpdateSecurityQuestion(Securityquestion updatedSecurityQuestion)
        {
            return Ok(await _securityQuestionsService.UpdateSecurityQuestion(updatedSecurityQuestion));
        }

        [Authorize(Roles = "מנהל")]
        [HttpPost("AddNewSecurityQuestion")]
        public async Task<IActionResult> AddNewSecurityQuestion(Securityquestion securityQuestion)
        {
            return Ok(await _securityQuestionsService.AddNewSecurityQuestion(securityQuestion));
        }

        [Authorize(Roles = "מנהל,מנפיק,תומך")]
        [HttpGet("GetAllSecurityQuestions")]
        public IActionResult GetAllSecurityQuestions()
        {
            return Ok(_securityQuestionsService.GetAllSecurityQuestions());
        }
    }
}
