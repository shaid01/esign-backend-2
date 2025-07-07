using EsignBackend.Models;
using EsignBackend.Services.SettingsService.IdentificationDocument;
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
    public class IdentificationDocumentController : ControllerBase
    {
        private readonly IIdentificationDocumentService _identificationDocumentService;
        public IdentificationDocumentController(IIdentificationDocumentService identificationDocumentService)
        {
            _identificationDocumentService = identificationDocumentService;
        }

        [Authorize(Roles = "מנהל")]
        [HttpGet("GetIdentificationDocuments")]
        public IActionResult GetIdentificationDocuments(int skip, int take)
        {
            return Ok(_identificationDocumentService.GetIdentificationDocuments(skip, take));
        }

        [Authorize(Roles = "מנהל")]
        [HttpPut("UpdateIdentificationDocument")]
        public async Task<IActionResult> UpdateIdentificationDocument(Docstype UpdatedIdentificationDocument)
        {
            return Ok(await _identificationDocumentService.UpdateIdentificationDocument(UpdatedIdentificationDocument));
        }

        [Authorize(Roles = "מנהל")]
        [HttpPost("AddNewIdentificationDocument")]
        public async Task<IActionResult> AddNewIdentificationDocument(Docstype identificationDocument)
        {
            return Ok(await _identificationDocumentService.AddNewIdentificationDocument(identificationDocument));
        }

        [Authorize(Roles = "מנהל,מנפיק,תומך")]
        [HttpGet("GetAllIdentificationDocuments")]
        public IActionResult GetAllIdentificationDocuments()
        {
            return Ok(_identificationDocumentService.GetAllIdentificationDocuments());
        }
    }
}