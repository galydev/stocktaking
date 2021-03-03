using AutoMapper;
using Inventory.Application.Commands.Movements;
using Inventory.Application.DTOs;
using Inventory.Application.Wrappers;
using Inventory.Domain.Entities;
using Inventory.Domain.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Inventory.Application.Handlers.Movements
{
    public class CreateMovementHandler : IRequestHandler<CreateMovementCommand, ApiResponse<MovementDto>>
    {
        private readonly IMovementService _movementService;
        private readonly IMapper _mapper;

        public CreateMovementHandler(IMovementService movementService, IMapper mapper)
        {
            _movementService = movementService ?? throw new ArgumentNullException(nameof(movementService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<ApiResponse<MovementDto>> Handle(CreateMovementCommand request, CancellationToken cancellationToken)
        {
            var movement = _mapper.Map<Movement>(request);
            var result = await _movementService.InsertProductWareahouse(movement);
            var movementDto = _mapper.Map<MovementDto>(result);
            return new ApiResponse<MovementDto>(movementDto);
        }
    }
}
