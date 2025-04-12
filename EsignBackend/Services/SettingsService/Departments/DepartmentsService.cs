using DocumentFormat.OpenXml.Bibliography;
using EsignBackend.Migrations;
using EsignBackend.Models;
using EsignBackend.Models.DTOs;
using EsignBackend.Models.DTOs.Settings;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
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

        private bool isNameAlreadyInUse(int id, string title)
        {
            if (id >= 0)
            {
                return _context.Departments.Where(x => x.Title.Equals(title) && x.Id != id).Count() != 0;
            }
            else
            {
                return _context.Departments.Where(x => x.Title.Equals(title)).Count() != 0;
            }
        }

        public async Task<ServiceResponse<int>> AddNewDepartment(Models.Department department)
        {
            _logger.Information($"Add new department: {department.Title}");

            var serviceResponse = new ServiceResponse<int>();

            var nameIsAlreadyTaken = isNameAlreadyInUse(-1, department.Title);

            if (nameIsAlreadyTaken)
            {
                _logger.Warning($"Add new department: {department.Title}. Department name is already taken");

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
                    _logger.Error($"Add new department: {department.Title} exception: {exception}");

                    serviceResponse.Success = false;
                    serviceResponse.Message = $"Adding new department failed. {exception}";
                    serviceResponse.Data = -1;
                    return serviceResponse;
                }
            }
        }

        public async Task<ServiceResponse<int>> GetAmountOfDepartments()
        {
            _logger.Debug("Get amount of departments");

            var serviceResponse = new ServiceResponse<int>();
            serviceResponse.Data = _context.Departments.Where(x => !string.IsNullOrWhiteSpace(x.Title)).Count();

            return serviceResponse;
        }

        public ServiceResponse<List<DepartmentDTO>> GetDepartments(int skip, int take)
        {
            _logger.Debug($"Get departments: skip - {skip}, take - {take}");

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
                _logger.Error($"Get departments error: {ex}");

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
            _logger.Information($"Update department ID {updatedDepartment.Id} to {updatedDepartment.Title}");

            var serviceResponse = new ServiceResponse<int>();

            var nameIsAlreadyTaken = isNameAlreadyInUse(updatedDepartment.Id, updatedDepartment.Title);

            if (nameIsAlreadyTaken)
            {
                _logger.Warning($"Update department ID {updatedDepartment.Id} to {updatedDepartment.Title}. Department name is already taken.");

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
                _logger.Error("Exception detected while trying to UpdateDepartment: " + exception);

                serviceResponse.Success = false;
                serviceResponse.Message = $"Updated failed. {exception}";
                serviceResponse.Data = -1;
                return serviceResponse;
            }
        }
    }
}
