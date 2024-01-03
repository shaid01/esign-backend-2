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
    [Authorize(Roles = "אדמין,מחדש,מנהל,מנפיק,תומך")]
    [ApiController]
    [Route("[controller]")]
    public class IdentificationDocumentController : ControllerBase
    {
        private readonly IIdentificationDocumentService _identificationDocumentService;
        public IdentificationDocumentController(IIdentificationDocumentService identificationDocumentService)
        {
            _identificationDocumentService = identificationDocumentService;
        }

        [HttpGet("GetIdentificationDocuments")]
        public async Task<IActionResult> GetIdentificationDocuments(int skip, int take)
        {
            return Ok(await _identificationDocumentService.GetIdentificationDocuments(skip, take));
        }
        [Authorize(Roles = "אדמין,מנהל")]
        [HttpPut("UpdateIdentificationDocument")]
        public async Task<IActionResult> UpdateIdentificationDocument(DocsType UpdatedIdentificationDocument)
        {
            return Ok(await _identificationDocumentService.UpdateIdentificationDocument(UpdatedIdentificationDocument));
        }
        [Authorize(Roles = "אדמין,מנהל")]
        [HttpPost("AddNewIdentificationDocument")]
        public async Task<IActionResult> AddNewIdentificationDocument(DocsType identificationDocument)
        {
            return Ok(await _identificationDocumentService.AddNewIdentificationDocument(identificationDocument));
        }
        [HttpGet("GetAllIdentificationDocuments")]
        public async Task<IActionResult> GetAllIdentificationDocuments()
        {
            return Ok(await _identificationDocumentService.GetAllIdentificationDocuments());
        }
    }
}