using Inventory.Application.DTOs;
using Inventory.Application.Wrappers;
using MediatR;

namespace Inventory.Application.Commands.Products
{
    public class CreateProductCommand : IRequest<ApiResponse<ProductDto>>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Sku { get; set; }
        public decimal Price { get; set; }
        public int MinimunStock { get; set; }
        public int MaximumStock { get; set; }
    }
}
