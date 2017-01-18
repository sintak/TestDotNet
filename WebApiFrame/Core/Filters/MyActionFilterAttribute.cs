using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiFrame.Core.Filters
{
    //public class MyActionFilterAttribute : Attribute, IActionFilter
    //{
    //    private readonly ILogger<MyActionFilterAttribute> _logger;

    //    public MyActionFilterAttribute(ILoggerFactory loggerFactory)
    //    {
    //        this._logger = loggerFactory.CreateLogger<MyActionFilterAttribute>();
    //    }

    //    private readonly string _key;
    //    private readonly string _value;

    //    public MyActionFilterAttribute(string key, string value)
    //    {
    //        _key = key;
    //        _value = value;
    //    }

    //    public void OnActionExecuted(ActionExecutedContext context)
    //    {

    //    }

    //    public void OnActionExecuting(ActionExecutingContext context)
    //    {
    //        context.HttpContext.Response.Headers.Add(this._key, this._value);
    //        //this._logger.LogInformation("MyActionFilterAttribute Executiong!");
    //    }

    //}

    // or

    //public class MyActionFilterAttribute : TypeFilterAttribute
    //{
    //    public MyActionFilterAttribute() : base(typeof(MyActionFilterImpl))
    //    {

    //    }

    //    private class MyActionFilterImpl : IActionFilter
    //    {
    //        private readonly ILogger<MyActionFilterImpl> _logger;

    //        public MyActionFilterImpl(ILoggerFactory loggerFactory)
    //        {
    //            this._logger = loggerFactory.CreateLogger<MyActionFilterImpl>();
    //        }

    //        public void OnActionExecuted(ActionExecutedContext context)
    //        {

    //        }

    //        public void OnActionExecuting(ActionExecutingContext context)
    //        {
    //            context.HttpContext.Response.Headers.Add("My-Header", "WebApiFrame-Header");
    //            this._logger.LogInformation("MyActionFilterAttribute Executiong!");
    //        }
    //    }
    //}

    // or

    public class MyActionFilterAttribute : Attribute, IActionFilter, IOrderedFilter
    {
        private readonly int _order;

        private readonly string _target;

        private readonly ILogger<MyActionFilterAttribute> logger;

        public int Order
        {
            get
            {
                return _order;
            }
        }

        public MyActionFilterAttribute(string target, int order = 0)
        {
            _order = order;
            _target = target;

            ILoggerFactory loggerFactory = new LoggerFactory();
            loggerFactory.WithFilter(new FilterLoggerSettings()
            {
                { "Microsoft", LogLevel.Warning }
            })
            .AddConsole().AddDebug();

            logger = loggerFactory.CreateLogger<MyActionFilterAttribute>();
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            logger.LogInformation($"{_target} Executed!");
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            logger.LogInformation($"{_target} Executing!");
        }
    }
}
