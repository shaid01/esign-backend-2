using EsignBackend.Models;
using EsignBackend.Models.DTOs.Settings;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EsignBackend.Services.SettingsService.CustomerIdentifier
{
    public class CustomerIdentifierService : ICustomerIdentifierService
    {
        private readonly AppDbContext _context;
        private readonly ILogger _logger;
        private static object _locker = new object();

        public CustomerIdentifierService(AppDbContext context, ILogger logger)
        {
            _context = context;
            _logger = logger;
        }
        private int GenerateId()
        {
            _logger.Debug("GenerateId");
            lock (_locker)
            {
                int maxId = _context.Custidents.OrderByDescending(item => item.Id).Take(1).ToList()[0].Id;
                return maxId + 1;
            }

        }
        private bool isTheNameAlreadyInUse(string title)
        {
            _logger.Debug("isTheNameAlreadyInUse");
            return _context.Custidents.Where(item => item.Title.Equals(title)).Count() != 0;
        }
        public async Task<ServiceResponse<int>> AddNewCustomerIdentifier(Custident customerIdentifer)
        {
            _logger.Debug("AddNewCertificatesStatus");
            var serviceRespone = new ServiceResponse<int>();
            var nameIsAlreadyTaken = isTheNameAlreadyInUse(customerIdentifer.Title);
            if (nameIsAlreadyTaken)
            {
                serviceRespone.Success = false;
                serviceRespone.Message = "CustomerIdentifer name is already taken";
                serviceRespone.Data = -1;
                return serviceRespone;
            }
            lock (_locker)
            {
                customerIdentifer.Id = GenerateId();
                var newCustomerIdentifierInDb = _context.Custidents.Add(customerIdentifer);
                try
                {
                    _context.SaveChanges();
                    serviceRespone.Data = newCustomerIdentifierInDb.Entity.Id;
                    serviceRespone.Amount = _context.Custidents.Count();
                    return serviceRespone;
                }
                catch (Exception exception)
                {
                    _logger.Error("exception detected while trying to AddNewCertificatesStatus: " + exception);
                    serviceRespone.Success = false;
                    serviceRespone.Message = $"Adding new customerIdentifer failed. {exception}";
                    serviceRespone.Data = -1;
                    return serviceRespone;
                }
            }
        }
        public async Task<ServiceResponse<List<CustidentDTO>>> GetCustomersIdentifiers(int skip, int take)
        {
            _logger.Debug("GetCustomersIdentifiers");
            var serviceResponse = new ServiceResponse<List<CustidentDTO>>();
            var outputList = new List<CustidentDTO>();
            var data = _context.Custidents.Skip(skip).Take(take).ToList();
            foreach (var ci in data)
            {
                outputList.Add(new CustidentDTO(ci));
            }
            serviceResponse.Data = outputList;
            serviceResponse.Amount = _context.Custidents.Count();
            return serviceResponse;
        }
        public async Task<ServiceResponse<int>> UpdateCustomerIdentifer(Custident updatedCustomerIdentifer)
        {
            _logger.Debug("UpdateCustomerIdentifer");
            var serviceResponse = new ServiceResponse<int>();
            var updatedCustomerIdentifierInDb = _context.Custidents.Update(updatedCustomerIdentifer);
            try
            {
                _context.SaveChanges();
                serviceResponse.Success = true;
                serviceResponse.Data = updatedCustomerIdentifierInDb.Entity.Id;
                serviceResponse.Message = "CustomerIdentifer updated successfully.";
                return serviceResponse;
            }
            catch (Exception exception)
            {
                _logger.Error("exception detection while trying to UpdateCustomerIdentifer: " + exception);
                serviceResponse.Success = false;
                serviceResponse.Message = $"Updated failed. {exception}";
                serviceResponse.Data = -1;
                return serviceResponse;
            }
        }
        public async Task<ServiceResponse<List<CustidentDTO>>> GetAllCustomerIdentifiers()
        {
            _logger.Debug("GetAllCustomerIdentifiers");
            var serviceResponse = new ServiceResponse<List<CustidentDTO>>();
            var outputList = new List<CustidentDTO>();
            var data = _context.Custidents.ToList();
            foreach (var ci in data)
            {
                outputList.Add(new CustidentDTO(ci));
            }
            serviceResponse.Amount = _context.Custidents.Count();
            serviceResponse.Data = outputList;
            return serviceResponse;
        }
    }
}
