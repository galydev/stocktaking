using AutoMapper;
using Inventory.Application.Commands.Movements;
using Inventory.Application.Commands.Products;
using Inventory.Application.DTOs;
using Inventory.Domain.CustomEntities;
using Inventory.Domain.Entities;

namespace Inventory.Application.Mappings
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<ProductDto, Product>();
            CreateMap<Product, ProductDto>();
            CreateMap<WarehouseDto, Warehouse>();
            CreateMap<Warehouse, WarehouseDto>();
            CreateMap<Movement, MovementDto>();
            CreateMap<MovementDto, Movement>();
            CreateMap<MoveProductsDto, MoveProducts>();
            CreateMap<MoveProducts, MoveProductsDto>();
            CreateMap<ShoppingProductDto, ShoppingProduct>();
            CreateMap<ShoppingProduct, ShoppingProductDto>();
            CreateMap<RemoveProductWarehouse, RemoveProductWarehouseDto>();
            CreateMap<RemoveProductWarehouseDto, RemoveProductWarehouse>();
            CreateMap<CreateProduct, CreateProductDto>();
            CreateMap<CreateProductDto, CreateProduct>();
            CreateMap<CreateProductCommand, CreateProduct>();
            CreateMap<UpdateProductCommand, Product>();
            CreateMap<MoveProductCommand, MoveProducts>();
            CreateMap<RemoveProductWarehouseCommand, RemoveProductWarehouse>();
            CreateMap<CreateMovementCommand, Movement>();
        }
    }
}