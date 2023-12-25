using EsignBackend.Models;
using EsignBackend.Models.DTOs;
using EsignBackend.Models.Tools;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EsignBackend.Services.CharacterService
{
    public interface IUsersService
    {
        Task<ServiceResponse<List<UserDTO>>> GetAllUsers(int skip,int take);
        Task<ServiceResponse<int>> GetAmountOfUsers();
        Task<ServiceResponse<int>> AddNewUser(Buuser newUser);
        Task<ServiceResponse<int>> UpdateUser(Buuser updatedUser);
        Task<ServiceResponse<List<UserDTO>>> SearchUsers(UserAdvancedSearch userAdvancedSearch, int skip, int take);
        Task<ServiceResponse<int>> ChangeUserPassword(Buuser user);
    }
}
