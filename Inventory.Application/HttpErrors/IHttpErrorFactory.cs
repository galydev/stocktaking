using System;

namespace Inventory.Application.HttpErrors
{
    public interface IHttpErrorFactory
    {
        HttpError CreateFrom(Exception exception, string traceId);
    }
}