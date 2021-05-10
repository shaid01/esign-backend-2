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
    [Authorize(Roles = "אדמין,מחדש,מנהל,מנפיק,תומך")]
    [ApiController]
    [Route("[controller]")]
    public class SecurityQuestionsController : ControllerBase
    {
        private readonly ISecurityQuestionsService _securityQuestionsService;
        public SecurityQuestionsController(ISecurityQuestionsService securityQuestionsService)
        {
            this._securityQuestionsService = securityQuestionsService;
        }
        [HttpGet("GetSecurityQuestions")]
        public async Task<IActionResult> GetSecurityQuestions(int skip, int take)
        {
            return Ok(await _securityQuestionsService.GetSecurityQuestions(skip, take));
        }
        [Authorize(Roles = "אדמין,מנהל")]
        [HttpPut("UpdateSecurityQuestion")]
        public async Task<IActionResult> UpdateSecurityQuestion(Securityquestion updatedSecurityQuestion)
        {
            return Ok(await _securityQuestionsService.UpdateSecurityQuestion(updatedSecurityQuestion));
        }
        [Authorize(Roles = "אדמין,מנהל")]
        [HttpPost("AddNewSecurityQuestion")]
        public async Task<IActionResult> AddNewSecurityQuestion(Securityquestion securityQuestion)
        {
            return Ok(await _securityQuestionsService.AddNewSecurityQuestion(securityQuestion));
        }

        [HttpGet("GetAllSecurityQuestions")]
        public async Task<IActionResult> GetAllSecurityQuestions()
        {
            return Ok(await _securityQuestionsService.GetAllSecurityQuestions());
        }
    }
}