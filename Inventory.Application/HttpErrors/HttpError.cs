using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System.Net;

namespace Inventory.Application.HttpErrors
{
    public class HttpError
    {
        public bool Succeeded { get; set; }
        public int Status { get; set; }
        public string[] Message { get; set; }
        public string TraceId { get; set; }
        public string DeveloperMessage { get; set; }
        public string[] ValidationErrors { get; set; }

        public static HttpError CreateHttpValidationError(HttpStatusCode statusCode, string[] userMessage, string[] validationErrors)
        {
            var httpError = CreateDefaultHttpError(statusCode, userMessage);
            httpError.ValidationErrors = validationErrors;
            return httpError;
        }

        public static HttpError Create(
            IWebHostEnvironment environment,
            HttpStatusCode statusCode,
            string[] userMessage,
            string developerMessage,
            string traceId)
        {
            var httpError = CreateDefaultHttpError(statusCode, userMessage);
            if (environment.IsDevelopment())
            {
                httpError.DeveloperMessage = developerMessage;
            }
            httpError.TraceId = traceId;
            return httpError;
        }

        private static HttpError CreateDefaultHttpError(HttpStatusCode statusCode, string[] Message)
        {
            var httpError = new HttpError
            {
                Status = (int)statusCode,
                Message = Message,
                Succeeded = false
            };
            return httpError;
        }
    }
}