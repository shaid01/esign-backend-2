using Microsoft.Extensions.DependencyInjection;
using AspNetCoreRateLimit;
using System.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Builder;

namespace EsignBackend.Middlewares
{
    public static class RateLimitingMiddleware
    {
        public static IServiceCollection AddRateLimiting(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMemoryCache();

            services.Configure<IpRateLimitOptions>(options => configuration.GetSection("IpRateLimitingSettings").Bind(options));

            services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
            services.AddInMemoryRateLimiting();

            return services;
        }

        public static IApplicationBuilder UseRateLimiting(this IApplicationBuilder app)
        {
            app.UseIpRateLimiting();
            return app;
        }
    }
}
