using FluentValidation.AspNetCore;
using Inventory.Application.Filters;
using Inventory.Application.Handlers.Movements;
using Inventory.Application.Handlers.Products;
using Inventory.Application.Handlers.Warehouses;
using Inventory.Application.Validators;
using Inventory.Domain.Interfaces;
using Inventory.Domain.Services;
using Inventory.Infrastructure.Data;
using Inventory.Infrastructure.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;

namespace Inventory.Api.Extensions
{
    public static class ServiceCollectionExtencion
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<IWarehouseService, WarehouseService>();
            services.AddTransient<IMovementService, MovementService>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            return services;
        }

        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "Inventory API", Version = "v1" });
                var xmlFile = $"{ Assembly.GetExecutingAssembly().GetName().Name }.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);
            });
            return services;
        }

        public static IServiceCollection AddValidationFilters(this IServiceCollection services)
        {
            services.AddMvc(options =>
            {
                options.Filters.Add<ValidationFilter>();
            }).AddFluentValidation(options =>
            {
                options.RegisterValidatorsFromAssemblyContaining<MovementValidator>();
                options.RegisterValidatorsFromAssemblyContaining<ProductValidator>();
                options.RegisterValidatorsFromAssemblyContaining<WarehouseValidator>();
                options.RegisterValidatorsFromAssemblyContaining<CreateProductValidator>();
                options.RegisterValidatorsFromAssemblyContaining<RemoveProductWarehouseValidator>();
                options.RegisterValidatorsFromAssemblyContaining<MoveProductsValidator>();
                options.ImplicitlyValidateChildProperties = true;
            });
            return services;
        }

        public static IServiceCollection AddDatabaseContext(this IServiceCollection services, string connetionString)
        {
            services.AddDbContext<InventoryContext>(options =>
            {
                options.UseSqlServer(connetionString,
                     x => x.MigrationsAssembly("Inventory.Infrastructure.Migrations"));
                options.EnableSensitiveDataLogging();
            });
            return services;
        }

        public static IServiceCollection AddCorsPolicy(this IServiceCollection services)
        {
            services.AddCors(
             options => options.AddPolicy("CorsPolicyInventory",
             builder =>
             builder
                 .AllowAnyMethod()
                 .WithExposedHeaders("content-disposition")
                 .AllowAnyHeader()
                 .SetPreflightMaxAge(TimeSpan.FromSeconds(3600)))
             );
            return services;
        }

        public static IServiceCollection AddMediators(this IServiceCollection services)
        {
            services.AddMediatR(typeof(GetAllProductsHandler).Assembly,
                                typeof(GetProductByIdHandler).Assembly,
                                typeof(GetProductByNameHandler).Assembly,
                                typeof(CreateProductHandler).Assembly,
                                typeof(UpdateProductHandler).Assembly,
                                typeof(DeleteProductHandler).Assembly,
                                typeof(GetAllWarehouseHandler).Assembly,
                                typeof(CreateWarehouseHandler).Assembly,
                                typeof(GetTotalShoppingProductHandler).Assembly,
                                typeof(MovementProductHandler).Assembly,
                                typeof(RemoveProductWarehouseHandler).Assembly,
                                typeof(CreateMovementHandler).Assembly
                                );
            return services;
        }
    }
}