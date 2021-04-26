using EsignBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EsignBackend.Services.SettingsService.IdentificationDocument
{
    public interface IIdentificationDocumentService
    {
        Task<ServiceResponse<int>> GetAmountOfIdentificationDocuments();
        Task<ServiceResponse<List<Docstype>>> GetIdentificationDocuments(int skip, int take);
        Task<ServiceResponse<int>> UpdateIdentificationDocument(Docstype updatedIdentificationDocument);
        Task<ServiceResponse<int>> AddNewIdentificationDocument(Docstype identificationDocument);
        Task<ServiceResponse<List<Docstype>>> GetAllIdentificationDocuments();
    }
}
