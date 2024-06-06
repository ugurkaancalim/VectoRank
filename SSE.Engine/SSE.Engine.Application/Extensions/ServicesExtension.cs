using Microsoft.Extensions.DependencyInjection;
using SSE.Engine.Application.Services.Implementations;
using SSE.Engine.Application.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSE.Engine.Application.Extensions
{
    public static class ServicesExtension
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IVectorDataService, VectorDataService>();
            services.AddScoped<IAppService, AppService>();
            services.AddScoped<IDocumentService, DocumentService>();
            services.AddScoped<IQueryService, QueryService>();
            services.AddScoped<IClaimService, ClaimService>();
            return services;
        }
    }
}
