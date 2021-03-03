using Inventory.Application.DTOs;
using Inventory.Application.Wrappers;
using MediatR;
using System;

namespace Inventory.Application.Queries.Movements
{
    public class GetTotalShoppingProductQuery : IRequest<ApiResponse<ShoppingProductDto>>
    {
        public Guid Id { get; set; }
        public GetTotalShoppingProductQuery(Guid id)
        {
            Id = id;
        }
    }
}
