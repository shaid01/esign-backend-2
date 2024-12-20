using DocumentFormat.OpenXml.Spreadsheet;
using EsignBackend.Extensions.CacheHandlers;
using EsignBackend.Extensions.EncryptDecrypt;
using EsignBackend.Models;
using EsignBackend.Models.DTOs;
using EsignBackend.Models.Tools;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EsignBackend.Services.CharacterService
{
    public class UserService : IUsersService
    {

        private readonly AppDbContext _context;
        private readonly ILogger _logger;
        private readonly ICache _cache;
        private static object _locker = new object();
        private const int UNLIMITED = -1;

        public UserService(AppDbContext context, ILogger logger, ICache cache)
        {
            _context = context;
            _logger = logger;
            _cache = cache;
        }

        private int GenerateUserId()
        {
            _logger.Debug("GenerateUserId");
            lock (_locker)
            {
                int maxId = _context.Buusers.OrderByDescending(user => user.Id).Take(1).ToList()[0].Id;
                return maxId + 1;
            }
        }

        private bool IsUserNameAlreadyInUse(string username)
        {
            _logger.Debug("IsUserNameAlreadyInUse");
            return _context.Buusers.Where(user => user.Username.Equals(username)).Count() != 0;
        }

        private string encryptPass(String pass)
        {
            _logger.Debug("encryptPass");
            return EncryptDecryptHandler.encryptUserPass(pass);
        }

        public async Task<ServiceResponse<List<UserDTO>>> GetAllUsers(int skip, int take)
        {
            _logger.Debug("GetAllUsers");

            var serviceRespone = new ServiceResponse<List<UserDTO>>();

            List<Buuser> users = null;

            try
            {
                users = await _context.Buusers
                    .AsNoTracking()

                    .OrderByDescending(c => c.Id)

                    .Skip(skip).Take(take).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.Error($"Error while processing DB query in GetAllUsers. {ex.Message}");
                var errorData = new ServiceResponse<List<UserDTO>>();
                errorData.Data = null;
                errorData.Success = false;
                errorData.Message = ex.Message;

                return errorData;
            }

            var usersList = new List<UserDTO>();

            foreach (var user in users)
            {
                usersList.Add(new UserDTO(user));
            }

            serviceRespone.Success = true;
            serviceRespone.Data = usersList;
            serviceRespone.Amount = _cache.GetCounterByType(CacheType.Users);

            return serviceRespone;
        }

        public async Task<ServiceResponse<List<UserDTO>>> SearchUsers(UserAdvancedSearch userAdvancedSearch, int skip, int take)
        {
            _logger.Debug("SearchUsers");

            var serviceResponse = new ServiceResponse<List<UserDTO>>();

            var query = _context.Buusers.AsNoTracking().Where(user =>
                ((userAdvancedSearch.UserName == null) || EF.Functions.Like(user.Username, $"%{userAdvancedSearch.UserName}%"))
                && ((userAdvancedSearch.FirstName == null) || EF.Functions.Like(user.Firstname, $"%{userAdvancedSearch.FirstName}%"))
                && ((userAdvancedSearch.LastName == null) || EF.Functions.Like(user.Lastname, $"%{userAdvancedSearch.LastName}%"))
                && ((userAdvancedSearch.Email == null) || EF.Functions.Like(user.Email, $"%{userAdvancedSearch.Email}%"))
                && ((userAdvancedSearch.UserGroup == null) || EF.Functions.Like(user.Usergroup, userAdvancedSearch.UserGroup))
                );

            query = query.OrderByDescending(user => user.Id);

            List<Buuser> users = null;

            try
            {
                serviceResponse.Amount = await query.CountAsync();

                query = query.Skip(skip);

                if (take != UNLIMITED)
                {
                    query = query.Take(take);
                }

                users = await query.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.Error($"Error while processing DB query in SearchUsers. {ex.Message}");
                var errorData = new ServiceResponse<List<UserDTO>>();
                errorData.Data = null;
                errorData.Success = false;
                errorData.Message = ex.Message;

                return errorData;
            }

            var userList = new List<UserDTO>();

            foreach (var user in users)
            {
                try
                {
                    userList.Add(new UserDTO(user));
                }
                catch (Exception ex)
                {
                    _logger.Error($"Error in add user to userDtoList, user id - {user.Id}");
                }
            }

            serviceResponse.Data = userList;
            serviceResponse.Success = true;

            return serviceResponse;

            //serviceRespone.Amount = dbUsers.Count();
            //serviceRespone.Data = take == UNLIMITED ? dbUsers.Skip(skip).ToList() : dbUsers.Skip(skip).Take(take).ToList();
            //return serviceRespone;
        }

        public async Task<ServiceResponse<UserDTO>> GetUserByUsername(string username)
        {
            _logger.Debug("GetUserByUsername");
            var serviceRespone = new ServiceResponse<UserDTO>();
            var user = await _context.Buusers.FirstOrDefaultAsync(u => u.Username.Equals(username));
            if (user == null)
            {
                serviceRespone.Amount = 0;
                _logger.Error($"Not found user by Username = {username}");
                return serviceRespone;
            }
            Department department = await _context.Departments.FirstOrDefaultAsync(d => d.Id == user.Departmentid);
            if (department == null)
            {
                serviceRespone.Success = false;
                serviceRespone.Message = $" Not found department for user {user.Username}: departmentId = {user.Departmentid}";
                serviceRespone.Data =new UserDTO(user, "???");
                return serviceRespone;
            }

            serviceRespone.Amount = 1;
            serviceRespone.Data = new UserDTO(user, department.Title);
            return serviceRespone;
        }

        public async Task<ServiceResponse<int>> GetAmountOfUsers()
        {
            _logger.Debug("GetAmountOfUsers");
            var serviceRespone = new ServiceResponse<int>();
            serviceRespone.Data = _cache.GetCounterByType(CacheType.Users);
            return serviceRespone;
        }

        /// <summary>
        /// Creating new user to Buusers table. 
        /// Success - returns the id of the new user.
        /// Fails - returns -1.
        /// </summary>
        /// <param name="newUser"></param>
        /// <returns></returns>
        public async Task<ServiceResponse<int>> AddNewUser(Buuser newUser)
        {
            _logger.Debug("AddNewUser");
            var serviceRespone = new ServiceResponse<int>();
            var userNameIsAlreadyTaken = IsUserNameAlreadyInUse(newUser.Username);
            if (userNameIsAlreadyTaken)
            {
                serviceRespone.Success = false;
                serviceRespone.Message = "Username is already taken";
                serviceRespone.Data = -1;
                return serviceRespone;
            }
            lock (_locker)
            {
                //var newId = GenerateUserId();
                //newUser.Id = newId;

                newUser.Pass = encryptPass(newUser.Pass);
                newUser.Expires = newUser.Expires.Value.ToLocalTime();
                var newUserInDb = _context.Buusers.Add(newUser);
                try
                {
                    _context.SaveChanges();
                    serviceRespone.Data = newUserInDb.Entity.Id;
                    _cache.Increment(CacheType.Users);
                    return serviceRespone;
                }
                catch (Exception ex)
                {
                    _logger.Debug("AddNewUser exception: " + ex);
                    serviceRespone.Success = false;
                    serviceRespone.Message = $"Registration failed. {ex}";
                    serviceRespone.Data = -1;
                    return serviceRespone;
                }
            }
        }

        public async Task<ServiceResponse<int>> UpdateUser(Buuser updatedUser)
        {
            _logger.Debug("UpdateUser");
            updatedUser.Updateddate = updatedUser.Updateddate.Value.ToLocalTime();
            var serviceResponse = new ServiceResponse<int>();
            var dbUser = _context.Buusers.AsNoTracking().FirstOrDefault(x => x.Id == updatedUser.Id);
            if (dbUser != null)
            {
                updatedUser.Pass = dbUser.Pass;
            }
            var updatedUserInDb = _context.Buusers.Update(updatedUser);
            try
            {
                _context.SaveChanges();
                serviceResponse.Success = true;
                serviceResponse.Data = updatedUserInDb.Entity.Id;
                serviceResponse.Message = "User updated successfully.";
                return serviceResponse;
            }
            catch (Exception exception)
            {
                _logger.Debug("exception detected while trying to UpdateUser: " + exception);
                serviceResponse.Success = false;
                serviceResponse.Message = $"Updated user failed. {exception}";
                serviceResponse.Data = -1;
                return serviceResponse;
            }
        }

        public async Task<ServiceResponse<int>> ChangeUserPassword(Buuser user)
        {
            _logger.Debug("ChangeUserPassword");

            var encryptedPass = encryptPass(user.Pass);
            user.Pass = encryptedPass;
            var serviceResponse = new ServiceResponse<int>();
            var updatedUserInDb = _context.Buusers.Update(user);
            try
            {
                _context.SaveChangesAsync();
                serviceResponse.Success = true;
                serviceResponse.Data = updatedUserInDb.Entity.Id;
                serviceResponse.Message = "Password updated successfully.";
                return serviceResponse;
            }
            catch (Exception exception)
            {
                _logger.Error("exception detected while trying to ChangeUserPassword: " + exception);
                serviceResponse.Success = false;
                serviceResponse.Message = $"Change password failed. {exception}";
                serviceResponse.Data = -1;
                return serviceResponse;
            }
        }

    }
}
