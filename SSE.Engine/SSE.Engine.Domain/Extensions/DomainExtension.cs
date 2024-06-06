using Microsoft.Extensions.DependencyInjection;
using SSE.Engine.Domain.Repositories.Implementations;
using SSE.Engine.Domain.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSE.Engine.Domain.Extensions
{
    public static class DomainExtension
    {
        public static void CreateDependencyInjectionsForDomainLayer(this IServiceCollection services)
        {
            services.AddScoped<IDocumentRepository, DocumentRepository>();
            services.AddScoped<IQueryRepository, QueryRepository>();
        }
    }
}
