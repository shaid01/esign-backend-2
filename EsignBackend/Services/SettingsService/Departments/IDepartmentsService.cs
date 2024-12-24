using EsignBackend.Models;
using EsignBackend.Models.DTOs.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EsignBackend.Services.SettingsService.Departments
{
    public interface IDepartmentsService
    {
        ServiceResponse<List<DepartmentDTO>> GetDepartments(int skip, int take);
        Task<ServiceResponse<int>> UpdateDepartment(Department updatedDepartment);
        Task<ServiceResponse<int>> AddNewDepartment(Department department);
    }
}
