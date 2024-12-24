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
        ServiceResponse<List<DocstypeDTO>> GetIdentificationDocuments(int skip, int take);
        Task<ServiceResponse<int>> UpdateIdentificationDocument(Docstype updatedIdentificationDocument);
        Task<ServiceResponse<int>> AddNewIdentificationDocument(Docstype identificationDocument);
        ServiceResponse<List<DocstypeDTO>> GetAllIdentificationDocuments();
    }
}
