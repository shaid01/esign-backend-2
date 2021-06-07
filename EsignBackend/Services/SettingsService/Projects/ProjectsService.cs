using EsignBackend.Models;
using EsignBackend.Models.DTOs;
using Serilog;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace EsignBackend.Services.SettingsService
{
    public class ProjectsService : IProjectsService
    {
        private readonly AppDbContext _context;
        private readonly ILogger _logger;
        private static object _locker = new object();

        public ProjectsService(AppDbContext context, ILogger logger)
        {
            _context = context;
            _logger = logger;
        }
        private enum projectType
        {
            PROJECT,
            SUBPROJECT
        }

        private bool IsTheNameAlreadyInUse(string title, projectType projectType)
        {
            _logger.Debug("IsTheNameAlreadyInUse");
            if (projectType == projectType.SUBPROJECT)
            {
                return _context.Subprojects.Where(subproject => subproject.Title.Equals(title)).Count() != 0;
            }
            else
            {
                return _context.Projects.Where(project => project.Title.Equals(title)).Count() != 0;
            }
        }
        private int GenerateId(projectType projectType)
        {
            _logger.Debug("GenerateId");
            lock (_locker)
            {
                int maxId = 0;
                if (projectType == projectType.SUBPROJECT)
                {
                    maxId = _context.Subprojects.OrderByDescending(subproject => subproject.Id).Take(1).ToList()[0].Id;
                }
                else
                {
                    maxId = _context.Projects.OrderByDescending(project => project.Id).Take(1).ToList()[0].Id;
                }
                return maxId + 1;
            }
        }
        public async Task<ServiceResponse<List<ProjectDTO>>> GetProjects()
        {
            _logger.Debug("GetProjects");
            var serviceResponse = new ServiceResponse<List<ProjectDTO>>();
            var outputList = new List<ProjectDTO>();
            var data = _context.Projects.ToList();
            foreach (var p in data)
            {
                outputList.Add(new ProjectDTO(p));
            }
            serviceResponse.Amount = data.Count();
            serviceResponse.Data = outputList;
            return serviceResponse;
        }
        public async Task<ServiceResponse<int>> UpdateProject(Project updatedProject)
        {
            _logger.Debug("UpdateProject");
            var serviceResponse = new ServiceResponse<int>();
            var updatedProjectInDb = _context.Projects.Update(updatedProject);
            try
            {
                await _context.SaveChangesAsync();
                serviceResponse.Success = true;
                serviceResponse.Data = updatedProjectInDb.Entity.Id;
                serviceResponse.Message = "Project updated successfully.";
            }
            catch (Exception exception)
            {
                _logger.Error("exception detected while trying to UpdateProject: " + exception);
                serviceResponse.Success = false;
                serviceResponse.Message = $"Updated failed. {exception}";
                serviceResponse.Data = -1;
            }
            return serviceResponse;
        }
        public async Task<ServiceResponse<List<SubprojectDTO>>> GetSubprojectsInRange(int skip, int take)
        {
            _logger.Debug("GetSubprojectsInRange");
            var serviceResponse = new ServiceResponse<List<SubprojectDTO>>();
            var outputList = new List<SubprojectDTO>();
            var data = _context.Subprojects.Skip(skip).Take(take).ToList();
            foreach (var sp in data)
            {
                outputList.Add(new SubprojectDTO(sp));
            }
            serviceResponse.Data = outputList;
            serviceResponse.Amount = _context.Subprojects.Count();
            return serviceResponse;
        }
        public async Task<ServiceResponse<int>> UpdateSubproject(Subproject updatedSubproject)
        {
            _logger.Debug("UpdateSubproject");
            var serviceResponse = new ServiceResponse<int>();
            var updatedSubprojectInDb = _context.Subprojects.Update(updatedSubproject);
            try
            {
                await _context.SaveChangesAsync();
                serviceResponse.Success = true;
                serviceResponse.Data = updatedSubprojectInDb.Entity.Id;
                serviceResponse.Message = "Subproject updated successfully.";
            }
            catch (Exception exception)
            {
                _logger.Error("exception detected while trying to UpdateSubproject: " + exception);
                serviceResponse.Success = false;
                serviceResponse.Message = $"Updated failed. {exception}";
                serviceResponse.Data = -1;
            }
            return serviceResponse;
        }
        public async Task<ServiceResponse<int>> AddNewProject(Project newProject)
        {
            _logger.Debug("AddNewProject");
            var serviceRespone = new ServiceResponse<int>();
            var projectNameIsAlreadyTaken = IsTheNameAlreadyInUse(newProject.Title, projectType.PROJECT);
            if (projectNameIsAlreadyTaken)
            {
                serviceRespone.Success = false;
                serviceRespone.Message = "Project name is already taken";
                serviceRespone.Data = -1;
                return serviceRespone;
            }
            lock (_locker)
            {
                //newProject.Id = GenerateId(projectType.PROJECT);
                newProject.Id = 0;
                var newProjectInDb = _context.Projects.Add(newProject);
                try
                {
                    _context.SaveChanges();
                    serviceRespone.Data = newProjectInDb.Entity.Id;
                    serviceRespone.Amount = _context.Projects.Count();
                    return serviceRespone;
                }
                catch (Exception exception)
                {
                    _logger.Error("exception detected while trying to AddNewProject: " + exception);
                    serviceRespone.Success = false;
                    serviceRespone.Message = $"Adding new project failed. {exception}";
                    serviceRespone.Data = -1;
                    return serviceRespone;
                }
            }
        }
        public async Task<ServiceResponse<int>> AddNewSubroject(Subproject newSubproject)
        {
            _logger.Debug("AddNewSubroject");
            var serviceRespone = new ServiceResponse<int>();
            var subprojectNameIsAlreadyTaken = IsTheNameAlreadyInUse(newSubproject.Title, projectType.SUBPROJECT);
            if (subprojectNameIsAlreadyTaken)
            {
                serviceRespone.Success = false;
                serviceRespone.Message = "Project name is already taken";
                serviceRespone.Data = -1;
                return serviceRespone;
            }
            lock (_locker)
            {
                //newSubproject.Id = GenerateId(projectType.SUBPROJECT);
                newSubproject.Id = 0;
                var newSubprojectInDb = _context.Subprojects.Add(newSubproject);
                try
                {
                    _context.SaveChanges();
                    serviceRespone.Data = newSubprojectInDb.Entity.Id;
                    serviceRespone.Amount = _context.Subprojects.Count();
                    return serviceRespone;
                }
                catch (Exception exception)
                {
                    _logger.Error("exception detected while trying to AddNewSubroject: " + exception);
                    serviceRespone.Success = false;
                    serviceRespone.Message = $"Adding new subproject failed. {exception}";
                    serviceRespone.Data = -1;
                    return serviceRespone;
                }
            }
        }
        public async Task<ServiceResponse<List<SubprojectDTO>>> GetAllSubprojects()
        {
            _logger.Debug("GetAllSubprojects");
            var serviceResponse = new ServiceResponse<List<SubprojectDTO>>();
            var outputList = new List<SubprojectDTO>();
            var data = _context.Subprojects.ToList();
            foreach (var sp in data)
            {
                outputList.Add(new SubprojectDTO(sp));
            }
            serviceResponse.Amount = _context.Subprojects.Count();
            serviceResponse.Data = outputList;
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<ProjectDTO>>> GetProjectsInRange(int skip, int take)
        {
            _logger.Debug("GetProjectsInRange");
            var serviceResponse = new ServiceResponse<List<ProjectDTO>>();
            var outputList = new List<ProjectDTO>();
            var data = _context.Projects.Skip(skip).Take(take).ToList();
            foreach (var pro in data)
            {
                outputList.Add(new ProjectDTO(pro));
            }
            serviceResponse.Amount = _context.Projects.Count();
            serviceResponse.Data = outputList;
            return serviceResponse;
        }
    }
}