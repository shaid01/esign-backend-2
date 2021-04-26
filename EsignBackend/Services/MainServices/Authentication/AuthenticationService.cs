using EsignBackend.Extensions.EncryptDecrypt;
using EsignBackend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EsignBackend.Services.CharacterService
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;

        public AuthenticationService(AppDbContext context, IConfiguration configuration, ILogger logger)
        {
            _context = context;
            _configuration = configuration;
            _logger = logger;
        }
        //Todo not mockup - real checking.
        private bool VerifyPassword(string password, string userPassInDb)
        {
            _logger.Debug("VerifyPassword");
            var encryptedPass = EncryptDecryptHandler.encryptUserPass(password);
            return userPassInDb.Equals(encryptedPass);
           // return password.Equals("5F585E5F5A46");
        }
        private string CreateToken(Buuser user)
        {
            _logger.Debug("CreateToken");

            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier , user.Id.ToString()),
                new Claim(ClaimTypes.Name , user.Firstname.ToString()),
                new Claim(ClaimTypes.Role , user.Usergroup.ToString())
            };

            SymmetricSecurityKey key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value)
                );
            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                // Expires = DateTime.Now.AddHours(1),
                // Expires = DateTime.Now.AddSeconds(20),
                Expires = DateTime.Now.AddMinutes(30),
                //Expires = DateTime.Now.AddMinutes(2),
                SigningCredentials = creds
            };
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        public async Task<ServiceResponse<string>> Login(string username, string password)
        {
            // JUST FOR NOW - MOCK UP!
            password = "123456";
            _logger.Debug("Login");


            var response = new ServiceResponse<string>();
            var user = await _context.Buusers.FirstOrDefaultAsync(x => x.Username.Equals(username));
            if (user == null)
            {
                _logger.Debug("user is null");
                response.Success = false;
                response.Message = "User Not Found";
            }
            else if (!VerifyPassword(password, user.Pass))
            {
                _logger.Debug("verifyPassword failed");
                response.Success = false;
                response.Message = "Username or Password incorrect";
            }
            else if (IsExpired(user.Expires))
            {
                _logger.Debug("Expiration date expired");
                response.Success = false;
                response.Message = "Expiration date expired";
            }
            else
            {
                _logger.Debug("user successfully login");
                response.Data = CreateToken(user);
                response.Message = user.Id.ToString();
                response.Success = true;
            }
            return response;
        }

        private bool IsExpired(DateTime? expires)
        {
            return !(expires != null && DateTime.Now < expires);
        }
    }
}