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
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _usersService;
        private readonly ILogger _logger;

        public UsersController(IUsersService usersService, ILogger logger)
        {
            _usersService = usersService;
            _logger = logger;
        }

        [Authorize(Roles = "מנהל")]
        [HttpGet("GetAllUsersByRange")]
        public async Task<IActionResult> GetAllUsersInRange(int skip, int take)
        {
            _logger.Debug("GetAllUsersByRange");
            return Ok(await _usersService.GetAllUsers(skip, take));
        }

        [HttpGet("GetUserByUsername")]
        public async Task<IActionResult> GetUserByUsername(string username)
        {
            _logger.Debug("GetUsersByUsername");
            return Ok(await _usersService.GetUserByUsername(username));
        }

        [Authorize(Roles = "מנהל")]
        [HttpGet("GetAmountOfUsers")]
        public async Task<IActionResult> GetAmountOfUsers()
        {
            _logger.Debug("GetAmountOfUsers");
            return Ok(await _usersService.GetAmountOfUsers());
        }

        [Authorize(Roles = "מנהל")]
        [HttpPost("AddNewUser")]
        public async Task<IActionResult> AddNewUser(Buuser newUser)
        {
            _logger.Debug("AddNewUser");
            return Ok(await _usersService.AddNewUser(newUser));
        }

        [Authorize(Roles = "מנהל")]
        [HttpPut("UpdateUser")]
        public async Task<IActionResult> UpdateUser(Buuser updatedUser)
        {
            _logger.Debug("UpdateUser");
            return Ok(await _usersService.UpdateUser(updatedUser));
        }

        [Authorize(Roles = "מנהל")]
        [HttpPost("SearchUsers")]  // TODO: change to Get
        public async Task<IActionResult> SearchUsers(UserAdvancedSearch userAdvancedSearch, int skip, int take)
        {
            _logger.Debug("SearchUsers");
            return Ok(await _usersService.SearchUsers(userAdvancedSearch, skip, take));
        }

        [Authorize(Roles = "מנהל")]
        [HttpPut("ChangeUserPassword")]
        public async Task<IActionResult> ChangeUserPassword(Buuser User)
        {
            _logger.Debug("ChangeUserPassword");
            return Ok(await _usersService.ChangeUserPassword(User));
        }
    }
}