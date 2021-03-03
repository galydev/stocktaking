﻿using FluentValidation;
using Inventory.Application.Commands.Products;
using Microsoft.Extensions.Logging;

namespace Inventory.Application.Validators
{
    public class CreateProductValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductValidator(ILogger<CreateProductValidator> logger)
        {
            RuleFor(product => product.Name)
               .NotNull()
               .NotEmpty()
               .NotEqual(string.Empty)
               .NotEqual(typeof(string).Name.ToLower())
               .MaximumLength(500);
            RuleFor(product => product.Description)
                .NotNull()
                .NotEmpty()
                .NotEqual(string.Empty)
                .NotEqual(typeof(string).Name.ToLower())
                .MaximumLength(500);
            RuleFor(product => product.Sku)
               .NotNull()
               .NotEmpty()
               .NotEqual(string.Empty)
               .NotEqual(typeof(string).Name.ToLower())
               .MaximumLength(500);
            RuleFor(product => product.Price)
               .NotNull()
               .NotEmpty()
               .NotEqual(0);
            RuleFor(product => product.MinimunStock)
               .NotNull()
               .NotEmpty()
               .NotEqual(0);
            RuleFor(product => product.MaximumStock)
               .NotNull()
               .NotEmpty()
               .NotEqual(0);

            logger.LogTrace("----- INSTANCE CREATED - {ClassName}", GetType().Name);
        }
    }
}