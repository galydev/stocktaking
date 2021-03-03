using AutoMapper;
using Inventory.Application.Commands.Products;
using Inventory.Application.DTOs;
using Inventory.Application.Wrappers;
using Inventory.Domain.CustomEntities;
using Inventory.Domain.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Inventory.Application.Handlers.Products
{
    public class CreateProductHandler : IRequestHandler<CreateProductCommand, ApiResponse<ProductDto>>
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public CreateProductHandler(IProductService productService, IMapper mapper)
        {
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<ApiResponse<ProductDto>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = _mapper.Map<CreateProduct>(request);
            var result = await _productService.InsertProductAsync(product);
            var newProductDto = _mapper.Map<ProductDto>(result);
            return new ApiResponse<ProductDto>(newProductDto);
        }
    }
}
