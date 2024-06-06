using Microsoft.Extensions.DependencyInjection;
using SSE.VectorData.Domain.Repositories.Implementations;
using SSE.VectorData.Domain.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSE.VectorData.Domain.Extensions
{
    public static class RepositoryExtension
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IVectorRepository, VectorRepository>();

            return services;
        }
    }
}
