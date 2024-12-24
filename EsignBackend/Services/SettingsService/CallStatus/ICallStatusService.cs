using EsignBackend.Models;
using EsignBackend.Models.DTOs.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EsignBackend.Services.SettingsService.CallStatus
{
    public interface ICallStatusService
    {
        ServiceResponse<List<CallStatusDto>> GetCallsStatus(int skip, int take);
        Task<ServiceResponse<int>> UpdateCallStatus(Callstatus updatedCallstatus);
        Task<ServiceResponse<int>> AddNewCallStatus(Callstatus callstatus);
    }
}