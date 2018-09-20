using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;

namespace Blog.WebApi.Middleware
{
    public class DevExceptionHandlerMiddleware
    {
        private readonly ILogger<DevExceptionHandlerMiddleware> _logger;
        private readonly RequestDelegate _next;

        public DevExceptionHandlerMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<DevExceptionHandlerMiddleware>();
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                if (httpContext.Response.HasStarted)
                {
                    _logger.LogWarning("The response has already started, the error handler will not be executed.");
                    throw;
                }

                _logger.LogCritical(ex, "An error occured");
                httpContext.Response.Clear();
                httpContext.Response.StatusCode = 500;
                httpContext.Response.ContentType = new MediaTypeHeaderValue("application/json").ToString();

                var error = new Error
                {
                    Message = ex.Message,
                    Source = ex.Source,
                    StackTrace = ex.StackTrace
                };

                await httpContext.Response.WriteAsync(JsonConvert.SerializeObject(error));
            }
        }

        internal class Error
        {
            public string Message { get; set; }
            public string StackTrace { get; set; }
            public string Source { get; set; }
        }
    }

    public static class DevExceptionHandlerMiddlewareExtensions
    {
        public static IApplicationBuilder DevExceptionHandlerMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<DevExceptionHandlerMiddleware>();
        }
    }
}