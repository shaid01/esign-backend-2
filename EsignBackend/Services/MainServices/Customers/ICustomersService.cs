using EsignBackend.Models;
using EsignBackend.Models.DTOs;
using EsignBackend.Models.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EsignBackend.Services.CharacterService
{
    public interface ICustomersService
    {
        Task<ServiceResponse<List<CustomerDTO>>> GetCustomers(int skip, int take);
        Task<ServiceResponse<int>> GetAmountOfCustomers();
        CustomerDTO GetCustomerById(int id);
        Task<ServiceResponse<List<Securityquestion>>> GetSecurityQuestions();
        Task<ServiceResponse<int>> UpdateCustomer(Customer updatedCustomer);
        Task<ServiceResponse<IEnumerable<CustomerDTO>>> SearchCustomers(CustomerAdvancedSearch customerAdvancedSearch, int skip, int take);
        Task<ServiceResponse<int>> AddNewCustomer(Customer customer);
    }
}