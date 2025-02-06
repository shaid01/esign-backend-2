using EsignBackend.Models;
using EsignBackend.Models.DTOs;
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

        private bool isNameAlreadyInUse(int id, string title)
        {
            if (id >= 0)
            {
                return _context.Custidents.Where(x => x.Title.Equals(title) && x.Id != id).Count() != 0;
            }
            else
            {
                return _context.Custidents.Where(x => x.Title.Equals(title)).Count() != 0;
            }
        }

        public async Task<ServiceResponse<int>> AddNewCustomerIdentifier(Custident customerIdentifer)
        {
            _logger.Debug("AddNewCustomerIdentifier");

            var serviceResponse = new ServiceResponse<int>();

            var nameIsAlreadyTaken = isNameAlreadyInUse(-1, customerIdentifer.Title);

            if (nameIsAlreadyTaken)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "CustomerIdentifer name is already taken";
                serviceResponse.Data = -1;
                return serviceResponse;
            }

            lock (_locker)
            {
                //customerIdentifer.Id = GenerateId();
                customerIdentifer.Id = 0;
                var newCustomerIdentifierInDb = _context.Custidents.Add(customerIdentifer);
                try
                {
                    _context.SaveChanges();
                    serviceResponse.Data = newCustomerIdentifierInDb.Entity.Id;
                    serviceResponse.Amount = _context.Custidents.Where(x => !string.IsNullOrWhiteSpace(x.Title)).Count();
                    serviceResponse.Success = true;
                    return serviceResponse;
                }
                catch (Exception exception)
                {
                    _logger.Error("exception detected while trying to AddNewCertificatesStatus: " + exception);
                    serviceResponse.Success = false;
                    serviceResponse.Message = $"Adding new customerIdentifer failed. {exception}";
                    serviceResponse.Data = -1;
                    return serviceResponse;
                }
            }
        }

        public ServiceResponse<List<CustidentDTO>> GetCustomersIdentifiers(int skip, int take)
        {
            _logger.Debug("GetCustomersIdentifiers");

            //var serviceResponse = new ServiceResponse<List<CustidentDTO>>();
            //var outputList = new List<CustidentDTO>();
            //var data = _context.Custidents.Skip(skip).Take(take).ToList();
            //foreach (var ci in data)
            //{
            //    outputList.Add(new CustidentDTO(ci));
            //}
            //serviceResponse.Data = outputList;
            //serviceResponse.Amount = _context.Custidents.Count();
            //return serviceResponse;

            var serviceResponse = new ServiceResponse<List<CustidentDTO>>();
            serviceResponse.Amount = _context.Custidents.Where(x => !string.IsNullOrWhiteSpace(x.Title)).Count();

            List<Custident> custIds = null;

            try
            {
                custIds = _context.Custidents.Where(x => !string.IsNullOrWhiteSpace(x.Title))
                    .Skip(skip)
                    .Take(take)
                    .ToList();
            }
            catch (Exception ex)
            {
                _logger.Error($"Error while processing DB query in GetCustomersIdentifiers. {ex.Message}");

                serviceResponse.Data = null;
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;

                return serviceResponse;
            }

            var outputList = new List<CustidentDTO>();

            foreach (var custId in custIds)
            {
                outputList.Add(new CustidentDTO(custId));
            }

            serviceResponse.Success = true;
            serviceResponse.Data = outputList;

            return serviceResponse;
        }

        public async Task<ServiceResponse<int>> UpdateCustomerIdentifer(Custident updatedCustomerIdentifer)
        {
            _logger.Debug("UpdateCustomerIdentifer");

            var serviceResponse = new ServiceResponse<int>();

            var nameIsAlreadyTaken = isNameAlreadyInUse(updatedCustomerIdentifer.Id, updatedCustomerIdentifer.Title);

            if (nameIsAlreadyTaken)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "CustomerIdentifer name is already taken";
                serviceResponse.Data = -1;
                return serviceResponse;
            }

            var updatedCustomerIdentifierInDb = _context.Custidents.Update(updatedCustomerIdentifer);

            try
            {
                _context.SaveChanges();
                serviceResponse.Data = updatedCustomerIdentifierInDb.Entity.Id;
                serviceResponse.Message = "CustomerIdentifer updated successfully.";
                serviceResponse.Success = true;
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

        public ServiceResponse<List<CustidentDTO>> GetAllCustomerIdentifiers()
        {
            _logger.Debug("GetAllCustomerIdentifiers");

            var serviceResponse = new ServiceResponse<List<CustidentDTO>>();

            var outputList = new List<CustidentDTO>();

            var data = _context.Custidents.Where(x => !string.IsNullOrWhiteSpace(x.Title)).ToList();

            foreach (var ci in data)
            {
                outputList.Add(new CustidentDTO(ci));
            }

            serviceResponse.Amount = outputList.Count();//_context.Custidents.Where(x => !string.IsNullOrWhiteSpace(x.Title)).Count();
            serviceResponse.Data = outputList;
            return serviceResponse;
        }
    }
}
