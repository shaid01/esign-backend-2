using EsignBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EsignBackend.Services.SettingsService.CallsPriority
{
    public interface ICallsPriorityService
    {
        Task<ServiceResponse<int>> GetAmountOfCallsPriority();
        Task<ServiceResponse<List<Callpriority>>> GetCallsPriority(int skip, int take);
        Task<ServiceResponse<int>> UpdateCallPriority(Callpriority updatedCallPriority);
        Task<ServiceResponse<int>> AddNewCallPriority(Callpriority callPriority);
    }
}
