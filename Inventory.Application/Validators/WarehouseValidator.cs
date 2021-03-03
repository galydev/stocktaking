using FluentValidation;
using Inventory.Application.Commands.Warehouses;
using Microsoft.Extensions.Logging;

namespace Inventory.Application.Validators
{
    public class WarehouseValidator : AbstractValidator<CreateWarehouseCommand>
    {
        public WarehouseValidator(ILogger<WarehouseValidator> logger)
        {
            RuleFor(Warehouse => Warehouse.Name)
                .NotNull()
                .NotEmpty();
            RuleFor(Warehouse => Warehouse.Description)
                .NotEqual(string.Empty)
                .NotEqual(typeof(string).Name.ToLower())
                .MaximumLength(500);
            RuleFor(Warehouse => Warehouse.Location)
                .NotNull()
                .NotNull()
                .NotEqual(string.Empty)
                .NotEqual(typeof(string).Name.ToLower())
                .MaximumLength(500);
            RuleFor(Warehouse => Warehouse.MaximumCapacity)
                .NotNull()
                .NotEmpty()
                .NotEqual(0);
            logger.LogTrace("----- INSTANCE CREATED - {ClassName}", GetType().Name);
        }
    }
}