using EsignBackend.Models;
using EsignBackend.Models.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EsignBackend.Services.CharacterService
{
    public interface IUsersService
    {
        Task<ServiceResponse<List<Buuser>>> GetAllUsers(int skip,int take);
        //Task<ServiceResponse<List<Buuser>>> GetUser(string searchValue, string criterion);
        Task<ServiceResponse<int>> GetAmountOfUsers();
        Task<ServiceResponse<int>> AddNewUser(Buuser newUser);
        Task<ServiceResponse<int>> UpdateUser(Buuser updatedUser);
        //Task<ServiceResponse<int>> ExportUsers(List<Buuser> usersToExport);
        Task<ServiceResponse<List<Buuser>>> SearchUsers(UserAdvancedSearch userAdvancedSearch, int skip, int take);
        Task<ServiceResponse<int>> ChangeUserPassword(Buuser user);
    }
}
