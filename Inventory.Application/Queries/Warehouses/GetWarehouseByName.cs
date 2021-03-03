using Inventory.Application.DTOs;
using Inventory.Application.Wrappers;
using MediatR;

namespace Inventory.Application.Queries.Warehouses
{
    public class GetWarehouseByName : IRequest<ApiResponse<WarehouseDto>>
    {
        public string Name { get; set; }
        public GetWarehouseByName(string name)
        {
            Name = name;
        }
    }
}
