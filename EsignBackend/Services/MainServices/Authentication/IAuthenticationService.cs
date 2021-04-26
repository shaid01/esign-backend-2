using EsignBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EsignBackend.Services.CharacterService
{
    public interface IAuthenticationService
    {
        Task<ServiceResponse<string>> Login(string username,string password);        
    }
}
