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
using System.Text.RegularExpressions;
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

        private bool isUserNameAlreadyInUse(string username)
        {
            return _context.Buusers.Where(user => user.Username.Equals(username)).Count() != 0;
        }

        private string encryptPass(String pass)
        {
            return EncryptDecryptHandler.encryptUserPass(pass);
        }

        public async Task<ServiceResponse<List<UserDTO>>> GetAllUsers(int skip, int take)
        {
            _logger.Debug($"Get all users: skip - {skip}, take - {take}");

            var serviceResponse = new ServiceResponse<List<UserDTO>>();

            List<Buuser> users = null;

            try
            {
                users = await _context.Buusers
                    .AsNoTracking()

                    .OrderByDescending(c => c.Id)

                    .Skip(skip).Take(take).ToListAsync();

                serviceResponse.Amount = _cache.GetCounterByType(CacheType.Users);
            }
            catch (Exception ex)
            {
                _logger.Error($"Get all users: Error processing: {ex}");

                serviceResponse.Data = null;
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;

                return serviceResponse;
            }

            var usersList = new List<UserDTO>();

            foreach (var user in users)
            {
                usersList.Add(new UserDTO(user));
            }

            serviceResponse.Success = true;
            serviceResponse.Data = usersList;

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<UserDTO>>> SearchUsers(UserAdvancedSearch userAdvancedSearch, int skip, int take)
        {
            _logger.Debug($"Search users: skip - {skip}, take - {take}");

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
                _logger.Error($"Search users - error while processing: {ex}");

                serviceResponse.Data = null;
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;

                return serviceResponse;
            }

            var userList = new List<UserDTO>();

            foreach (var user in users)
            {
                userList.Add(new UserDTO(user));
            }

            serviceResponse.Success = true;
            serviceResponse.Data = userList;
            serviceResponse.Success = true;

            return serviceResponse;

            //serviceResponse.Amount = dbUsers.Count();
            //serviceResponse.Data = take == UNLIMITED ? dbUsers.Skip(skip).ToList() : dbUsers.Skip(skip).Take(take).ToList();
            //return serviceResponse;
        }

        public UserDTO GetUserByUsername(string username)
        {
            _logger.Debug($"Get user by name: {username}");

            //var serviceResponse = new ServiceResponse<UserDTO>();

            var user = _context.Buusers
                .Where(x => x.Username == username)
                //First will throw an exception when there are no results. 
                .First();

                //await _context.Buusers.FirstOrDefaultAsync(u => u.Username.Equals(username));

            //if (user == null)
            //{
            //    serviceResponse.Amount = 0;
            //    _logger.Error($"Not found user by Username = {username}");
            //    return serviceResponse;
            //}

            Department department = _context.Departments
                .Where(x => x.Id == user.Departmentid)
                .FirstOrDefault();

            UserDTO userDto = null;

            if (department == null)
            {
                userDto = new UserDTO(user, "???");
            }
            else
            {
                userDto = new UserDTO(user, department.Title);
            }

            //await _context.Departments.FirstOrDefaultAsync(d => d.Id == user.Departmentid);


            //if (department == null)
            //{
            //    serviceResponse.Success = false;
            //    serviceResponse.Message = $" Not found department for user {user.Username}: departmentId = {user.Departmentid}";
            //    serviceResponse.Data = new UserDTO(user, "???");
            //    return serviceResponse;
            //}

            //serviceResponse.Amount = 1;
            //serviceResponse.Data = new UserDTO(user, department.Title);
            //return serviceResponse;

            return userDto;
        }

        public async Task<ServiceResponse<int>> GetAmountOfUsers()
        {
            _logger.Debug("Get amount of users");

            var serviceResponse = new ServiceResponse<int>();

            try
            {
                serviceResponse.Data = _cache.GetCounterByType(CacheType.Users);
            }
            catch (Exception ex)
            {
                _logger.Error($"Get amount of users error: {ex}");

                serviceResponse.Data = -1;
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;

                return serviceResponse;
            }
            return serviceResponse;
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
            _logger.Information($"Add new user: {newUser.Username}");

            var serviceResponse = new ServiceResponse<int>();

            string validateMessage = string.Empty;

            if (!validateUserPassword(newUser.Pass, out validateMessage))
            {
                _logger.Warning($"Add new user - cannot accept password: {newUser.Username}");

                serviceResponse.Success = false;
                serviceResponse.Message = validateMessage;
                serviceResponse.Data = -1;
                return serviceResponse;
            }

            var userNameIsAlreadyTaken = isUserNameAlreadyInUse(newUser.Username);

            if (userNameIsAlreadyTaken)
            {
                _logger.Warning($"Add new user - username is already taken: {newUser.Username}");

                serviceResponse.Success = false;
                serviceResponse.Message = "Username is already taken";
                serviceResponse.Data = -1;
                return serviceResponse;
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
                    serviceResponse.Data = newUserInDb.Entity.Id;
                    _cache.Increment(CacheType.Users);
                    serviceResponse.Success = true;
                    return serviceResponse;
                }
                catch (Exception ex)
                {
                    _logger.Error($"Add new user [{newUser.Username}] exception: {ex}");

                    serviceResponse.Success = false;
                    serviceResponse.Message = $"Adding new user failed. {ex}";
                    serviceResponse.Data = -1;
                    return serviceResponse;
                }
            }
        }

        public async Task<ServiceResponse<int>> UpdateUser(Buuser updatedUser)
        {
            _logger.Information($"Update user: {updatedUser.Username}");

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
                serviceResponse.Data = updatedUserInDb.Entity.Id;
                serviceResponse.Message = "User updated successfully.";
                serviceResponse.Success = true;
                return serviceResponse;
            }
            catch (Exception exception)
            {
                _logger.Error($"Exception detected while trying to update user [{updatedUser.Username}]: {exception}");

                serviceResponse.Success = false;
                serviceResponse.Message = $"Updated user failed. {exception}";
                serviceResponse.Data = -1;
                return serviceResponse;
            }
        }

        public async Task<ServiceResponse<int>> ChangeUserPassword(Buuser user)
        {
            _logger.Information($"Change user password for user: {user.Username}");

            var serviceResponse = new ServiceResponse<int>();

            string validateMessage = string.Empty;

            if (!validateUserPassword(user.Pass, out validateMessage))
            {
                _logger.Warning($"Change user password - cannot accept password: {user.Username}");

                serviceResponse.Success = false;
                serviceResponse.Message = validateMessage;
                serviceResponse.Data = -1;
                return serviceResponse;
            }

            var encryptedPass = encryptPass(user.Pass);

            user.Pass = encryptedPass;

            var updatedUserInDb = _context.Buusers.Update(user);

            try
            {
                _context.SaveChangesAsync();
                serviceResponse.Data = updatedUserInDb.Entity.Id;
                serviceResponse.Message = "Password updated successfully.";
                serviceResponse.Success = true;
                return serviceResponse;
            }
            catch (Exception exception)
            {
                _logger.Error($"Exception detected while trying to change user password: {exception}");

                serviceResponse.Success = false;
                serviceResponse.Message = $"Change password failed. {exception}";
                serviceResponse.Data = -1;
                return serviceResponse;
            }
        }

        private bool validateUserPassword(string plainPass, out string messageStr)
        {
            messageStr = string.Empty;

            if (string.IsNullOrWhiteSpace(plainPass))
            {
                messageStr = "הסיסמה לא הוזנה";
                return false;
            }

            if (plainPass.Length < 8)
            {
                messageStr = "אורך הסיסמה המינימלי חייב להיות לפחות 8 תווים";
                return false;
            }

            bool isValid =
                plainPass.Any(ch => char.IsLower(ch)) &&
                plainPass.Any(ch => char.IsDigit(ch)) &&
                !plainPass.Any(ch => char.IsUpper(ch));

            if (!isValid)
            {
                messageStr = "סיסמה יכול להכיל רק אותיות קטנות באנגלית ומספרים";
                return false;
            }

            return true;
        }

    }
}
