using System.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using VehicleTrackingAPI.Utility;

namespace VehicleTrackingAPI.Filters
{
    public class LoggingFilter : ActionFilterAttribute
    {
        private readonly ILogger<GlobalExceptionFilter> _logger;
        private readonly IHostingEnvironment _hostingEnvironment;
        
        public LoggingFilter(ILogger<GlobalExceptionFilter> logger, IHostingEnvironment hostingEnvironment)
        {
            _logger = logger;
            _hostingEnvironment = hostingEnvironment;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (_hostingEnvironment.IsDevelopment())
            {
                var loggingWatch = Stopwatch.StartNew();
                context.HttpContext.Items.Add(Constants.StopwatchKey, loggingWatch);

                var controllerName = context.ActionDescriptor.RouteValues.ContainsKey("controller")
                    ? context.ActionDescriptor.RouteValues["controller"] : string.Empty;

                var actionName = context.ActionDescriptor.RouteValues.ContainsKey("action")
                    ? context.ActionDescriptor.RouteValues["action"] : string.Empty;

                var message = $"Executing /{controllerName}/{actionName}";
                _logger.LogInformation(message);
            }
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if (_hostingEnvironment.IsDevelopment())
            {
                if (context.HttpContext.Items[Constants.StopwatchKey] != null)
                {
                    var loggingWatch = (Stopwatch)context.HttpContext.Items[Constants.StopwatchKey];
                    loggingWatch.Stop();

                    var timeSpent = loggingWatch.ElapsedMilliseconds;

                    var controllerName = context.ActionDescriptor.RouteValues.ContainsKey("controller")
                        ? context.ActionDescriptor.RouteValues["controller"] : string.Empty;

                    var actionName = context.ActionDescriptor.RouteValues.ContainsKey("action")
                        ? context.ActionDescriptor.RouteValues["action"] : string.Empty;

                    var message = $"Finished executing /{controllerName}/{actionName} - time spent: {timeSpent} ms";

                    _logger.LogInformation(message);
                    context.HttpContext.Items.Remove(Constants.StopwatchKey);
                }
            }
        }
    }
}
