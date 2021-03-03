using Inventory.Application.HttpErrors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Inventory.Application.Filters
{
    public class ValidationFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.ModelState.IsValid)
            {
                var validationErrors = context.ModelState
                    .Keys
                    .SelectMany(k => context.ModelState[k].Errors)
                    .Select(e => e.ErrorMessage)
                    .ToArray();
                var error = HttpError.CreateHttpValidationError(
                    statusCode: HttpStatusCode.BadRequest,
                    userMessage: new[] { "There are validation errors" },
                    validationErrors: validationErrors
                    );

                context.Result = new BadRequestObjectResult(error);
                return;
            }
            await next();
        }
    }
}