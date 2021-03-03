using FluentValidation;
using Inventory.Application.Commands.Movements;
using Microsoft.Extensions.Logging;

namespace Inventory.Application.Validators
{
    public class MoveProductsValidator : AbstractValidator<MoveProductCommand>
    {
        public MoveProductsValidator(ILogger<MoveProductsValidator> logger)
        {
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
            RuleFor(product => product.CurrentWarehouseId)
               .NotNull()
               .NotEmpty();
            RuleFor(product => product.NewWarehouseId)
               .NotNull()
               .NotEmpty();
            logger.LogTrace("----- INSTANCE CREATED - {ClassName}", GetType().Name);
        }
    }
}