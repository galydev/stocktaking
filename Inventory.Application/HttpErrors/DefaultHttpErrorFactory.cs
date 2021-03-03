using Inventory.Domain.Exceptions;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Net;

namespace Inventory.Application.HttpErrors
{
    public class DefaultHttpErrorFactory : IHttpErrorFactory
    {
        private readonly IWebHostEnvironment _env;
        private readonly IDictionary<Type, Func<Exception, string, HttpError>> _factory;

        public DefaultHttpErrorFactory(IWebHostEnvironment env)
        {
            _env = env;
            _factory = new Dictionary<Type, Func<Exception, string, HttpError>>
            {
                { typeof(Exception),InternalServerError },
                { typeof(BusinessException), BadRequest }
            };
        }

        public HttpError CreateFrom(Exception exception, string traceId)
        {
            if (_factory.TryGetValue(exception.GetType(), out Func<Exception, string, HttpError> func))
            {
                return func(exception, traceId);
            }
            return _factory[typeof(Exception)](exception, traceId);
        }

        private HttpError BadRequest(Exception exception, string traceId)
        {
            return HttpError.Create(
                _env,
                statusCode: HttpStatusCode.BadRequest,
                userMessage: new[] { exception.Message },
                developerMessage: $"{exception.Message}\r\n{exception.StackTrace}",
                traceId: traceId
                );
        }

        private HttpError InternalServerError(Exception exception, string traceId)
        {
            return HttpError.Create(
                _env,
                statusCode: HttpStatusCode.InternalServerError,
                userMessage: new[] { exception.Message },
                developerMessage: $"{exception.Message}\r\n{exception.StackTrace}",
                traceId: traceId
                );
        }
    }
}