using AutoMapper;
using EsignBackend.Data;
//using EsignBackend.Dtos.Character;
using EsignBackend.Dtos.Issplace;
using EsignBackend.Dtos.User;
using EsignBackend.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EsignBackend.Services.CharacterService
{
    public class CharacterService : ICharacterService
    {
   
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public CharacterService(IMapper mapper,DataContext context)
        {
            _mapper = mapper;
            _context = context;
        }


     /*   public async Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto newCharacter)
        {
            ServiceResponse<List<GetCharacterDto>> serviceRespone = new ServiceResponse<List<GetCharacterDto>>();
           Character character = _mapper.Map<Character>(newCharacter);
            await _context.characters.AddAsync(character);
            await _context.SaveChangesAsync();
            serviceRespone.Data = (_context.characters.Select(c => _mapper.Map<GetCharacterDto>(c))).ToList();
            return serviceRespone;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacters()
        {
            ServiceResponse<List<GetCharacterDto>> serviceRespone = new ServiceResponse<List<GetCharacterDto>>();
            List<Character> dbCharacters = await _context.characters.ToListAsync();

            serviceRespone.Data = (dbCharacters.Select(c => _mapper.Map<GetCharacterDto>(c))).ToList();
            return serviceRespone;
        }

        public async Task<ServiceResponse<List<GetIssplaceDto>>> GetAllIssplaces()
        {
            ServiceResponse<List<GetIssplaceDto>> serviceRespone = new ServiceResponse<List<GetIssplaceDto>>();
            List<Issplace> dbIssplaces = await _context.issplace.ToListAsync();
            Console.Out.Write($"{dbIssplaces}");
            Console.Out.Write($"{dbIssplaces}");

            serviceRespone.Data = (dbIssplaces.Select(c => _mapper.Map<GetIssplaceDto>(c))).ToList();
            return serviceRespone;
        }

        public async Task<ServiceResponse<List<GetUserDto>>> GetAllUsers()
        {
            ServiceResponse<List<GetUserDto>> serviceRespone = new ServiceResponse<List<GetUserDto>>();

            

            List<User> dbUsers = await _context.buusers.ToListAsync();
            Console.Out.Write($"dbUsers: {dbUsers}");
            Console.Out.Write($"{dbUsers}");

            serviceRespone.Data = (dbUsers.Select(c => _mapper.Map<GetUserDto>(c))).ToList();
            return serviceRespone;
        }



        public async Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id)
        {
            ServiceResponse<GetCharacterDto> serviceRespone = new ServiceResponse<GetCharacterDto>();
            Character dbCharacter = await _context.characters.FirstOrDefaultAsync(c => c.Id == id);
            serviceRespone.Data = _mapper.Map<GetCharacterDto>(dbCharacter);
            return serviceRespone;
        }

        public async Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(UpdateCharacterDto updatedCharacter)
        {
            ServiceResponse<GetCharacterDto> serviceResponse = new ServiceResponse<GetCharacterDto>();
            // Its not working. suppose to catch and not to crash.
            try
            {
                Character character = await _context.characters.FirstOrDefaultAsync(c => c.Id == updatedCharacter.Id);
                character.Name = updatedCharacter.Name;
                _context.characters.Update(character);
                await _context.SaveChangesAsync();

                serviceResponse.Data = _mapper.Map<GetCharacterDto>(character);
            }
            catch(Exception e)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = e.Message;
            }            
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> DeleteCharacter(int id)
        {
            ServiceResponse<List<GetCharacterDto>> serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            // Its not working. suppose to catch and not to crash.
            try
            {
                Character character = await _context.characters.FirstAsync(c => c.Id == id);
                _context.characters.Remove(character);
                await _context.SaveChangesAsync();

                serviceResponse.Data = (_context.characters.Select(c => _mapper.Map<GetCharacterDto>(c))).ToList();
            }
            catch (Exception e)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = e.Message;
            }
            return serviceResponse;
        }*/
    }
}
