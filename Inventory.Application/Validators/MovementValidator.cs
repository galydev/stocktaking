using FluentValidation;
using Inventory.Application.Commands.Movements;
using Microsoft.Extensions.Logging;

namespace Inventory.Application.Validators
{
    public class MovementValidator : AbstractValidator<CreateMovementCommand>
    {
        public MovementValidator(ILogger<MovementValidator> logger)
        {
            RuleFor(product => product.Type)
                .NotNull();
            RuleFor(product => product.Quantity)
                .NotNull()
                .NotEmpty()
                .NotEqual(0);
            RuleFor(product => product.Price)
                .NotNull()
                .NotEmpty()
                .NotEqual(0);
            RuleFor(product => product.ProductId)
               .NotNull()
               .NotEmpty();
            RuleFor(product => product.WarehouseId)
               .NotNull()
               .NotEmpty();
            logger.LogTrace("----- INSTANCE CREATED - {ClassName}", GetType().Name);
        }
    }
}