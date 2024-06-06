using Microsoft.Extensions.Configuration;
using SSE.Engine.Infrastructure.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSE.Engine.Domain.Extensions
{
    public static class ConnectionStringExtension
    {
        public static void ConfigureConnectionStrings(this ConfigurationManager configurationManager)
        {
            ConnectionConstants.ConnectionStrings = new Dictionary<string, string>();
            var childrens = configurationManager.GetSection("ConnectionStrings").GetChildren();
            foreach (var children in childrens)
                ConnectionConstants.ConnectionStrings.Add(children.Key, children.Value);
            ConnectionConstants.AppsConnectionString = configurationManager["EngineConnectionStrings:Apps"];
        }
    }
}
