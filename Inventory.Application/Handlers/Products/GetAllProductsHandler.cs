using AutoMapper;
using Inventory.Application.DTOs;
using Inventory.Application.Queries.Products;
using Inventory.Application.Wrappers;
using Inventory.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Inventory.Application.Handlers.Products
{
    public class GetAllProductsHandler : IRequestHandler<GetAllProductsQuery, ApiResponse<IEnumerable<ProductDto>>>
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;
        public GetAllProductsHandler(IProductService productService, IMapper mapper)
        {
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<ApiResponse<IEnumerable<ProductDto>>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await _productService.GetProductsAsync();
            var productsDto = _mapper.Map<IEnumerable<ProductDto>>(products);
            return new ApiResponse<IEnumerable<ProductDto>>(productsDto);
        }
    }
}
