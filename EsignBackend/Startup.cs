
using EsignBackend.Services.CharacterService;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EsignBackend.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using EsignBackend.Services.MainServices.Certificates;
using EsignBackend.Extensions;
using FluentValidation.AspNetCore;
using Serilog;
using EsignBackend.Middlewares;
using Microsoft.OpenApi.Models;
using System.IO;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Hangfire;
using Hangfire.SqlServer;
using Hangfire.MemoryStorage;
using Microsoft.AspNetCore.Mvc.Formatters;
using System.Text.Json;
using EsignBackend.Extensions.CacheHandlers;
using System.Threading;
using Microsoft.Extensions.Options;
using Serilog.Context;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using DocumentFormat.OpenXml.InkML;
using Serilog.Events;

namespace EsignBackend
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            _config = configuration;
            _env = env;
        }

        public IConfiguration _config { get; }
        public IWebHostEnvironment _env { get; }
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHangfire(config =>
            {
                config.SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseDefaultTypeSerializer()
                .UseMemoryStorage();
            });

            // Add the processing server as IHostedService
            services.AddHangfireServer();

            //services.AddDbContext<DataContext>(x => x.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddMvc().AddFluentValidation().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            services.AddDbContext<AppDbContext>(config =>
                config.UseSqlServer(_config.GetConnectionString("DefaultConnection"),
                    providerOptions =>
                    {
                        providerOptions.CommandTimeout(Convert.ToInt32(_config.GetSection("AppSettings:SqlServerWaitTimeToExecuteCommand").Value));
                        providerOptions.EnableRetryOnFailure(3);
                    }
                )
            );

            services.AddControllersWithViews(options =>
                {
                    //options.AllowEmptyInputInBodyModelBinding = true;
                })
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                }
                );

        services.AddControllers();

        services.AddAutoMapper(typeof(Startup));

        services.AddRazorPages();

            //services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            //    .AddJwtBearer(options =>
            //    {
            //        options.TokenValidationParameters = new TokenValidationParameters
            //        {
            //            ValidateIssuerSigningKey = true,
            //            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_config.GetSection("AppSettings:Token").Value)),
            //            ValidateIssuer = false,
            //            ValidateAudience = false,
            //            ValidateLifetime = true,
            //            ClockSkew = TimeSpan.Zero
            //        };
            //    });

            //services.ConfigureApplicationCookie(options =>
            //{
            //    options.Cookie.Name = "tokenInCookie";
            //    options.Cookie.HttpOnly = true;
            //    options.Cookie.SameSite = SameSiteMode.Strict;
            //    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
            //    //options.Cookie.Domain = "localhost";
            //    //options.SlidingExpiration = true;
            //    options.ExpireTimeSpan = TimeSpan.FromMinutes(Convert.ToDouble(_config.GetSection("AppSettings").GetSection("SessionExpireMinuteTime").Value));
            //    options.Cookie.IsEssential = true;

            //});

            //services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            //services.AddAuthentication(options =>
            //{
            //    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            //    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //})
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_config.GetSection("AppSettings:Token").Value)),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };

                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = ctx =>
                    {
                        ctx.Request.Cookies.TryGetValue("accessToken", out var accessToken);

                        if (!string.IsNullOrEmpty(accessToken))
                        {
                            ctx.Token = accessToken;

                            //ctx.Request.Headers.Add("Authorization", $"Bearer: {accessToken}");
                        }
                        return Task.CompletedTask;
                    },
                };
            });

            services.AddHttpContextAccessor();

            services.AddHandlers(_env);
            // services.AddScoped<ICharacterService, CharacterService>();

            var configuration = new ConfigurationBuilder()
                              .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)                           
                              .Build();

            services.AddSingleton<ILogger>(
                new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                //.WriteTo.EventLog(
                //      logName: "Application",
                //      source: "Esign",
                //      manageEventSource: true,
                //      restrictedToMinimumLevel: LogEventLevel.Information)
                .Enrich.FromLogContext()
                .CreateLogger());

            services.AddRateLimiting(_config);

            var address = configuration.GetSection("AppSettings").GetSection("FrontURL").Value;
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                builder => builder.WithOrigins(address)
                //.AllowAnyOrigin() - bug 03/04/2025
                .AllowAnyMethod()
                .AllowAnyHeader()
                //.WithExposedHeaders("Access-Control-Expose-Headers")
                //.WithExposedHeaders("X-Access-Token")
                //.WithExposedHeaders("Set-Cookie")
                .AllowCredentials()
                );
            });

            //services.AddHsts(options =>
            //{
            //    options.Preload = true;
            //    options.IncludeSubDomains = true;
            //    options.MaxAge = TimeSpan.FromDays(365);
            //});

            services.AddValidation();
            services.AddConfiguration(_config);
            //How to apply in Swagger authorize text box:
            //Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiIyMTMiLCJ1bmlxdWVfbmFtZSI6ImdhbGgiLCJyb2xlIjoi157XoNeU15wiLCJuYmYiOjE3NDM0MTQ5OTgsImV4cCI6MTc0MzQyMjE5OCwiaWF0IjoxNzQzNDE0OTk4fQ.RSBBL5bIU6NfOvILnbbw__5H6YjtelR756xtFSbFC4E
            services.AddSwagger();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, ICertificatesService certificatesService, IRecurringJobManager recurringJobManager, IBackgroundJobClient backgroundJobs, IWebHostEnvironment env)
        {
            app.UseRateLimiting();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseCors("CorsPolicy");
        
            app.UseRouting();
            app.UseHttpsRedirection();
            app.UseAuthentication();

            app.UseErrorHandlingMiddleware();

            app.UseMiddleware<AddTokenFromCookieMiddleware>();

            app.UseAuthorization();

            app.UseMiddleware<LogUserNameMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHangfireDashboard();
            });

            app.UseSwagger(options =>
            {
                options.SerializeAsV2 = true;
            });

            if(env.IsDevelopment())
            {
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "ESign API V1");
                    options.RoutePrefix = "swagger";
                });
            }
            else
            {
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/api/swagger/v1/swagger.json", "ESign API V1");
                    options.RoutePrefix = "swagger";
                });
            }

            app.UseHangfireDashboard();
            backgroundJobs.Enqueue(() => certificatesService.UpdateExpiredCertificates());
            recurringJobManager.AddOrUpdate("#Hangfire update expire certificates", () => certificatesService.UpdateExpiredCertificates(),
            Cron.HourInterval(8));
        }
    }
}