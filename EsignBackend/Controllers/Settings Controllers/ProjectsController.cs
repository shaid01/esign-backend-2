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
    [Authorize(Roles = "אדמין,מחדש,מנהל,מנפיק,תומך")]
    [ApiController]
    [Route("[controller]")]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectsService _projectsService;

        public ProjectsController(IProjectsService projectsService)
        {
            _projectsService = projectsService;
        }

        [HttpGet("GetAmountOfProjects")]
        public async Task<IActionResult> GetAmountOfProjects()
        {
            return Ok(await _projectsService.GetAmountOfProjects());
        }

        [HttpGet("GetProjectsInRange")]
        public async Task<IActionResult> GetProjectsInRange(int skip, int take)
        {
            return Ok(await _projectsService.GetProjectsInRange(skip, take));
        }

        [HttpGet("GetProjects")]
        public async Task<IActionResult> GetProjects()
        {
            return Ok(await _projectsService.GetProjects());
        }
        [Authorize(Roles = "אדמין,מנהל")]
        [HttpPut("UpdateProject")]
        public async Task<IActionResult> UpdateProject(Project updatedProject)
        {
            return Ok(await _projectsService.UpdateProject(updatedProject));
        }

        [HttpGet("GetSubprojects")]
        public async Task<IActionResult> GetSubprojectsInRange(int skip, int take)
        {
            return Ok(await _projectsService.GetSubprojectsInRange(skip, take));
        }

        [HttpGet("GetAmountOfSubprojects")]
        public async Task<IActionResult> GetAmountOfSubprojects()
        {           
            return Ok(await _projectsService.GetAmountOfSubprojects());
        }
        [Authorize(Roles = "אדמין,מנהל")]
        [HttpPut("UpdateSubproject")]
        public async Task<IActionResult> UpdateSubproject(Subproject updatedSubproject)
        {
            return Ok(await _projectsService.UpdateSubproject(updatedSubproject));
        }
        [Authorize(Roles = "אדמין,מנהל")]
        [HttpPost("AddNewProject")]
        public async Task<IActionResult> AddNewProject(Project newProject)
        {
            return Ok(await _projectsService.AddNewProject(newProject));
        }
        [Authorize(Roles = "אדמין,מנהל")]
        [HttpPost("AddNewSubproject")]
        public async Task<IActionResult> AddNewSubproject(Subproject newSubproject)
        {
            return Ok(await _projectsService.AddNewSubroject(newSubproject));
        }

        [HttpGet("GetAllSubprojects")]
        public async Task<IActionResult> GetAllSubprojects()
        {          
            return Ok(await _projectsService.GetAllSubprojects());
        }
    }
}