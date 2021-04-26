using EsignBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EsignBackend.Services.SettingsService.Departments
{
    public interface IDepartmentsService
    {
        Task<ServiceResponse<int>> GetAmountOfDepartments();
        Task<ServiceResponse<List<Departmant>>> GetDepartments(int skip, int take);
        Task<ServiceResponse<int>> UpdateDepartment(Departmant updatedDepartment);
        Task<ServiceResponse<int>> AddNewDepartment(Departmant department);
    }
}
