using EsignBackend.Models;
using EsignBackend.Models.Tools;
using EsignBackend.Services.CharacterService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EsignBackend.Controllers
{
    [Authorize(Roles = "מנהל,מנפיק,תומך")]
    [ApiController]
    [Route("[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomersService _customersService;
        private readonly ILogger _logger;

        public CustomersController(ICustomersService customersService, ILogger logger)
        {
            _customersService = customersService;
            _logger = logger;
        }

        [HttpGet("GetCustomersInRange")]
        public async Task<IActionResult> GetAllCustomersInRange(int skip, int take)
        {
            return Ok(await _customersService.GetCustomers(skip, take));
        }

        [HttpGet("GetAmountOfCustomers")]
        public async Task<IActionResult> GetAmountOfCustomers()
        {
            return Ok(await _customersService.GetAmountOfCustomers());
        }
        
        [HttpGet("GetCustomerById")]
        public IActionResult GetCustomerById(int id)
        {
            return Ok(_customersService.GetCustomerById(id));
        }

        [HttpGet("GetSecurityQuestions")]
        public async Task<IActionResult> GetSecurityQuestions()
        {
            return Ok(await _customersService.GetSecurityQuestions());
        }

        [Authorize(Roles = "מנהל")]
        [HttpPut("UpdateCustomer")]
        public async Task<IActionResult> UpdateCustomer(Customer updatedCustomer)
        {
            return Ok(await _customersService.UpdateCustomer(updatedCustomer));
        }

        [Authorize(Roles = "מנהל")]
        [HttpPut("DeleteCustomers")]
        public async Task<IActionResult> DeleteCustomers(List<Customer> custsToDelete)
        {
            return Ok(await _customersService.DeleteCustomers(custsToDelete));
        }

        [HttpPost("SearchCustomers")]
        public async Task<IActionResult> SearchCustomers(CustomerAdvancedSearch customerAdvancedSearch, int skip, int take)
        {
            return Ok(await _customersService.SearchCustomers(customerAdvancedSearch, skip, take));
        }

        [Authorize(Roles = "מנהל")]
        [HttpPost("AddNewCustomer")]
        public async Task<IActionResult> AddNewCustomer(Customer customer)
        {
            return Ok(await _customersService.AddNewCustomer(customer));
        }
    }
}