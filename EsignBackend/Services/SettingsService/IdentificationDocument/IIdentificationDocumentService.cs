using EsignBackend.Models;
using EsignBackend.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EsignBackend.Services.SettingsService.IdentificationDocument
{
    public interface IIdentificationDocumentService
    {
        Task<ServiceResponse<List<DocstypeDTO>>> GetIdentificationDocuments(int skip, int take);
        Task<ServiceResponse<int>> UpdateIdentificationDocument(DocsType updatedIdentificationDocument);
        Task<ServiceResponse<int>> AddNewIdentificationDocument(DocsType identificationDocument);
        Task<ServiceResponse<List<DocstypeDTO>>> GetAllIdentificationDocuments();
    }
}
