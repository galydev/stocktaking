using Inventory.Application.DTOs;
using Inventory.Application.Wrappers;
using MediatR;
using System;

namespace Inventory.Application.Queries.Warehouses
{
    public class GetWarehouseByIdQuery : IRequest<ApiResponse<WarehouseDto>>
    {
        public Guid Id { get; set; }
        public GetWarehouseByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
