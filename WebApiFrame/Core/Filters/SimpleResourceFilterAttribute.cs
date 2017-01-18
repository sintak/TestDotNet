using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiFrame.Core.Filters
{
    public class SimpleResourceFilterAttribute : Attribute, IResourceFilter
    {
        private readonly ILogger<SimpleResourceFilterAttribute> _logger;

        public SimpleResourceFilterAttribute(ILoggerFactory loggerFactory)
        {
            this._logger = loggerFactory.CreateLogger<SimpleResourceFilterAttribute>();
        }

        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            this._logger.LogInformation("ResourceFilter Executed!");
        }

        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            this._logger.LogInformation("ResourceFilter Executing!");
        }
    }
}
