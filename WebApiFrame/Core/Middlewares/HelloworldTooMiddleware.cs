using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace WebApiFrame.Core.Middlewares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class HelloworldTooMiddleware
    {
        private readonly RequestDelegate _next;

        public HelloworldTooMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {

            //return _next(httpContext);
            await httpContext.Response.WriteAsync("Hello Middleware too!");
            //await this._next(httpContext);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class HelloworldTooMiddlewareExtensions
    {
        public static IApplicationBuilder UseHelloworldTooMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<HelloworldTooMiddleware>();
        }
    }
}
