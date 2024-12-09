using EsignBackend.Models;
using EsignBackend.Services.SettingsService.Departments;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EsignBackend.Controllers.Settings_Controllers
{
    [Authorize(Roles = "מנהל,מנפיק,תומך")]
    [ApiController]
    [Route("[controller]")]
    public class DepartmentsController : ControllerBase
    {
        private readonly IDepartmentsService _departmentService;
        public DepartmentsController(IDepartmentsService departmentsService)
        {
            _departmentService = departmentsService;
        }

        [HttpGet("GetDepartments")]
        public async Task<IActionResult> GetDepartments(int skip, int take)
        {
            return Ok(await _departmentService.GetDepartments(skip, take));
        }
        [Authorize(Roles = "מנהל")]
        [HttpPut("UpdateDepartment")]
        public async Task<IActionResult> UpdateDepartment(Department updatedDepartment)
        {
            return Ok(await _departmentService.UpdateDepartment(updatedDepartment));
        }
        [Authorize(Roles = "מנהל")]
        [HttpPost("AddNewDepartment")]
        public async Task<IActionResult> AddNewDepartment(Department department)
        {
            return Ok(await _departmentService.AddNewDepartment(department));
        }
    }
}
