using DocumentFormat.OpenXml.InkML;
using DocumentFormat.OpenXml.Spreadsheet;
using EsignBackend.Extensions.CacheHandlers;
using EsignBackend.Extensions.EncryptDecrypt;
using EsignBackend.Models;
using EsignBackend.Models.DTOs;
using EsignBackend.Models.Tools;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EsignBackend.Services.CharacterService
{
    public class CustomersService : ICustomersService
    {
        private readonly AppDbContext _context;
        private readonly ICache _cache;
        private readonly ILogger _logger;
        private static object _locker = new object();
        private const int UNLIMITED = -1;

        public CustomersService(AppDbContext context, ILogger logger, ICache cache)
        {
            _context = context;
            _logger = logger;
            _cache = cache;
        }

        private bool IsUserIdAlreadyInUse(string username)
        {
            _logger.Debug("IsUserIdAlreadyInUse");
            return _context.Customers.Where(customer => customer.Idnumber.Equals(username)).Count() != 0;
        }

        public async Task<ServiceResponse<List<CustomerDTO>>> GetCustomers(int skip, int take)
        {
            _logger.Debug("GetCustomers");

            var serviceRespone = new ServiceResponse<List<CustomerDTO>>();

            List<Customer> customers = null;

            try
            {
                customers = await _context.Customers
                    .Include(c => c.RelatedSecurityquestion)
                    //.Where(c => c.Idnumber != "")
                    .AsNoTracking()

                    //.OrderBy(c => c.Idnumber)
                    .OrderByDescending(c => c.Id)

                    .Skip(skip).Take(take).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.Error($"Error while processing DB query in GetCustomers. {ex.Message}");

                serviceRespone.Data = null;
                serviceRespone.Success = false;
                serviceRespone.Message = ex.Message;

                return serviceRespone;
            }

            var customersList = new List<CustomerDTO>();

            foreach (var customer in customers)
            {
                customersList.Add(new CustomerDTO(customer));
            }

            serviceRespone.Success = true;
            serviceRespone.Data = customersList;
            serviceRespone.Amount = _cache.GetCounterByType(CacheType.Customers);

            return serviceRespone;
        }

        public async Task<ServiceResponse<IEnumerable<CustomerDTO>>> SearchCustomers(CustomerAdvancedSearch customerAdvancedSearch, int skip, int take)
        {
            _logger.Debug("SearchCustomers");

            var serviceResponse = new ServiceResponse<IEnumerable<CustomerDTO>>();

            var query = _context.Customers.AsNoTracking().Include(cu => cu.RelatedSecurityquestion).Where(customer =>
                (customer.Id > 0)

                && ((customerAdvancedSearch.CustomerId == null) || EF.Functions.Like(customer.Idnumber, $"%{customerAdvancedSearch.CustomerId}%"))

                && ((customerAdvancedSearch.FirstName == null) || EF.Functions.Like(customer.Firstname, $"%{customerAdvancedSearch.FirstName}%"))

                && ((customerAdvancedSearch.LastName == null) || EF.Functions.Like(customer.Lastname, $"%{customerAdvancedSearch.LastName}%"))

                && ((customerAdvancedSearch.Email == null) || EF.Functions.Like(customer.Email, $"%{customerAdvancedSearch.Email}%"))

                && ((customerAdvancedSearch.Company == null) || EF.Functions.Like(customer.Company, $"%{customerAdvancedSearch.Company}%"))

                && (((customerAdvancedSearch.Phone == null) || EF.Functions.Like(customer.Phone1, $"%{customerAdvancedSearch.Phone}%"))

                    || ((customerAdvancedSearch.Phone == null) || EF.Functions.Like(customer.Mobile1, $"%{customerAdvancedSearch.Phone}%")))

                );

            //query = query.OrderByDescending(cust => cust.Id);
            query = query.OrderByDescending(cust => EF.Property<object>(cust, "Id"));

            List<Customer> customerList = null;

            try
            {
                serviceResponse.Amount = await query.CountAsync();

                query = query.Skip(skip);

                if (take != UNLIMITED)
                {
                    query = query.Take(take);
                }

                customerList = await query.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.Error($"Error while processing DB query in SearchCustomers. {ex.Message}");

                serviceResponse.Data = null;
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;

                return serviceResponse;
            }

            var customersDtoList = new List<CustomerDTO>();

            foreach (var customer in customerList)
            {
                customersDtoList.Add(new CustomerDTO(customer));
            }

            serviceResponse.Success = true;
            serviceResponse.Data = customersDtoList;

            return serviceResponse;
        }

        public async Task<ServiceResponse<int>> GetAmountOfCustomers()
        {
            _logger.Debug("GetAmountOfCustomers");
            var serviceRespone = new ServiceResponse<int>();
            serviceRespone.Data = _cache.GetCounterByType(CacheType.Customers);
            return serviceRespone;
        }

        public CustomerDTO GetCustomerById(int id)
        {
            _logger.Debug("GetCustomerById");

            //var serviceResponse = new ServiceResponse<CustomerDTO>();
            //serviceResponse.Amount = _cache.GetCounterByType(CacheType.Customers);

            //await _context.Customers.FirstOrDefaultAsync(c => c.Id == id);
            var customer = _context.Customers
                .Where(x => x.Id == id)
                //First will throw an exception when there are no results. 
                .First();

            //if (customer == null)
            //{
            //    serviceResponse.Amount = 0;
            //    _logger.Error($"Not found customer by Id = {id}");
            //    return serviceResponse;
            //}

            //serviceResponse.Amount = 1;
            //serviceResponse.Data = new CustomerDTO(customer);

            var customerDto = new CustomerDTO(customer);

            //return serviceRespone;
            return customerDto;
        }

        public async Task<ServiceResponse<List<Securityquestion>>> GetSecurityQuestions()
        {
            _logger.Debug("GetSecurityQuestions");
            var serviceResponse = new ServiceResponse<List<Securityquestion>>();
            var dbSecurityQuestions = await _context.Securityquestions.ToListAsync();
            serviceResponse.Data = dbSecurityQuestions;
            return serviceResponse;
        }

        public async Task<ServiceResponse<int>> UpdateCustomer(Customer updatedCustomer)
        {
            _logger.Debug("UpdateCustomer");
            var serviceResponse = new ServiceResponse<int>();
            var updatedCustomerInDb = _context.Customers.Update(updatedCustomer);
            try
            {
                _context.SaveChanges();
                serviceResponse.Success = true;
                serviceResponse.Data = updatedCustomerInDb.Entity.Id;
                serviceResponse.Message = "Customer updated successfully.";
                return serviceResponse;
            }
            catch (Exception exception)
            {
                _logger.Error("exception detected while trying to UpdateCustomer: " + exception);
                serviceResponse.Success = false;
                serviceResponse.Message = $"Updated failed. {exception}";
                serviceResponse.Data = -1;
                return serviceResponse;
            }
        }

        public async Task<ServiceResponse<int>> AddNewCustomer(Customer customer)
        {
            _logger.Debug("AddNewCustomer");

            var serviceRespone = new ServiceResponse<int>();
            var customerIdAlreadyInDb = IsUserIdAlreadyInUse(customer.Idnumber);
            if (customerIdAlreadyInDb)
            {
                serviceRespone.Success = false;
                serviceRespone.Message = $"Customer {customer.Idnumber} is already exists";
                _logger.Error($"Customer {customer.Idnumber} is already exists");
                serviceRespone.Data = -1;
                return serviceRespone;
            }

            lock (_locker)
            {
                //customer.Id = GenerateUserId();
                customer.Id = 0;
                var encryptedSecurityAns = EncryptDecryptHandler.encryptSecurityAns(customer.Securityansware);
                customer.Securityansware = encryptedSecurityAns;
                var newCustomerInDb = _context.Customers.Add(customer);
                try
                {
                    _context.SaveChanges();
                    serviceRespone.Data = newCustomerInDb.Entity.Id;
                    _cache.Increment(CacheType.Customers);
                    return serviceRespone;
                }
                catch (Exception exception)
                {
                    _logger.Error("Exception detected while trying to AddNewCustomer: " + exception);
                    serviceRespone.Success = false;
                    serviceRespone.Message = $"Registration failed. {exception}";
                    serviceRespone.Data = -1;
                    return serviceRespone;
                }
            }
        }
    }
}