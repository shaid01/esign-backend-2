
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
using EsignBackend.Services.SettingsService;
using EsignBackend.Services.SettingsService.CertificatesStatus;
using EsignBackend.Services.SettingsService.SmartObject;
using EsignBackend.Services.SettingsService.Expirationtype;
using EsignBackend.Services.SettingsService.CertificateRemarks;
using EsignBackend.Services.SettingsService.SecurityQuestions;
using EsignBackend.Services.SettingsService.CallStatus;
using EsignBackend.Services.SettingsService.Departments;
using EsignBackend.Services.SettingsService.IdentificationDocument;
using EsignBackend.Services.SettingsService.CallsPriority;
using EsignBackend.Services.SettingsService.CustomerIdentifier;
using EsignBackend.Services.SettingsService.CertificateIssuer;
using EsignBackend.Services.SettingsService.IssueLocation;
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
using EsignBackend.Extensions.CashHandlers;

namespace EsignBackend
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment _env)
        {
            Configuration = configuration;
            env = _env;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment env { get; }
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
            services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(Configuration.
            GetConnectionString("DefaultConnection"))
            );
            services.AddControllersWithViews()
                .AddNewtonsoftJson(options =>
            options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );
            services.AddControllers();
            services.AddAutoMapper(typeof(Startup));
            services.AddRazorPages();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration.GetSection("AppSettings:Token").Value)),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero
                    };
                });
            services.AddHttpContextAccessor();

            services.AddHandlers(env);
            // services.AddScoped<ICharacterService, CharacterService>();

            var configuration = new ConfigurationBuilder()
                              .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)                           
                              .Build();

            services.AddSingleton<ILogger>(
                new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .CreateLogger());

            services.AddRateLimiting(Configuration);

            var address = configuration.GetSection("AppSettings").GetSection("FrontURL").Value;
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                builder => builder.WithOrigins(address)
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()                
                );
            });
            services.AddHsts(options =>
            {
                options.Preload = true;
                options.IncludeSubDomains = true;
                options.MaxAge = TimeSpan.FromDays(365);
            });

            services.AddValidation();
            services.AddConfiguration(Configuration);
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
        
            //app.UseCors(builder =>
            //{
            //    builder.WithOrigins("http://localhost:81");
            //    builder.AllowAnyOrigin();
            //    builder.AllowAnyMethod();
            //    builder.AllowAnyHeader().WithExposedHeaders("x-file-name");
            //    builder.AllowCredentials();
            //});

            app.UseRouting();
            app.UseHttpsRedirection();
            app.UseAuthentication();

            app.UseErrorHandlingMiddleware();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHangfireDashboard();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "ESign API V1");
            });

            app.UseHangfireDashboard();
            backgroundJobs.Enqueue(() => certificatesService.UpdateExpiredCertificates());
            recurringJobManager.AddOrUpdate("#Hangfire update expire certificates", () => certificatesService.UpdateExpiredCertificates(),
            Cron.HourInterval(8));
        }
    }
}