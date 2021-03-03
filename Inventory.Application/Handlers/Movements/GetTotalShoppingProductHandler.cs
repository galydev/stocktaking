using AutoMapper;
using Inventory.Application.DTOs;
using Inventory.Application.Queries.Movements;
using Inventory.Application.Wrappers;
using Inventory.Domain.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Inventory.Application.Handlers.Movements
{
    public class GetTotalShoppingProductHandler : IRequestHandler<GetTotalShoppingProductQuery, ApiResponse<ShoppingProductDto>>
    {
        private readonly IMovementService _movementService;
        private readonly IMapper _mapper;

        public GetTotalShoppingProductHandler(IMovementService movementService, IMapper mapper)
        {
            _movementService = movementService ?? throw new ArgumentNullException(nameof(movementService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<ApiResponse<ShoppingProductDto>> Handle(GetTotalShoppingProductQuery request, CancellationToken cancellationToken)
        {
            var result = await _movementService.GetTotalShoppingProduct(request.Id);
            var shoppingProduct = _mapper.Map<ShoppingProductDto>(result);
            return new ApiResponse<ShoppingProductDto>(shoppingProduct);
        }
    }
}
