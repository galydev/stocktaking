using AutoMapper;
using Inventory.Application.Commands.Products;
using Inventory.Application.Wrappers;
using Inventory.Domain.Entities;
using Inventory.Domain.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Inventory.Application.Handlers.Products
{
    public class UpdateProductHandler : IRequestHandler<UpdateProductCommand, ApiResponse<bool>>
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public UpdateProductHandler(IProductService productService, IMapper mapper)
        {
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<ApiResponse<bool>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = _mapper.Map<Product>(request);
            var result = await _productService.UpdateProductAsync(product);
            return new ApiResponse<bool>(result);
        }
    }
}
