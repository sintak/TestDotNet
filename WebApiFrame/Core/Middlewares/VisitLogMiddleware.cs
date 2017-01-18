using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using WebApiFrame.Models;
using System.IO;

namespace WebApiFrame.Core.Middlewares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    /// <summary>
    /// 访问日志记录中间件
    /// </summary>
    public class VisitLogMiddleware
    {
        private readonly RequestDelegate _next;

        private readonly ILogger logger;
        private VisitLog visitLog;

        public VisitLogMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _next = next;
            this.logger = loggerFactory.CreateLogger<VisitLogMiddleware>();
        }

        public async Task Invoke(HttpContext httpContext)
        {

            //return _next(httpContext);
            this.visitLog = new VisitLog();
            HttpRequest request = httpContext.Request;
            visitLog.Url = request.Path.ToString();
            visitLog.Headers = request.Headers.ToDictionary(k => k.Key, v => string.Join(";", v.Value.ToList()));
            visitLog.Method = request.Method;
            visitLog.ExcuteStartTime = DateTime.Now;

            using (StreamReader reader = new StreamReader(request.Body))
            {
                visitLog.RequestBody = reader.ReadToEnd();
            }

            httpContext.Response.OnCompleted(ResponseCompletedCallback, httpContext);
            await _next(httpContext);
        }

        private Task ResponseCompletedCallback(object obj)
        {
            visitLog.ExcuteEndTime = DateTime.Now;
            logger.LogInformation($"VisitLog: {visitLog.ToString()}");
            return Task.FromResult(0);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class VisitLogMiddlewareExtensions
    {
        public static IApplicationBuilder UseVisitLogMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<VisitLogMiddleware>();
        }
    }
}
