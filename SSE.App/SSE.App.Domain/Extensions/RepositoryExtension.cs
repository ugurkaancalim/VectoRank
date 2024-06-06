using Microsoft.Extensions.DependencyInjection;
using SSE.App.Domain.Repositories.Implementations;
using SSE.App.Domain.Repositories.Interfaces;
using SSE.App.Domain.UnitOfWork.Implementations;
using SSE.App.Domain.UnitOfWork.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSE.App.Domain.Extensions
{
    public static class RepositoryExtension
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            //services.AddScoped<IAppRepository, AppRepository>();
            //services.AddScoped<IDataSourceRepository, DataSourceRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork.Implementations.UnitOfWork>();
        }
    }
}
