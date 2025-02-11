using DocumentFormat.OpenXml.Spreadsheet;
using EsignBackend.Extensions.EncryptDecrypt;
using EsignBackend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
using Newtonsoft.Json;
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
        private readonly ILogger _logger;
        private readonly AppSettings _appSettings;

        public AuthenticationService(AppDbContext context, ILogger logger, IOptions<AppSettings> appSettings)
        {
            _context = context;
            _logger = logger;
            _appSettings = appSettings.Value;
        }

        private bool VerifyPassword(string password, string userPassInDb)
        {
            _logger.Debug("VerifyPassword");
            var encryptedPass = EncryptDecryptHandler.encryptUserPass(password);

            return userPassInDb.Equals(encryptedPass);
        }

        private string CreateToken(Buuser user)
        {
            _logger.Debug("CreateToken");

            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier , user.Id.ToString()),
                new Claim(ClaimTypes.Name , user.Username),
                new Claim(ClaimTypes.Role , user.Usergroup.ToString())
            };

            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.Token));
            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddMinutes(_appSettings.SessionExpireMinuteTime),
                SigningCredentials = creds
            };
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        public async Task<ServiceResponse<string>> Login(string username, string password)
        {
            // password = "123456";
            _logger.Debug("Login");

            var response = new ServiceResponse<string>();

            try
            {
                var user = await _context.Buusers.FirstOrDefaultAsync(x => x.Username.Equals(username));

                if (user == null)
                {
                    _logger.Debug("user is null");
                    response.Success = false;
                    response.Message = "Username or Password incorrect";
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
                    response.Message = JsonConvert.SerializeObject(user);
                    response.Success = true;
                }
            }
            catch (Exception ex)
            {
                _logger.Debug("Select user exception: " + ex.Message);
                response.Success = false;
                response.Message = ex.Message;

                return response;
            }

            return response;
        }

        private bool IsExpired(DateTime? expires)
        {
            return !(expires != null && DateTime.Now < expires);
        }
    }

}