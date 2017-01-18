using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiFrame.Core.Filters
{
    public class SimpleResultFilterAttribute : Attribute, IResultFilter
    {
        private readonly ILogger<SimpleResultFilterAttribute> _logger;

        public SimpleResultFilterAttribute(ILoggerFactory loggerFactory)
        {
            this._logger = loggerFactory.CreateLogger<SimpleResultFilterAttribute>();
        }

        public void OnResultExecuted(ResultExecutedContext context)
        {
            this._logger.LogInformation("ResultFilter Executd!");
        }

        public void OnResultExecuting(ResultExecutingContext context)
        {
            this._logger.LogInformation("ResultFilter Executing!");
        }
    }
}
