using EsignBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EsignBackend.Services.SettingsService.Expirationtype
{
    public interface IExpirationTypeService
    {
        Task<ServiceResponse<int>> GetAmountOfExpirationTypes();
        Task<ServiceResponse<List<Models.Expirationtype>>> GetExpirationTypes(int skip, int take);
        Task<ServiceResponse<int>> UpdateExpirationType(Models.Expirationtype updatedExpirationType);
        Task<ServiceResponse<int>> AddNewExpirationType(Models.Expirationtype expirationType);
        Task<ServiceResponse<List<Models.Expirationtype>>> GetAllExpirationTypes();
    }
}
