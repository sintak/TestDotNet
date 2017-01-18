using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiFrame.Core.Filters
{
    public class SimpleActionFilterAttribute : Attribute, IActionFilter
    {
        private readonly ILogger<SimpleActionFilterAttribute> _logger;

        public SimpleActionFilterAttribute(ILoggerFactory loggerFactory)
        {
            this._logger = loggerFactory.CreateLogger<SimpleActionFilterAttribute>();
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            this._logger.LogInformation("ActionFilter Executed!");
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            this._logger.LogInformation("ActionFilter Executing!");
        }
    }
}
