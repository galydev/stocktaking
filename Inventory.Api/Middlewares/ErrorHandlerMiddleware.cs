using Inventory.Application.HttpErrors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace Inventory.Api.Middlewares
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlerMiddleware> _logger;
        private IWebHostEnvironment _env;

        public ErrorHandlerMiddleware(RequestDelegate next, ILogger<ErrorHandlerMiddleware> logger)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task Invoke(HttpContext context, IWebHostEnvironment env)
        {
            try
            {
                _env = env;
                await _next(context);
            }
            catch (Exception exception)
            {
                await CreateHttpError(context, exception);
                _logger.LogError(exception.HResult, exception, exception.Message);
            }
        }

        private async Task CreateHttpError(HttpContext context, Exception exception)
        {
            var error = new DefaultHttpErrorFactory(_env).CreateFrom(exception, context.TraceIdentifier);
            await WriteResponseAsync(context, JsonConvert.SerializeObject(error), "application/json", error.Status);
        }

        private Task WriteResponseAsync(HttpContext context, string content, string contentType, int statusCode)
        {
            context.Response.Headers["Content-Type"] = new[] { contentType };
            context.Response.Headers["Cache-Control"] = new[] { "no-cache, no-store" };
            context.Response.Headers["pragma"] = new[] { "no-cache" };
            context.Response.Headers["Expires"] = new[] { "0" };
            context.Response.StatusCode = statusCode;

            return context.Response.WriteAsync(content);
        }
    }
}