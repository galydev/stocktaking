using Inventory.Application.DTOs;
using Inventory.Application.Wrappers;
using MediatR;
using System;

namespace Inventory.Application.Commands.Products
{
    public class UpdateProductCommand : IRequest<ApiResponse<bool>>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Sku { get; set; }
        public decimal Price { get; set; }
        public int MinimunStock { get; set; }
        public int MaximumStock { get; set; }
        public int? Stock { get; set; }
    }
}
