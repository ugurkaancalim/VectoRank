using Microsoft.Extensions.DependencyInjection;
using SSE.VectorData.Application.Caching;
using SSE.VectorData.Application.Services.Implementations;
using SSE.VectorData.Application.Services.Interfaces;
using SSE.VectorData.Domain.Repositories.Implementations;
using SSE.VectorData.Domain.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSE.VectorData.Application.Extensions
{
    public static class ServiceExtension
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IVectorService, VectorService>();
            //services.AddSingleton<VectorMemoryCache>();
            return services;
        }
    }
}
