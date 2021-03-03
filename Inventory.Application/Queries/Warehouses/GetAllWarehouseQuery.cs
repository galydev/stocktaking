using Inventory.Application.DTOs;
using Inventory.Application.Wrappers;
using MediatR;
using System.Collections.Generic;

namespace Inventory.Application.Queries.Warehouses
{
    public class GetAllWarehouseQuery : IRequest<ApiResponse<IEnumerable<WarehouseDto>>>
    {
    }
}
