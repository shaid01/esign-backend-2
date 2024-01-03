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
            services.AddTransient<IValidator<LoginDto>, LoginDtoValidator>();
            services.AddTransient<IValidator<BuUser>, UserValidator>();
            services.AddTransient<IValidator<Customer>, CustomerValidator>();
            services.AddTransient<IValidator<Certificate>, CertificatesValidator>();
            services.AddTransient<IValidator<CallPriority>, CallpriorityValidator>();
            services.AddTransient<IValidator<Callstatus>, CallStatusValidator>();
            services.AddTransient<IValidator<Isscert>, CertificateIssuerValidator>();
            services.AddTransient<IValidator<CertificaterMeark>, CertificateRemarksValidator>();
            services.AddTransient<IValidator<CertificatesStatus>, CertificatesstatusValidator>();
            services.AddTransient<IValidator<Custident>, CustomerIdentifierValidator>();
            services.AddTransient<IValidator<Department>, DepartmentsValidator>();
            services.AddTransient<IValidator<ExpirationType>, ExpirationTypeValidator>();
            services.AddTransient<IValidator<DocsType>, IdentificationDocumentValidator>();
            services.AddTransient<IValidator<IssPlace>, IssueLocationValidator>();
            services.AddTransient<IValidator<Project>, ProjectsValidator>();
            services.AddTransient<IValidator<SubProject>, SubProjectValidator>();
            services.AddTransient<IValidator<SecurityGuestion>, SecurityQuestionsValidator>();
            services.AddTransient<IValidator<SmartObject>, SmartObjectValidator>();

        }
    }
}
