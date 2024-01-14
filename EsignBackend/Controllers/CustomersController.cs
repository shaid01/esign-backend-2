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
    [Authorize(Roles = "אדמין,מחדש,מנהל,מנפיק,תומך")]
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
            _logger.Debug("GetAllCustomersInRange");
            return Ok(await _customersService.GetCustomers(skip, take));
        }

        [HttpGet("GetAmountOfCustomers")]
        public async Task<IActionResult> GetAmountOfCustomers()
        {
            _logger.Debug("GetAmountOfCustomers");
            return Ok(await _customersService.GetAmountOfCustomers());
        }
        
        [HttpGet("GetCustomerById")]
        public async Task<IActionResult> GetCustomerById(int id)
        {
            _logger.Debug("GetCustomerById");
            return Ok(await _customersService.GetCustomerById(id));
        }

        [HttpGet("GetSecurityQuestions")]
        public async Task<IActionResult> GetSecurityQuestions()
        {
            _logger.Debug("GetSecurityQuestions");
            return Ok(await _customersService.GetSecurityQuestions());
        }
        [Authorize(Roles = "אדמין,מנהל")]
        [HttpPut("UpdateCustomer")]
        public async Task<IActionResult> UpdateUser(Customer updatedCustomer)
        {
            _logger.Debug("UpdateUser");
            return Ok(await _customersService.UpdateCustomer(updatedCustomer));
        }
        [HttpPost("SearchCustomers")]
        public async Task<IActionResult> SearchCustomers(CustomerAdvancedSearch customerAdvancedSearch, int skip, int take)
        {
            _logger.Debug("SearchCustomers");
            return Ok(await _customersService.SearchCustomers(customerAdvancedSearch, skip, take));
        }
        [Authorize(Roles = "אדמין,מנהל")]
        [HttpPost("AddNewCustomer")]
        public async Task<IActionResult> AddNewCustomer(Customer customer)
        {
            _logger.Debug("AddNewCustomer");
            return Ok(await _customersService.AddNewCustomer(customer));
        }
    }
}