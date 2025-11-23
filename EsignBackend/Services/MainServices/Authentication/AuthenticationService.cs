using Azure;
using Azure.Core;
using DocumentFormat.OpenXml.Spreadsheet;
using EsignBackend.Common;
using EsignBackend.Dtos.Login;
using EsignBackend.Extensions.EncryptDecrypt;
using EsignBackend.Models;
using Microsoft.AspNetCore.Hosting;
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
        private readonly IWebHostEnvironment _env;
        private readonly AppSettings _appSettings;

        public AuthenticationService(AppDbContext context, ILogger logger, IOptions<AppSettings> appSettings, IWebHostEnvironment env)
        {
            _context = context;
            _logger = logger;
            _env = env;
            _appSettings = appSettings.Value;
        }

        private bool verifyPassword(string password, string userPassInDb)
        {
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
                    // don't use SameSiteMode.Strict or Lax, it won't work with Angular!
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
                    ////this one works
                    //context.Response.Cookies.Delete(cookieKey, new CookieOptions
                    //{
                    //    // Ensure you specify the same Path and Domain as when the cookie was created
                    //    // if they were explicitly set. Otherwise, the default will usually work.
                    //    Path = "/",
                    //    // Domain = "yourdomain.com", // Uncomment and set if necessary
                    //    IsEssential = true // Mark as essential if it's an authentication cookie
                    //});

                    context.Response.Cookies.Append(cookieKey, "",
                        new CookieOptions
                        {
                            Expires = DateTime.Now.AddDays(-1)
                            //MaxAge = TimeSpan.FromSeconds(0) 
                        });
                }
            }
        }

        private string createToken(Buuser user, ref UserClaimsDataDto userClaimsData)
        {
            _logger.Debug("Create token start");

            userClaimsData.UserName = user.Username;
            userClaimsData.UserRole = user.Usergroup.ToString();

            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier , user.Id.ToString()),
                new Claim(ClaimTypes.Name , user.Username),
                new Claim(ClaimTypes.Role , user.Usergroup.ToString())
            };

            //SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.Token));

            //var comp_var_name = "ESIGN_PRV_KEY";
            //var comp_var_val = Environment.GetEnvironmentVariable(comp_var_name, EnvironmentVariableTarget.Machine);//Environment.GetEnvironmentVariable(comp_var_name, EnvironmentVariableTarget.User);

            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Utils.GetEsignPrvKeyValue()));

            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddMinutes(_appSettings.SessionExpireMinuteTime),
                SigningCredentials = creds
            };
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

            string tokenString = tokenHandler.WriteToken(token);

            _logger.Debug("Create token end");

            return tokenString;
        }

        public async Task<ServiceResponse<UserClaimsDataDto>> Login(string username, string password)
        {
            _logger.Information($"Login username: {username}");
            _logger.Information($"Current Environment is: ({_env.EnvironmentName})");
            _logger.Information($"Runtime version: {System.Runtime.InteropServices.RuntimeInformation.FrameworkDescription}");

            var response = new ServiceResponse<UserClaimsDataDto>();

            try
            {
                var user = await _context.Buusers.FirstOrDefaultAsync(x => x.Username.Equals(username));

                if (user == null)
                {
                    _logger.Warning($"User [{username}] not found");
                    response.Success = false;
                    response.Message = "User name not found or password incorrect";
                }
                else if (!verifyPassword(password, user.Pass))
                {
                    _logger.Warning($"User [{username}] password is wrong");
                    response.Success = false;
                    response.Message = "User name not found or password incorrect";
                }
                else if (IsExpired(user.Expires))
                {
                    _logger.Warning($"User [{username}]: Expiration date is expired");
                    response.Success = false;
                    response.Message = "User expired";
                }
                else
                {
                    _logger.Information($"User [{username}] successfully logged-in");

                    //here we are creating token!
                    UserClaimsDataDto userData = new UserClaimsDataDto();
                    response.Data = userData;

                    response.Data.Token = createToken(user, ref userData);

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
                _logger.Error($"Login user [{username}] exception: " + ex.Message);

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