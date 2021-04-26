using EsignBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EsignBackend.Services.SettingsService
{
    public interface IProjectsService
    {
        Task<ServiceResponse<List<Project>>> GetProjects();
        Task<ServiceResponse<List<Subproject>>> GetSubprojectsInRange(int skip, int take);
        Task<ServiceResponse<int>> GetAmountOfSubprojects();
        Task<ServiceResponse<int>> UpdateProject(Project updatedProject);
        Task<ServiceResponse<int>> UpdateSubproject(Subproject updatedSubproject);
        Task<ServiceResponse<int>> AddNewProject(Project newProject);
        Task<ServiceResponse<int>> AddNewSubroject(Subproject newSubproject);
        Task<ServiceResponse<int>> GetAmountOfProjects();
        Task<ServiceResponse<List<Subproject>>> GetAllSubprojects();
        Task<ServiceResponse<List<Project>>> GetProjectsInRange(int skip, int take);
    }
}
