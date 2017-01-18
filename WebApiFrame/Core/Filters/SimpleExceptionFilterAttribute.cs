using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiFrame.Core.Filters
{
    public class SimpleExceptionFilterAttribute : Attribute, IExceptionFilter
    {
        private readonly ILogger<SimpleExceptionFilterAttribute> _logger;

        public SimpleExceptionFilterAttribute(ILoggerFactory loggerFactory)
        {
            this._logger = loggerFactory.CreateLogger<SimpleExceptionFilterAttribute>();
        }

        public void OnException(ExceptionContext context)
        {
            this._logger.LogError("Exception Execute! Message:" + context.Exception.Message);
            context.ExceptionHandled = true;
        }
    }
}
