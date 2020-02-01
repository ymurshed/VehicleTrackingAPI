using System.Net;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace VehicleTrackingAPI.Filters
{
    public class GlobalExceptionFilter : ExceptionFilterAttribute
    {
        private readonly ILogger<GlobalExceptionFilter> _logger;

        public GlobalExceptionFilter(ILogger<GlobalExceptionFilter> logger)
        {
            _logger = logger;
        }

        public override void OnException(ExceptionContext context)
        {
            var message = context.Exception.Message;
            var stackTrace = context.Exception.StackTrace;
            var error = GetErrorDetails(message, stackTrace);
            _logger.LogError(error); 

            const HttpStatusCode status = HttpStatusCode.InternalServerError;
            var response = context.HttpContext.Response;
            response.StatusCode = (int)status;
            context.ExceptionHandled = true;
            response.WriteAsync(error);
        }

        private static string GetErrorDetails(string message, string stackTrace)
        {
            var builder = new StringBuilder();
            builder.Append("Exception Occured: ");
            builder.Append(message);
            builder.AppendLine();
            builder.Append("Trace: ");
            builder.Append(stackTrace);
            builder.AppendLine();
            var error = builder.ToString();
            return error;
        }
    }
}
