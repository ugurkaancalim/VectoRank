using Microsoft.AspNetCore.Http;
using SSE.App.Application.ExceptionHandling.ExceptionTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSE.App.Application.ExceptionHandling
{
    public class AppErrorMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (AppException appEx)
            {
                await HandleExceptionAsync(context, appEx);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }
        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            // send message to logger

            context.Response.ContentType = "text/plain";
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            return context.Response.WriteAsync("Internal Server Error");
        }
        private Task HandleExceptionAsync(HttpContext context, AppException exception)
        {
            // send message to logger

            context.Response.ContentType = "text/plain";
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            return context.Response.WriteAsync(exception.ResponseMessage);
        }
    }
}
