using System;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using AccountingChildrens.Domain;
using System.Text.Json;

namespace AccountingChildrens.WebApi.Moddleware
{
    public class ExceptionHandlingMiddleware : IMiddleware
    {
        private readonly ILoggerManager _logger;

        public ExceptionHandlingMiddleware(ILoggerManager logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());

                await HandleExceptionAsync(context, e);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
        {
            httpContext.Response.ContentType = "application/json";

            httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;

            var response = new
            {
                error = exception.Message
            };

            await httpContext.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}

