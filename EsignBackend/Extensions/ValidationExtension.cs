using EsignBackend.Dtos.Login;
using EsignBackend.Models;
using EsignBackend.Validation;
using EsignBackend.Validation.Settings;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EsignBackend.Extensions
{
    public static class ValidationExtension
    {
        public static void AddValidation(this IServiceCollection services)
        {
            //services.AddHttpContextAccessor(); //30/03/2025
            services.AddTransient<IValidator<LoginDto>, LoginDtoValidator>();
            services.AddTransient<IValidator<Buuser>, UserValidator>();
            services.AddTransient<IValidator<Customer>, CustomerValidator>();
            services.AddTransient<IValidator<Certificate>, CertificatesValidator>();
            services.AddTransient<IValidator<Callpriority>, CallpriorityValidator>();
            services.AddTransient<IValidator<Callstatus>, CallStatusValidator>();
            services.AddTransient<IValidator<Isscert>, CertificateIssuerValidator>();
            services.AddTransient<IValidator<CertificateRemark>, CertificateRemarksValidator>();
            services.AddTransient<IValidator<Certificatesstatus>, CertificatesstatusValidator>();
            services.AddTransient<IValidator<Custident>, CustomerIdentifierValidator>();
            services.AddTransient<IValidator<Department>, DepartmentsValidator>();
            services.AddTransient<IValidator<Expirationtype>, ExpirationTypeValidator>();
            services.AddTransient<IValidator<Docstype>, IdentificationDocumentValidator>();
            services.AddTransient<IValidator<Issplace>, IssueLocationValidator>();
            services.AddTransient<IValidator<Project>, ProjectsValidator>();
            services.AddTransient<IValidator<Subproject>, SubProjectValidator>();
            services.AddTransient<IValidator<Securityquestion>, SecurityQuestionsValidator>();
            services.AddTransient<IValidator<Smartobject>, SmartObjectValidator>();

        }
    }
}
