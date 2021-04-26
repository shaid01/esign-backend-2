using EsignBackend.Models;
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

        public DepartmentsService(AppDbContext context, ILogger logger)
        {
            _context = context;
            _logger = logger;
        }
        private int GenerateId()
        {
            _logger.Debug("GenerateId");
            int maxId = _context.Departmants.OrderByDescending(item => item.Id).Take(1).ToList()[0].Id;
            return maxId + 1;
        }
        private bool isTheNameAlreadyInUse(string title)
        {
            _logger.Debug("isTheNameAlreadyInUse");
            return _context.Departmants.Where(item => item.Title.Equals(title)).Count() != 0;
        }
        public async Task<ServiceResponse<int>> AddNewDepartment(Departmant department)
        {
            _logger.Debug("AddNewDepartment");
            var serviceRespone = new ServiceResponse<int>();
            var nameIsAlreadyTaken = isTheNameAlreadyInUse(department.Title);
            if (nameIsAlreadyTaken)
            {
                serviceRespone.Success = false;
                serviceRespone.Message = "Departmant name is already taken";
                serviceRespone.Data = -1;
                return serviceRespone;
            }
            lock (_locker)
            {
                department.Id = GenerateId();
                var newDepartmentInDb = _context.Departmants.Add(department);
                try
                {
                    _context.SaveChanges();
                    serviceRespone.Data = newDepartmentInDb.Entity.Id;
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
            serviceResponse.Data = _context.Departmants.Count();
            return serviceResponse;
        }
        public async Task<ServiceResponse<List<Departmant>>> GetDepartments(int skip, int take)
        {
            _logger.Debug("GetDepartments");
            var serviceResponse = new ServiceResponse<List<Departmant>> ();
            serviceResponse.Data = _context.Departmants.Skip(skip).Take(take).ToList();
            serviceResponse.Message = serviceResponse.Data.Count().ToString();
            return serviceResponse;
        }
        public async Task<ServiceResponse<int>> UpdateDepartment(Departmant updatedDepartment)
        {
            _logger.Debug("UpdateDepartment");
            var serviceResponse = new ServiceResponse<int>();
            var updatedDepartmentInDb = _context.Departmants.Update(updatedDepartment);
            try
            {
                _context.SaveChanges();
                serviceResponse.Success = true;
                serviceResponse.Data = updatedDepartmentInDb.Entity.Id;
                serviceResponse.Message = "Departmant updated successfully.";
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