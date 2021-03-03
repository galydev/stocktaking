using AutoMapper;
using Inventory.Application.DTOs;
using Inventory.Application.Queries.Warehouses;
using Inventory.Application.Wrappers;
using Inventory.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Inventory.Application.Handlers.Warehouses
{
    public class GetAllWarehouseHandler : IRequestHandler<GetAllWarehouseQuery, ApiResponse<IEnumerable<WarehouseDto>>>
    {
        private readonly IWarehouseService _warehouseService;
        private readonly IMapper _mapper;

        public GetAllWarehouseHandler(IWarehouseService warehouseService, IMapper mapper)
        {
            _warehouseService = warehouseService ?? throw new ArgumentNullException(nameof(warehouseService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<ApiResponse<IEnumerable<WarehouseDto>>> Handle(GetAllWarehouseQuery request, CancellationToken cancellationToken)
        {
            var warehouses = await _warehouseService.GetWarehousesAsync();
            var result = _mapper.Map<IEnumerable<WarehouseDto>>(warehouses);
            return new ApiResponse<IEnumerable<WarehouseDto>>(result);
        }
    }
}
