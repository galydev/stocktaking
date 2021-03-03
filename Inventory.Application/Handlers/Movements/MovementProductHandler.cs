using AutoMapper;
using Inventory.Application.Commands.Movements;
using Inventory.Application.DTOs;
using Inventory.Application.Wrappers;
using Inventory.Domain.CustomEntities;
using Inventory.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Inventory.Application.Handlers.Movements
{
    public class MovementProductHandler : IRequestHandler<MoveProductCommand, ApiResponse<IEnumerable<MoveProductsDto>>>
    {
        private readonly IMovementService _movementService;
        private readonly IMapper _mapper;

        public MovementProductHandler(IMovementService movementService, IMapper mapper)
        {
            _movementService = movementService ?? throw new ArgumentNullException(nameof(movementService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async  Task<ApiResponse<IEnumerable<MoveProductsDto>>> Handle(MoveProductCommand request, CancellationToken cancellationToken)
        {
            var moveProduct = _mapper.Map<MoveProducts>(request);
            var result = await _movementService.MoveProductOtherWarehouse(moveProduct);
            var movemenstDto = _mapper.Map<IEnumerable<MoveProductsDto>>(result);
            return new ApiResponse<IEnumerable<MoveProductsDto>>(movemenstDto);
        }
    }
}
