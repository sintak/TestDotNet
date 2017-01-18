using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace WebApiFrame.Core.Middlewares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class HelloworldMiddleware
    {
        private readonly RequestDelegate _next;

        public HelloworldMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {

            //return _next(httpContext);
            await httpContext.Response.WriteAsync("Hello Middleware!");
            await _next(httpContext);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class HelloworldMiddlewareExtensions
    {
        public static IApplicationBuilder UseHelloworldMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<HelloworldMiddleware>();
        }
    }
}
