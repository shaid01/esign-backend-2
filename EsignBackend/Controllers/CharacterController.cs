//using EsignBackend.Dtos.Character;
using EsignBackend.Models;
using EsignBackend.Services.CharacterService;
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
    public class CharacterController:ControllerBase
    {
        private readonly ICharacterService _characterService;
        private readonly ILogger _logger;

        public CharacterController(ICharacterService characterService, ILogger logger)
        {
           _characterService = characterService;
            _logger = logger;
        }



/*
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSingle(int id)
        {
            return Ok(await _characterService.GetCharacterById(id));
        }

        [HttpPost]
        public async Task<IActionResult> AddCharacter(AddCharacterDto newCharacter)
        {
            return Ok(await _characterService.AddCharacter(newCharacter));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCharacter(UpdateCharacterDto updatedCharacter)
        {
            ServiceResponse<GetCharacterDto> respone = await _characterService.UpdateCharacter(updatedCharacter);
            if (respone.Data == null)
            {
                return NotFound(respone);
            }
            return Ok(respone);
        }

        [HttpGet("GetAll")]
         public async Task<IActionResult> Get()
        {
         return Ok(await _characterService.GetAllCharacters());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            ServiceResponse<List<GetCharacterDto>> respone = await _characterService.DeleteCharacter(id);
            if (respone.Data == null)
            {
                return NotFound(respone);
            }
            return Ok(respone);
        }

        [HttpGet("GetIss")]
        public async Task<IActionResult> GetAllIssplaces()
        {
            return Ok(await _characterService.GetAllIssplaces());
        }


        [HttpGet("GetUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            return Ok(await _characterService.GetAllUsers());
        }*/
    }
}
