using EsignBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EsignBackend.Services.SettingsService.SmartObject
{
    public interface ISmartObjectService
    {
        Task<ServiceResponse<List<Smartobject>>> GetSmartObjects(int skip, int take);
        Task<ServiceResponse<int>> GetAmountOfSmartObjects();
        Task<ServiceResponse<int>> UpdateSmartObject(Smartobject updatedSmartObject);
        Task<ServiceResponse<int>> AddNewSmartObject(Smartobject smartobject);
        Task<ServiceResponse<List<Smartobject>>> GetAllSmartObjects();
    }
}
