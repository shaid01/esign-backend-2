using EsignBackend.Extensions.EncryptDecrypt;
using EsignBackend.Models;
using EsignBackend.Models.Tools;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
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

        private static object _locker = new object();

        public UserService(AppDbContext context, ILogger logger)
        {
            _context = context;
            _logger = logger;
        }


        private int GenerateUserId()
        {
            _logger.Debug("GenerateUserId");
            int maxId = _context.Buusers.OrderByDescending(user => user.Id).Take(1).ToList()[0].Id;
            return maxId + 1;
        }

        private bool IsUserNameAlreadyInUse(string username)
        {
            _logger.Debug("IsUserNameAlreadyInUse");
            return _context.Buusers.Where(user => user.Username.Equals(username)).Count() != 0;
        }

        private string encryptPass(String pass)
        {
          /*  _logger.Debug("encryptPass");
            var encryptedPass = ESignEncrypt.Encrypt(pass, "kikiitb");
            return encryptedPass;*/

            return EncryptDecryptHandler.encryptUserPass(pass);
        }

        public async Task<ServiceResponse<List<Buuser>>> GetAllUsers(int skip, int take)
        {
            _logger.Debug("GetAllUsers");
            var serviceRespone = new ServiceResponse<List<Buuser>>();
            serviceRespone.Data = await _context.Buusers.Skip(skip).Take(take).ToListAsync();
            return serviceRespone;
        }

        public async Task<ServiceResponse<int>> GetAmountOfUsers()
        {
            _logger.Debug("GetAmountOfUsers");
            var serviceRespone = new ServiceResponse<int>();
            serviceRespone.Data = _context.Buusers.Count();
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
                var newId = GenerateUserId();
                newUser.Id = newId;

                newUser.Pass = encryptPass(newUser.Pass);
                newUser.Expires = newUser.Expires.Value.ToLocalTime();
                var newUserInDb =  _context.Buusers.Add(newUser);
                try
                {
                    _context.SaveChanges();
                    serviceRespone.Data = newUserInDb.Entity.Id;
                    return serviceRespone;
                }
                catch (Exception exception)
                {
                    _logger.Debug("exception detected while trying to AddNewUser: " + exception);
                    serviceRespone.Success = false;
                    serviceRespone.Message = $"Registration failed. {exception}";
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
                serviceResponse.Message = $"Updated failed. {exception}";
                serviceResponse.Data = -1;
                return serviceResponse;
            }
        }

        public async Task<ServiceResponse<List<Buuser>>> SearchUsers(UserAdvancedSearch userAdvancedSearch, int skip, int take)
        {
            _logger.Debug("SearchUsers");

            var serviceRespone = new ServiceResponse<List<Buuser>>();
            var dbUsers = await _context.Buusers.Where(user =>
            ((userAdvancedSearch.UserName == null) || EF.Functions.Like(user.Username, $"%{userAdvancedSearch.UserName}%"))
            && ((userAdvancedSearch.FirstName == null) || EF.Functions.Like(user.Firstname, $"%{userAdvancedSearch.FirstName}%"))
            && ((userAdvancedSearch.LastName == null) || EF.Functions.Like(user.Lastname, $"%{userAdvancedSearch.LastName}%"))
            && ((userAdvancedSearch.Email == null) || EF.Functions.Like(user.Email, $"%{userAdvancedSearch.Email}%"))
            && ((userAdvancedSearch.UserGroup == null) || EF.Functions.Like(user.Usergroup, userAdvancedSearch.UserGroup))
            ).ToListAsync();
            serviceRespone.Message = dbUsers.Count().ToString();
            //serviceRespone.Data = dbUsers.Skip(skip).Take(take).ToList();
            serviceRespone.Data = dbUsers.Skip(skip).Take(take).ToList();
            return serviceRespone;
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
                serviceResponse.Message = "User updated successfully.";
                return serviceResponse;
            }
            catch (Exception exception)
            {
                _logger.Error("exception detected while trying to ChangeUserPassword: " + exception);
                serviceResponse.Success = false;
                serviceResponse.Message = $"Updated failed. {exception}";
                serviceResponse.Data = -1;
                return serviceResponse;
            }
        }

     

        /*public async Task<ServiceResponse<List<Buuser>>> GetUser(string searchValue, string criterion)
        {
            ServiceResponse<List<Buuser>> serviceRespone = new ServiceResponse<List<Buuser>>();
            switch (criterion)
            {
                case "username":
                    List<Buuser> dbUsers = await _context.Buusers.Where(user => user.Username.Equals(searchValue)).ToListAsync();
                    serviceRespone.Data = dbUsers.ToList();
                    break;
                case "firstname":
                    dbUsers = await _context.Buusers.Where(user => user.Firstname.Equals(searchValue)).ToListAsync();
                    serviceRespone.Data = dbUsers.ToList();
                    break;
                case "lastname":
                    dbUsers = await _context.Buusers.Where(user => user.Lastname.Equals(searchValue)).ToListAsync();
                    serviceRespone.Data = dbUsers.ToList();
                    break;
                case "email":
                    dbUsers = await _context.Buusers.Where(user => user.Email.Equals(searchValue)).ToListAsync();
                    serviceRespone.Data = dbUsers.ToList();
                    break;
                default:
                    dbUsers = await _context.Buusers.Where(user => user.Username.Equals(searchValue)).ToListAsync();
                    serviceRespone.Data = dbUsers.ToList();
                    break;
            }
            return serviceRespone;
        }*/
        /* public async Task<ServiceResponse<int>> ExportUsers(List<Buuser> usersToExport)
         {
            return new ServiceResponse<int>();
         }
        */
    }
}
