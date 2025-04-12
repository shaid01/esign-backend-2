using EsignBackend.Migrations;
using EsignBackend.Models;
using EsignBackend.Models.DTOs;
using Serilog;
using System;
using System.Collections.Generic;
//using System.Data.Entity;
using System.Linq;
using System.Net.Mail;
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

        private bool isNameAlreadyInUse(int id, string title, projectType projectType)
        {
            if (projectType == projectType.SUBPROJECT)
            {
                if (id >= 0)
                {
                    return _context.Subprojects.Where(x => x.Title.Equals(title) && x.Id != id).Count() != 0;
                }
                else
                {
                    return _context.Subprojects.Where(x => x.Title.Equals(title)).Count() != 0;
                }
            }
            else
            {
                if (id >= 0)
                {
                    return _context.Projects.Where(x => x.Title.Equals(title) && x.Id != id).Count() != 0;
                }
                else
                {
                    return _context.Projects.Where(project => project.Title.Equals(title)).Count() != 0;
                }
            }
        }

        public ServiceResponse<List<ProjectDTO>> GetProjects()
        {
            _logger.Debug("Get projects");

            var serviceResponse = new ServiceResponse<List<ProjectDTO>>();

            var outputList = new List<ProjectDTO>();

            var data = _context.Projects.Where(x => !string.IsNullOrWhiteSpace(x.Title)).ToList();

            foreach (var p in data)
            {
                outputList.Add(new ProjectDTO(p));
            }

            serviceResponse.Amount = outputList.Count();
            serviceResponse.Data = outputList;
            return serviceResponse;
        }

        public async Task<ServiceResponse<int>> UpdateProject(Project updatedProject)
        {
            _logger.Information($"Update project ID {updatedProject.Id} to {updatedProject.Title}");

            var serviceResponse = new ServiceResponse<int>();

            var projectNameIsAlreadyTaken = isNameAlreadyInUse(updatedProject.Id, updatedProject.Title, projectType.PROJECT);

            if (projectNameIsAlreadyTaken)
            {
                _logger.Warning($"Update project ID {updatedProject.Id} to {updatedProject.Title}. Project name is already taken.");

                serviceResponse.Success = false;
                serviceResponse.Message = "Project name is already taken";
                serviceResponse.Data = -1;
                return serviceResponse;
            }

            var updatedProjectInDb = _context.Projects.Update(updatedProject);

            try
            {
                await _context.SaveChangesAsync();
                serviceResponse.Data = updatedProjectInDb.Entity.Id;
                serviceResponse.Message = "Project updated successfully.";
                serviceResponse.Success = true;
            }
            catch (Exception exception)
            {
                _logger.Error("Exception detected while trying to UpdateProject: " + exception);

                serviceResponse.Success = false;
                serviceResponse.Message = $"Updated failed. {exception}";
                serviceResponse.Data = -1;
            }
            return serviceResponse;
        }

        public ServiceResponse<List<SubprojectDTO>> GetSubprojectsInRange(int skip, int take)
        {
            _logger.Debug($"Get subprojects in range: skip - {skip}, take - {take}");

            //var serviceResponse = new ServiceResponse<List<SubprojectDTO>>();
            //var outputList = new List<SubprojectDTO>();
            //var data = _context.Subprojects.Skip(skip).Take(take).ToList();
            //foreach (var sp in data)
            //{
            //    outputList.Add(new SubprojectDTO(sp));
            //}
            //serviceResponse.Data = outputList;
            //serviceResponse.Amount = _context.Subprojects.Count();
            //return serviceResponse;

            var serviceResponse = new ServiceResponse<List<SubprojectDTO>>();
            serviceResponse.Amount = _context.Subprojects.Where(x => !string.IsNullOrWhiteSpace(x.Title)).Count();

            List<Subproject> subProjects = null;

            try
            {
                subProjects = _context.Subprojects.Where(x => !string.IsNullOrWhiteSpace(x.Title))
                    .Skip(skip)
                    .Take(take)
                    .ToList();
            }
            catch (Exception ex)
            {
                _logger.Error($"Error while processing DB query in GetSubprojectsInRange: {ex.Message}");

                serviceResponse.Data = null;
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;

                return serviceResponse;
            }

            var outputList = new List<SubprojectDTO>();

            foreach (var subProject in subProjects)
            {
                outputList.Add(new SubprojectDTO(subProject));
            }

            serviceResponse.Success = true;
            serviceResponse.Data = outputList;

            return serviceResponse;
        }

        public ServiceResponse<List<SubprojectDTO>> GetAllSubprojects(int projectId = -1)
        {
            _logger.Debug("Get all subprojects");

            var serviceResponse = new ServiceResponse<List<SubprojectDTO>>();

            var outputList = new List<SubprojectDTO>();

            var data = projectId == -1
                ? _context.Subprojects.Where(x => !string.IsNullOrWhiteSpace(x.Title)).ToList()
                : _context.Subprojects.Where(x => (x.Project == projectId) && !string.IsNullOrWhiteSpace(x.Title)).ToList();

            foreach (var sp in data)
            {
                outputList.Add(new SubprojectDTO(sp));
            }

            serviceResponse.Success = true;
            serviceResponse.Amount = outputList.Count();
            serviceResponse.Data = outputList;
            return serviceResponse;
        }

        public ServiceResponse<List<ProjectDTO>> GetProjectsInRange(int skip, int take)
        {
            _logger.Debug($"Get projects in range: skip - {skip}, take - {take}");

            //var serviceResponse = new ServiceResponse<List<ProjectDTO>>();
            //var outputList = new List<ProjectDTO>();
            //var data = _context.Projects.Skip(skip).Take(take).ToList();
            //foreach (var pro in data)
            //{
            //    outputList.Add(new ProjectDTO(pro));
            //}
            //serviceResponse.Amount = _context.Projects.Count();
            //serviceResponse.Data = outputList;
            //return serviceResponse;

            var serviceResponse = new ServiceResponse<List<ProjectDTO>>();
            serviceResponse.Amount = _context.Projects.Where(x => !string.IsNullOrWhiteSpace(x.Title)).Count();

            List<Project> projects = null;

            try
            {
                projects = _context.Projects.Where(x => !string.IsNullOrWhiteSpace(x.Title))
                    .Skip(skip)
                    .Take(take)
                    .ToList();
            }
            catch (Exception ex)
            {
                _logger.Error($"Error while processing DB query in GetProjectsInRange: {ex.Message}");

                serviceResponse.Data = null;
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;

                return serviceResponse;
            }

            var outputList = new List<ProjectDTO>();

            foreach (var project in projects)
            {
                outputList.Add(new ProjectDTO(project));
            }

            serviceResponse.Success = true;
            serviceResponse.Data = outputList;

            return serviceResponse;
        }

        public async Task<ServiceResponse<int>> UpdateSubproject(Subproject updatedSubproject)
        {
            _logger.Information($"Update subproject ID {updatedSubproject.Id} to {updatedSubproject.Title}");

            var serviceResponse = new ServiceResponse<int>();

            var subprojectNameIsAlreadyTaken = isNameAlreadyInUse(updatedSubproject.Id, updatedSubproject.Title, projectType.SUBPROJECT);

            if (subprojectNameIsAlreadyTaken)
            {
                _logger.Warning($"Update subproject ID {updatedSubproject.Id} to {updatedSubproject.Title}. Subproject name is already taken.");

                serviceResponse.Success = false;
                serviceResponse.Message = "SubProject name is already taken";
                serviceResponse.Data = -1;
                return serviceResponse;
            }

            var updatedSubprojectInDb = _context.Subprojects.Update(updatedSubproject);

            try
            {
                await _context.SaveChangesAsync();
                serviceResponse.Data = updatedSubprojectInDb.Entity.Id;
                serviceResponse.Message = "Subproject updated successfully.";
                serviceResponse.Success = true;
            }
            catch (Exception exception)
            {
                _logger.Error("Exception detected while trying to UpdateSubproject: " + exception);

                serviceResponse.Success = false;
                serviceResponse.Message = $"Updated failed. {exception}";
                serviceResponse.Data = -1;
            }
            return serviceResponse;
        }

        public async Task<ServiceResponse<int>> AddNewProject(Project newProject)
        {
            _logger.Information($"Add new project: {newProject.Title}");

            var serviceResponse = new ServiceResponse<int>();

            var projectNameIsAlreadyTaken = isNameAlreadyInUse(-1, newProject.Title, projectType.PROJECT);

            if (projectNameIsAlreadyTaken)
            {
                _logger.Warning($"Add new project: {newProject.Title}. Project name is already taken");

                serviceResponse.Success = false;
                serviceResponse.Message = "Project name is already taken";
                serviceResponse.Data = -1;
                return serviceResponse;
            }

            lock (_locker)
            {
                //newProject.Id = GenerateId(projectType.PROJECT);
                newProject.Id = 0;

                var newProjectInDb = _context.Projects.Add(newProject);

                try
                {
                    _context.SaveChanges();
                    serviceResponse.Data = newProjectInDb.Entity.Id;
                    serviceResponse.Amount = _context.Projects.Where(x => !string.IsNullOrWhiteSpace(x.Title)).Count();
                    serviceResponse.Success = true;
                    return serviceResponse;
                }
                catch (Exception exception)
                {
                    _logger.Error($"Add new project: {newProject.Title} exception: {exception}");

                    serviceResponse.Success = false;
                    serviceResponse.Message = $"Adding new project failed. {exception}";
                    serviceResponse.Data = -1;
                    return serviceResponse;
                }
            }
        }

        public async Task<ServiceResponse<int>> AddNewSubroject(Subproject newSubproject)
        {
            _logger.Information($"Add new subproject: {newSubproject.Title}");

            var serviceResponse = new ServiceResponse<int>();

            var subprojectNameIsAlreadyTaken = isNameAlreadyInUse(-1, newSubproject.Title, projectType.SUBPROJECT);

            if (subprojectNameIsAlreadyTaken)
            {
                _logger.Warning($"Add new subproject: {newSubproject.Title}. Subproject name is already taken");

                serviceResponse.Success = false;
                serviceResponse.Message = "SubProject name is already taken";
                serviceResponse.Data = -1;
                return serviceResponse;
            }

            lock (_locker)
            {
                //newSubproject.Id = GenerateId(projectType.SUBPROJECT);
                newSubproject.Id = 0;
                var newSubprojectInDb = _context.Subprojects.Add(newSubproject);
                try
                {
                    _context.SaveChanges();
                    serviceResponse.Data = newSubprojectInDb.Entity.Id;
                    serviceResponse.Amount = _context.Subprojects.Where(x => !string.IsNullOrWhiteSpace(x.Title)).Count();
                    serviceResponse.Success = true;
                    return serviceResponse;
                }
                catch (Exception exception)
                {
                    _logger.Error($"Add new subproject: {newSubproject.Title} exception: {exception}");

                    serviceResponse.Success = false;
                    serviceResponse.Message = $"Adding new subproject failed. {exception}";
                    serviceResponse.Data = -1;
                    return serviceResponse;
                }
            }
        }
    }
}