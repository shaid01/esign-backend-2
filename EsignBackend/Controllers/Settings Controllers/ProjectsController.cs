using EsignBackend.Models;
using EsignBackend.Services.SettingsService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EsignBackend.Controllers
{
    [Authorize(Roles = "מנהל,מנפיק,תומך")]
    [ApiController]
    [Route("[controller]")]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectsService _projectsService;

        public ProjectsController(IProjectsService projectsService)
        {
            _projectsService = projectsService;
        }

        [HttpGet("GetProjectsInRange")]
        public IActionResult GetProjectsInRange(int skip, int take)
        {
            return Ok(_projectsService.GetProjectsInRange(skip, take));
        }

        [HttpGet("GetProjects")]
        public IActionResult GetProjects()
        {
            return Ok(_projectsService.GetProjects());
        }

        [Authorize(Roles = "מנהל")]
        [HttpPut("UpdateProject")]
        public async Task<IActionResult> UpdateProject(Project updatedProject)
        {
            return Ok(await _projectsService.UpdateProject(updatedProject));
        }

        [HttpGet("GetSubprojects")]
        public IActionResult GetSubprojectsInRange(int skip, int take)
        {
            return Ok(_projectsService.GetSubprojectsInRange(skip, take));
        }

        [Authorize(Roles = "מנהל")]
        [HttpPut("UpdateSubproject")]
        public async Task<IActionResult> UpdateSubproject(Subproject updatedSubproject)
        {
            return Ok(await _projectsService.UpdateSubproject(updatedSubproject));
        }

        [Authorize(Roles = "מנהל")]
        [HttpPost("AddNewProject")]
        public async Task<IActionResult> AddNewProject(Project newProject)
        {
            return Ok(await _projectsService.AddNewProject(newProject));
        }
        [Authorize(Roles = "מנהל")]
        [HttpPost("AddNewSubproject")]

        public async Task<IActionResult> AddNewSubproject(Subproject newSubproject)
        {
            return Ok(await _projectsService.AddNewSubroject(newSubproject));
        }

        [HttpGet("GetAllSubprojects")]
        public IActionResult GetAllSubprojects(int projectId = -1)
        {
            return Ok(_projectsService.GetAllSubprojects(projectId));
        }
    }
}
