using EsignBackend.Models;
using EsignBackend.Models.DTOs.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EsignBackend.Services.SettingsService.CallsPriority
{
    public interface ICallsPriorityService
    {
        Task<ServiceResponse<List<CallpriorityDTO>>> GetCallsPriority(int skip, int take);
        Task<ServiceResponse<int>> UpdateCallPriority(CallPriority updatedCallPriority);
        Task<ServiceResponse<int>> AddNewCallPriority(CallPriority callPriority);
    }
}