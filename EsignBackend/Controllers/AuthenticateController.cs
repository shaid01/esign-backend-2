using Azure;
using EsignBackend.Dtos.Login;
using EsignBackend.Models;
using EsignBackend.Services.CharacterService;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace EsignBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticateController : ControllerBase
    {
        private readonly IAuthenticationService _authenticateService;
        private readonly ILogger _logger;

        public AuthenticateController(IAuthenticationService authenticateService,
            ILogger logger)
        {
            _authenticateService = authenticateService;
            _logger = logger;
        }

        //igorz, 12345
        [HttpPost("Login")]
        //[HttpPost]
        //[Route("Login")]

        public async Task<IActionResult> Login([FromBody] LoginDto request)
        {
            //var response = await _authenticateService.Login(request.UserName, request.Password);

            var response = await _authenticateService.Login(request.UserName, request.Password);

            if (response.Success)
            {
                _authenticateService.SetTokensInsideCookie(response.Data.Token, HttpContext);

                response.Data.Token = null;
            }

            return Ok(response);
            //return Ok();

            //if (!response.Success)
            //{
            //    return BadRequest(response);
            //}
            //else
            //{
            //    return Ok(response);
            //}
        }

        [HttpPost("Logout")]
        public async Task<IActionResult> Logout([FromBody] LogoutDto request)
        {
            _authenticateService.DeleteCookie(request.CookieName, HttpContext);

            return Ok(true);
        }
    }
}
