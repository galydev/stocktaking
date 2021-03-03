using AutoMapper;
using Inventory.Application.Commands.Warehouses;
using Inventory.Application.DTOs;
using Inventory.Application.Wrappers;
using Inventory.Domain.Entities;
using Inventory.Domain.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Inventory.Application.Handlers.Warehouses
{
    public class CreateWarehouseHandler : IRequestHandler<CreateWarehouseCommand, ApiResponse<WarehouseDto>>
    {
        private readonly IWarehouseService _warehouseService;
        private readonly IMapper _mapper;

        public CreateWarehouseHandler(IWarehouseService warehouseService, IMapper mapper)
        {
            _warehouseService = warehouseService ?? throw new ArgumentNullException(nameof(warehouseService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<ApiResponse<WarehouseDto>> Handle(CreateWarehouseCommand request, CancellationToken cancellationToken)
        {
            var warehouse = _mapper.Map<Warehouse>(request);
            var result = await _warehouseService.CreateWarehousesAsync(warehouse);
            var newWarehouse = _mapper.Map<WarehouseDto>(result);
            return new ApiResponse<WarehouseDto>(newWarehouse);
        }
    }
}
