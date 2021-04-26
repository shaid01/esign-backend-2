using Microsoft.Extensions.DependencyInjection;
//using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace EsignBackend.Extensions
{
    public static class SwaggerExtension
    {
        public static void AddSwagger(this IServiceCollection services)
        {
            //services.AddSwaggerGen(c =>
            //{
            //    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Esign", Version = "v1" });

            //    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            //    {
            //        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
            //        Name = "Authorization",
            //        In = ParameterLocation.Header,
            //        Type = SecuritySchemeType.ApiKey
            //    });

            //    var security = new OpenApiSecurityRequirement
            //    {
            //        { new OpenApiSecurityScheme
            //            {
            //                Reference = new OpenApiReference
            //                {
            //                    Type = ReferenceType.SecurityScheme, Id = "Bearer"
            //                },
            //                Scheme = "oauth2",
            //                Name = "Bearer",
            //                In = ParameterLocation.Header
            //                },
            //               new List<string>()
            //            }
            //    };
            //    c.AddSecurityRequirement(security);
              
            //});
        }
    }
}
