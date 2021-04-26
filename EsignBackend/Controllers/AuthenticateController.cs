using EsignBackend.Dtos.Login;
using EsignBackend.Models;
using EsignBackend.Services.CharacterService;
using Microsoft.AspNetCore.Mvc;
using Serilog;
//using Swashbuckle.AspNetCore.Annotations;
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

        [HttpPost("Login")]
       /* [SwaggerResponse((int)HttpStatusCode.InternalServerError, Type = typeof(ServiceResponse<string>))]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, Type = typeof(ServiceResponse<string>))]*/
        public async Task<IActionResult> Login(LoginDto request)
        {
            _logger.Debug("Login");
            
            var response = await _authenticateService.Login(
                request.UserName, request.Password);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}