using AutoMapper;
using Inventory.Application.Commands.Movements;
using Inventory.Application.Wrappers;
using Inventory.Domain.CustomEntities;
using Inventory.Domain.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Inventory.Application.Handlers.Movements
{
    public class RemoveProductWarehouseHandler : IRequestHandler<RemoveProductWarehouseCommand, ApiResponse<bool>>
    {
        private readonly IMovementService _movementService;
        private readonly IMapper _mapper;

        public RemoveProductWarehouseHandler(IMovementService movementService, IMapper mapper)
        {
            _movementService = movementService ?? throw new ArgumentNullException(nameof(movementService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<ApiResponse<bool>> Handle(RemoveProductWarehouseCommand request, CancellationToken cancellationToken)
        {
            var movement = _mapper.Map<RemoveProductWarehouse>(request);
            var result = await _movementService.RemoveProductsOfWarehouse(movement);
            return new ApiResponse<bool>(result);
        }
    }
}
