using Microsoft.AspNetCore.Builder;
using SSE.App.Application.ExceptionHandling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSE.App.Application.Extensions
{
    public static class ErrorMiddlewareException
    {
        public static IApplicationBuilder UseErrorMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AppErrorMiddleware>();
        }
    }
}
