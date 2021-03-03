using Inventory.Application.Commands.Products;
using Inventory.Application.Wrappers;
using Inventory.Domain.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Inventory.Application.Handlers.Products
{
    public class DeleteProductHandler : IRequestHandler<DeleteProductCommand, ApiResponse<bool>>
    {
        private readonly IProductService _productService;

        public DeleteProductHandler(IProductService productService)
        {
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));
        }

        public async Task<ApiResponse<bool>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var result = await _productService.DeleteAsync(request.Id);
            return new ApiResponse<bool>(result);
        }
    }
}
