using Inventory.Application.DTOs;
using Inventory.Application.Wrappers;
using MediatR;
using System;

namespace Inventory.Application.Queries.Products
{
    public class GetProductByIdQuery : IRequest<ApiResponse<ProductDto>>
    {
        public Guid Id { get; }
        public GetProductByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
