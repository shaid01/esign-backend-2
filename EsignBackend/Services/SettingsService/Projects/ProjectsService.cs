using EsignBackend.Models;
using Serilog;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace EsignBackend.Services.SettingsService
{
    public class ProjectsService:IProjectsService
    {
        private readonly AppDbContext _context;
        private readonly ILogger _logger;
        private static object _locker = new object();

        public ProjectsService(AppDbContext context, ILogger logger)
        {
            _context = context;
            _logger = logger;
        }
        private enum projectType{
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
        public async Task<ServiceResponse<List<Project>>> GetProjects()
        {
            _logger.Debug("GetProjects");
            var serviceResponse = new ServiceResponse<List<Project>>();
            serviceResponse.Data =  _context.Projects.ToList();
            serviceResponse.Message = serviceResponse.Data.Count().ToString();
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
        public async Task<ServiceResponse<List<Subproject>>> GetSubprojectsInRange(int skip, int take)
        {
            _logger.Debug("GetSubprojectsInRange");
            var serviceResponse = new ServiceResponse<List<Subproject>>();
            serviceResponse.Data =  _context.Subprojects.Skip(skip).Take(take).ToList(); 
            serviceResponse.Message = serviceResponse.Data.Count().ToString();
            return serviceResponse;
        }
        public async Task<ServiceResponse<int>> GetAmountOfSubprojects()
        {
            _logger.Debug("GetAmountOfSubprojects");
            var serviceResponse = new ServiceResponse<int>();
            serviceResponse.Data = _context.Subprojects.Count();
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
            var projectNameIsAlreadyTaken = IsTheNameAlreadyInUse(newProject.Title,projectType.PROJECT);
            if (projectNameIsAlreadyTaken)
            {
                serviceRespone.Success = false;
                serviceRespone.Message = "Project name is already taken";
                serviceRespone.Data = -1;
                return serviceRespone;
            }
            lock (_locker)
            {
                newProject.Id = GenerateId(projectType.PROJECT);
                var newProjectInDb = _context.Projects.Add(newProject);
                try
                {
                    _context.SaveChanges();
                    serviceRespone.Data = newProjectInDb.Entity.Id;
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
            var subprojectNameIsAlreadyTaken = IsTheNameAlreadyInUse(newSubproject.Title,projectType.SUBPROJECT);
            if (subprojectNameIsAlreadyTaken)
            {
                serviceRespone.Success = false;
                serviceRespone.Message = "Project name is already taken";
                serviceRespone.Data = -1;
                return serviceRespone;
            }
            lock (_locker)
            {
                newSubproject.Id = GenerateId(projectType.SUBPROJECT);
                var newSubprojectInDb = _context.Subprojects.Add(newSubproject);
                try
                {
                    _context.SaveChanges();
                    serviceRespone.Data = newSubprojectInDb.Entity.Id;
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
        public async Task<ServiceResponse<List<Subproject>>> GetAllSubprojects()
        {
            _logger.Debug("GetAllSubprojects");
            var serviceResponse = new ServiceResponse<List<Subproject>>();
            serviceResponse.Data = _context.Subprojects.ToList();
            serviceResponse.Message = serviceResponse.Data.Count().ToString();
            return serviceResponse;
        }
        public async Task<ServiceResponse<int>> GetAmountOfProjects()
        {
            _logger.Debug("GetAmountOfProjects");
            var serviceResponse = new ServiceResponse<int>();
            serviceResponse.Data = _context.Projects.Count();
            return serviceResponse;
        }
        public async Task<ServiceResponse<List<Project>>> GetProjectsInRange(int skip, int take)
        {
            _logger.Debug("GetProjectsInRange");
            var serviceResponse = new ServiceResponse<List<Project>>();
            serviceResponse.Data = _context.Projects.Skip(skip).Take(take).ToList();
            serviceResponse.Message = serviceResponse.Data.Count().ToString();
            return serviceResponse;
        }
    }
}