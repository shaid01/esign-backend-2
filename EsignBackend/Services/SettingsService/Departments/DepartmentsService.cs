using EsignBackend.Models;
using EsignBackend.Models.DTOs.Settings;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EsignBackend.Services.SettingsService.Departments
{
    public class DepartmentsService : IDepartmentsService
    {
        private readonly AppDbContext _context;
        private readonly ILogger _logger;
        private static object _locker = new object();
        private const int UNLIMITED = -1;

        public DepartmentsService(AppDbContext context, ILogger logger)
        {
            _context = context;
            _logger = logger;
        }
        private int GenerateId()
        {
            _logger.Debug("GenerateId");
            lock (_locker)
            {
                int maxId = _context.Departments.OrderByDescending(item => item.Id).Take(1).ToList()[0].Id;
                return maxId + 1;
            }
        }
        private bool isTheNameAlreadyInUse(string title)
        {
            _logger.Debug("isTheNameAlreadyInUse");
            return _context.Departments.Where(item => item.Title.Equals(title)).Count() != 0;
        }
        public async Task<ServiceResponse<int>> AddNewDepartment(Department department)
        {
            _logger.Debug($"Add new Department {department.Id}, {department.Title}");
            _logger.Debug("AddNewDepartment");
            var serviceRespone = new ServiceResponse<int>();
            var nameIsAlreadyTaken = isTheNameAlreadyInUse(department.Title);
            if (nameIsAlreadyTaken)
            {
                serviceRespone.Success = false;
                serviceRespone.Message = "Department name is already taken";
                serviceRespone.Data = -1;
                return serviceRespone;
            }
            lock (_locker)
            {
                //department.Id = GenerateId();
                var newDepartmentInDb = _context.Departments.Add(new Department() { Title = department.Title });
                try
                {
                    _context.SaveChanges();
                    serviceRespone.Data = newDepartmentInDb.Entity.Id;
                    serviceRespone.Amount = _context.Departments.Count();
                    return serviceRespone;
                }
                catch (Exception exception)
                {
                    _logger.Error("exception detected while trying to AddNewDepartment: " + exception);
                    serviceRespone.Success = false;
                    serviceRespone.Message = $"Adding new department failed. {exception}";
                    serviceRespone.Data = -1;
                    return serviceRespone;
                }
            }
        }
        public async Task<ServiceResponse<int>> GetAmountOfDepartments()
        {
            _logger.Debug("GetAmountOfDepartments");
            var serviceResponse = new ServiceResponse<int>();
            serviceResponse.Data = _context.Departments.Count();
            return serviceResponse;
        }
        public async Task<ServiceResponse<List<DepartmentDTO>>> GetDepartments(int skip, int take)
        {
            _logger.Debug("GetDepartments");
            var serviceResponse = new ServiceResponse<List<DepartmentDTO>>();
            var outputList = new List<DepartmentDTO>();

            var query = _context.Departments.AsNoTracking();
            query = query.Skip(skip);
            if (take != UNLIMITED)
            {
                query = query.Take(take);
            }
            var data = await query.ToListAsync();

            foreach (var d in data)
            {
                outputList.Add(new DepartmentDTO(d));
            }
            serviceResponse.Amount = _context.Departments.Count();
            serviceResponse.Data = outputList;
            return serviceResponse;
        }
        public async Task<ServiceResponse<int>> UpdateDepartment(Department updatedDepartment)
        {
            _logger.Debug("UpdateDepartment");
            var serviceResponse = new ServiceResponse<int>();
            var updatedDepartmentInDb = _context.Departments.Update(updatedDepartment);
            try
            {
                _context.SaveChanges();
                serviceResponse.Success = true;
                serviceResponse.Data = updatedDepartmentInDb.Entity.Id;
                serviceResponse.Message = "Department updated successfully.";
                return serviceResponse;
            }
            catch (Exception exception)
            {
                _logger.Error("exception detected while trying to UpdateDepartment: " + exception);
                serviceResponse.Success = false;
                serviceResponse.Message = $"Updated failed. {exception}";
                serviceResponse.Data = -1;
                return serviceResponse;
            }
        }
    }
}
