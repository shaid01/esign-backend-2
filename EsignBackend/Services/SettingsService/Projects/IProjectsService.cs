using EsignBackend.Models;
using EsignBackend.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EsignBackend.Services.SettingsService
{
    public interface IProjectsService
    {
        Task<ServiceResponse<List<ProjectDTO>>> GetProjects();
        Task<ServiceResponse<List<SubprojectDTO>>> GetSubprojectsInRange(int skip, int take);
        Task<ServiceResponse<int>> UpdateProject(Project updatedProject);
        Task<ServiceResponse<int>> UpdateSubproject(Subproject updatedSubproject);
        Task<ServiceResponse<int>> AddNewProject(Project newProject);
        Task<ServiceResponse<int>> AddNewSubroject(Subproject newSubproject);
        Task<ServiceResponse<List<SubprojectDTO>>> GetAllSubprojects();

        Task<ServiceResponse<List<ProjectDTO>>> GetProjectsInRange(int skip, int take);
    }
}