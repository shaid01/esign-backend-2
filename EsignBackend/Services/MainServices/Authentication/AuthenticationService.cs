using Azure;
using Azure.Core;
using DocumentFormat.OpenXml.Spreadsheet;
using EsignBackend.Dtos.Login;
using EsignBackend.Extensions.EncryptDecrypt;
using EsignBackend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
using Newtonsoft.Json;
using Serilog;
using Serilog.Context;
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

        //https://code-maze.com/how-to-use-httponly-cookie-in-net-core-for-authentication-and-refresh-token-actions/
        public void SetTokensInsideCookie(string tokenDto, HttpContext context)
        {
            //context.Response.Cookies.Delete("accessToken");
            //context.Response.Headers.Add("Access-Control-Expose-Headers",
            context.Response.Cookies.Append("accessToken", tokenDto,
                new CookieOptions
                {
                    Expires = DateTime.Now.AddMinutes(_appSettings.SessionExpireMinuteTime),//DateTimeOffset.UtcNow.AddMinutes(5),
                    HttpOnly = true,
                    IsEssential = true,
                    Secure = true,
                    // don't use SameSiteMode.Strict, it won't work with Angular!
                    SameSite = SameSiteMode.None,
                    MaxAge = null
                });

            //var x = context.Response.Headers["Set-Cookie"][0];

            //context.Response.Headers.Add("Access-Control-Expose-Headers", x);

            //context.Response.Cookies.Append("refreshToken", tokenDto.RefreshToken,
            //    new CookieOptions
            //    {
            //        Expires = DateTimeOffset.UtcNow.AddDays(7),
            //        HttpOnly = true,
            //        IsEssential = true,
            //        Secure = true,
            //        SameSite = SameSiteMode.None
            //    });
        }

        public void DeleteCookie(string cookieName, HttpContext context)
        {
            foreach (var cookieKey in context.Request.Cookies.Keys)
            {
                if (cookieKey.Equals(cookieName))
                {
                    //context.Response.Cookies.Delete(cookieKey);
                    context.Response.Cookies.Append(cookieKey, "",
                        new CookieOptions
                        {
                            Expires = DateTime.Now.AddDays(-1)
                        });
                }
            }
        }

        private string CreateToken(Buuser user, ref UserClaimsDataDto userClaimsData)
        {
            _logger.Debug("CreateToken");

            userClaimsData.UserName = user.Username;
            userClaimsData.UserRole = user.Usergroup.ToString();

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

        public async Task<ServiceResponse<UserClaimsDataDto>> Login(string username, string password)
        {
            // password = "123456";
            _logger.Debug($"Login username: {username}");

            var response = new ServiceResponse<UserClaimsDataDto>();

            try
            {
                var user = await _context.Buusers.FirstOrDefaultAsync(x => x.Username.Equals(username));

                if (user == null)
                {
                    _logger.Debug("User not found");
                    response.Success = false;
                    response.Message = "User not found";
                }
                else if (!VerifyPassword(password, user.Pass))
                {
                    _logger.Debug("verifyPassword failed");
                    response.Success = false;
                    response.Message = "Password incorrect";
                }
                else if (IsExpired(user.Expires))
                {
                    _logger.Debug("Expiration date expired");
                    response.Success = false;
                    response.Message = "User expired";
                }
                else
                {
                    _logger.Debug("User successfully login");

                    //here we are creating token!
                    UserClaimsDataDto userData = new UserClaimsDataDto();
                    response.Data = userData;

                    response.Data.Token = CreateToken(user, ref userData);

                    Department department = _context.Departments
                        .Where(x => x.Id == user.Departmentid)
                        .FirstOrDefault();

                    if (department != null)
                    {
                        response.Data.UserDept = department.Title;
                    }
                    else
                    {
                        response.Data.UserDept = "Unknown";
                    }

                    response.Data.UserName = userData.UserName;
                    response.Data.UserRole = userData.UserRole;
                    response.Data.UserFirstName = user.Firstname;

                    response.Message = JsonConvert.SerializeObject(user);
                    response.Success = true;
                }
            }
            catch (Exception ex)
            {
                _logger.Debug("Login user exception: " + ex.Message);
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