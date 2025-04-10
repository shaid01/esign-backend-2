using EsignBackend.Dtos.Login;
using EsignBackend.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EsignBackend.Services.CharacterService
{
    public interface IAuthenticationService
    {
        Task<ServiceResponse<UserClaimsDataDto>> Login(string username,string password);
        void SetTokensInsideCookie(string token, HttpContext context);
        void DeleteCookie(string cookieName, HttpContext context);
    }
}
