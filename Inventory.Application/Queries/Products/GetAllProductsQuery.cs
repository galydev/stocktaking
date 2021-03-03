using Inventory.Application.DTOs;
using Inventory.Application.Wrappers;
using MediatR;
using System.Collections.Generic;

namespace Inventory.Application.Queries.Products
{
    public class GetAllProductsQuery : IRequest<ApiResponse<IEnumerable<ProductDto>>>
    {
    }
}
