using EsignBackend.Models;
using EsignBackend.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EsignBackend.Services.SettingsService.IssueLocation
{
    public interface IIssueLocationService
    {
        Task<ServiceResponse<List<IssplaceDTO>>> GetIssueLocations(int skip, int take);
        Task<ServiceResponse<int>> UpdateIssueLocation(Issplace updatedIssueLocation);
        Task<ServiceResponse<int>> AddNewIssueLocation(Issplace issueLocation);
        Task<ServiceResponse<List<IssplaceDTO>>> GetAllIssueLocations();
    }
}
