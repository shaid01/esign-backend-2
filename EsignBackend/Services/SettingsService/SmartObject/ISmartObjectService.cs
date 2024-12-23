using EsignBackend.Models;
using EsignBackend.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EsignBackend.Services.SettingsService.SmartObject
{
    public interface ISmartObjectService
    {
        ServiceResponse<List<SmartobjectDTO>> GetSmartObjects(int skip, int take);
        ServiceResponse<List<SmartobjectDTO>> GetAllSmartObjects();
        Task<ServiceResponse<int>> UpdateSmartObject(Smartobject updatedSmartObject);
        Task<ServiceResponse<int>> AddNewSmartObject(Smartobject smartobject);
    }
}
