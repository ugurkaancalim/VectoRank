using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using SSE.App.Application.ExceptionHandling;
using SSE.App.Application.Services.Implementations;
using SSE.App.Application.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSE.App.Application.Extensions
{
    public static class ServicesExtension
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IAppClaimService, AppClaimService>();
            services.AddScoped<IResourceService, ResourceService>();
            services.AddScoped<IAppService, AppService>();
            services.AddScoped<IEngineService, EngineService>();
            services.AddTransient<AppErrorMiddleware>();
        }
    }
}
