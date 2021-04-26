using EsignBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EsignBackend.Services.SettingsService.CallStatus
{
    public interface ICallStatusService
    {
        Task<ServiceResponse<int>> GetAmountOfCallsStatus();
        Task<ServiceResponse<List<Callstatus>>> GetCallsStatus(int skip, int take);
        Task<ServiceResponse<int>> UpdateCallStatus(Callstatus updatedCallstatus);
        Task<ServiceResponse<int>> AddNewCallStatus(Callstatus callstatus);
    }
}
