using EsignBackend.Models;
using EsignBackend.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EsignBackend.Services.SettingsService.ExpirationType
{
    public interface IExpirationTypeService
    {        
        ServiceResponse<List<ExpirationtypeDTO>> GetExpirationTypes(int skip, int take);
        Task<ServiceResponse<int>> UpdateExpirationType(Models.Expirationtype updatedExpirationType);
        Task<ServiceResponse<int>> AddNewExpirationType(Models.Expirationtype expirationType);
        ServiceResponse<List<ExpirationtypeDTO>> GetAllExpirationTypes();
    }
}
