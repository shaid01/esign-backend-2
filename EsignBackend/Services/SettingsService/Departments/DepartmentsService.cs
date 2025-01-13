using DocumentFormat.OpenXml.Bibliography;
using EsignBackend.Models;
using EsignBackend.Models.DTOs;
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

        private bool isNameAlreadyInUse(string title)
        {
            return _context.Departments.Where(item => item.Title.Equals(title)).Count() != 0;
        }

        public async Task<ServiceResponse<int>> AddNewDepartment(Models.Department department)
        {
            _logger.Debug($"Add new Department {department.Id}, {department.Title}");

            var serviceResponse = new ServiceResponse<int>();

            var nameIsAlreadyTaken = isNameAlreadyInUse(department.Title);

            if (nameIsAlreadyTaken)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Department name is already taken";
                serviceResponse.Data = -1;
                return serviceResponse;
            }

            lock (_locker)
            {
                //department.Id = GenerateId();
                var newDepartmentInDb = _context.Departments.Add(new Models.Department() { Title = department.Title });
                try
                {
                    _context.SaveChanges();
                    serviceResponse.Data = newDepartmentInDb.Entity.Id;
                    serviceResponse.Amount = _context.Departments.Where(x => !string.IsNullOrWhiteSpace(x.Title)).Count();
                    serviceResponse.Success = true;
                    return serviceResponse;
                }
                catch (Exception exception)
                {
                    _logger.Error("exception detected while trying to AddNewDepartment: " + exception);
                    serviceResponse.Success = false;
                    serviceResponse.Message = $"Adding new department failed. {exception}";
                    serviceResponse.Data = -1;
                    return serviceResponse;
                }
            }
        }

        public async Task<ServiceResponse<int>> GetAmountOfDepartments()
        {
            _logger.Debug("GetAmountOfDepartments");

            var serviceResponse = new ServiceResponse<int>();
            serviceResponse.Data = _context.Departments.Where(x => !string.IsNullOrWhiteSpace(x.Title)).Count();

            return serviceResponse;
        }

        public ServiceResponse<List<DepartmentDTO>> GetDepartments(int skip, int take)
        {
            _logger.Debug("GetDepartments");

            //var serviceResponse = new ServiceResponse<List<DepartmentDTO>>();
            //var outputList = new List<DepartmentDTO>();

            //var query = _context.Departments.AsNoTracking();
            //query = query.Skip(skip);
            //if (take != UNLIMITED)
            //{
            //    query = query.Take(take);
            //}
            //var data = await query.ToListAsync();

            //foreach (var d in data)
            //{
            //    outputList.Add(new DepartmentDTO(d));
            //}
            //serviceResponse.Amount = _context.Departments.Count();
            //serviceResponse.Data = outputList;
            //return serviceResponse;

            var serviceResponse = new ServiceResponse<List<DepartmentDTO>>();
            serviceResponse.Amount = _context.Departments.Where(x => !string.IsNullOrWhiteSpace(x.Title)).Count();

            List<Models.Department> depts = null;

            try
            {
                if (take != UNLIMITED)
                {
                    depts = _context.Departments.Where(x => !string.IsNullOrWhiteSpace(x.Title))
                    .Skip(skip)
                    .Take(take)
                    .ToList();

                }
                else
                {
                    depts = _context.Departments.Where(x => !string.IsNullOrWhiteSpace(x.Title))
                    .Skip(skip)
                    .ToList();
                }
            }
            catch (Exception ex)
            {
                _logger.Error($"Error while processing DB query in GetDepartments. {ex.Message}");

                serviceResponse.Data = null;
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;

                return serviceResponse;
            }

            var outputList = new List<DepartmentDTO>();

            foreach (var dept in depts)
            {
                outputList.Add(new DepartmentDTO(dept));
            }

            serviceResponse.Success = true;
            serviceResponse.Data = outputList;

            return serviceResponse;
        }

        public async Task<ServiceResponse<int>> UpdateDepartment(Models.Department updatedDepartment)
        {
            _logger.Debug("UpdateDepartment");

            var serviceResponse = new ServiceResponse<int>();

            var nameIsAlreadyTaken = isNameAlreadyInUse(updatedDepartment.Title);

            if (nameIsAlreadyTaken)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Department name is already taken";
                serviceResponse.Data = -1;
                return serviceResponse;
            }

            var updatedDepartmentInDb = _context.Departments.Update(updatedDepartment);

            try
            {
                _context.SaveChanges();
                serviceResponse.Data = updatedDepartmentInDb.Entity.Id;
                serviceResponse.Message = "Department updated successfully.";
                serviceResponse.Success = true;
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
