using EsignBackend.Models;
using EsignBackend.Models.DTOs.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EsignBackend.Services.SettingsService.CustomerIdentifier
{
    public interface ICustomerIdentifierService
    {
        Task<ServiceResponse<List<CustidentDTO>>> GetCustomersIdentifiers(int skip, int take);
        Task<ServiceResponse<int>> UpdateCustomerIdentifer(Custident updatedCustomerIdentifer);
        Task<ServiceResponse<int>> AddNewCustomerIdentifier(Custident customerIdentifer);
        Task<ServiceResponse<List<CustidentDTO>>> GetAllCustomerIdentifiers();
    }
}
