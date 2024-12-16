using EsignBackend.Extensions.CacheHandlers;
using EsignBackend.Services.CharacterService;
using EsignBackend.Services.MainServices.Certificates;
using EsignBackend.Services.SettingsService.CallsPriority;
using EsignBackend.Services.SettingsService.CallStatus;
using EsignBackend.Services.SettingsService.CertificateIssuer;
using EsignBackend.Services.SettingsService.CertificateRemarks;
using EsignBackend.Services.SettingsService.CertificatesStatus;
using EsignBackend.Services.SettingsService.CustomerIdentifier;
using EsignBackend.Services.SettingsService.Departments;
using EsignBackend.Services.SettingsService.Expirationtype;
using EsignBackend.Services.SettingsService.IdentificationDocument;
using EsignBackend.Services.SettingsService.IssueLocation;
using EsignBackend.Services.SettingsService.SecurityQuestions;
using EsignBackend.Services.SettingsService.SmartObject;
using EsignBackend.Services.SettingsService;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace EsignBackend.Extensions
{
    public static class HandlersExtention
    {
        public static void AddHandlers(this IServiceCollection services, IWebHostEnvironment env)
        {
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IUsersService, UserService>();
            services.AddScoped<ICustomersService, CustomersService>();
            services.AddScoped<IProjectsService, ProjectsService>();
            services.AddScoped<ICertificatesstatusService, CertificatesstatusService>();
            services.AddScoped<ISmartObjectService, SmartObjectService>();
            services.AddScoped<IExpirationTypeService, ExpirationTypeService>();
            services.AddScoped<ICertificateRemarksService, CertificateRemarksService>();
            services.AddScoped<ISecurityQuestionsService, SecurityQuestionsService>();
            services.AddScoped<ICallStatusService, CallStatusService>();
            services.AddScoped<IDepartmentsService, DepartmentsService>();
            services.AddScoped<IIdentificationDocumentService, IdentificationDocumentService>();
            services.AddScoped<ICallsPriorityService, CallsPriorityService>();
            services.AddScoped<ICustomerIdentifierService, CustomerIdentifierService>();
            services.AddScoped<ICertificateIssuerService, CertificateIssuerService>();
            services.AddScoped<IIssueLocationService, IssueLocationService>();
            services.AddScoped<ICertificatesService, CertificatesService>();

            services.AddSingleton<ICache, CacheHandler>();

        }
    }
}
