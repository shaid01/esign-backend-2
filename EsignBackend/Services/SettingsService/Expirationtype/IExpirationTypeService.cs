using EsignBackend.Models;
using EsignBackend.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EsignBackend.Services.SettingsService.Expirationtype
{
    public interface IExpirationTypeService
    {        
        Task<ServiceResponse<List<ExpirationtypeDTO>>> GetExpirationTypes(int skip, int take);
        Task<ServiceResponse<int>> UpdateExpirationType(Models.ExpirationType updatedExpirationType);
        Task<ServiceResponse<int>> AddNewExpirationType(Models.ExpirationType expirationType);
        Task<ServiceResponse<List<ExpirationtypeDTO>>> GetAllExpirationTypes();
    }
}
