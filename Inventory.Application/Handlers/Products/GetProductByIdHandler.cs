using AutoMapper;
using Inventory.Application.DTOs;
using Inventory.Application.Queries.Products;
using Inventory.Application.Wrappers;
using Inventory.Domain.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Inventory.Application.Handlers.Products
{
    public class GetProductByIdHandler : IRequestHandler<GetProductByIdQuery, ApiResponse<ProductDto>>
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;
        public GetProductByIdHandler(IProductService productService, IMapper mapper)
        {
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<ApiResponse<ProductDto>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await _productService.GetProductByIdAsync(request.Id);
            var productDto = _mapper.Map<ProductDto>(product);
            return new ApiResponse<ProductDto>(productDto);
        }
    }
}
