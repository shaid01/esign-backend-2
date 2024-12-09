using EsignBackend.Models;
using EsignBackend.Services.SettingsService.CustomerIdentifier;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EsignBackend.Controllers.Settings_Controllers
{
    [Authorize(Roles = "מנהל,מנפיק,תומך")]
    [ApiController]
    [Route("[controller]")]
    public class CustomerIdentifierController : ControllerBase
    {
        private readonly ICustomerIdentifierService _customerIdentifierService;
        public CustomerIdentifierController(ICustomerIdentifierService customerIdentifierService)
        {
            _customerIdentifierService = customerIdentifierService;
        }

        [HttpGet("GetCustomersIdentifiers")]
        public async Task<IActionResult> GetCustomersIdentifiers(int skip, int take)
        {
            return Ok(await _customerIdentifierService.GetCustomersIdentifiers(skip, take));
        }

        [Authorize(Roles = "מנהל")]
        [HttpPut("UpdateCustomerIdentifer")]
        public async Task<IActionResult> UpdateCustomerIdentifer(Custident updatedCustomerIdentifer)
        {
            return Ok(await _customerIdentifierService.UpdateCustomerIdentifer(updatedCustomerIdentifer));
        }
        [Authorize(Roles = "מנהל")]
        [HttpPost("AddNewCustomerIdentifer")]
        public async Task<IActionResult> AddNewCertificatesStatus(Custident customerIdentifer)
        {
            return Ok(await _customerIdentifierService.AddNewCustomerIdentifier(customerIdentifer));
        }

        [HttpGet("GetAllCustomerIdentifiers")]
        public async Task<IActionResult> GetAllCustomerIdentifiers()
        {
            return Ok(await _customerIdentifierService.GetAllCustomerIdentifiers());
        }
    }
}