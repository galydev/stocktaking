using Inventory.Application.DTOs;
using Inventory.Application.Wrappers;
using MediatR;

namespace Inventory.Application.Queries.Products
{
    public class GetProductByNameQuery : IRequest<ApiResponse<ProductDto>>
    {
        public string Name { get; set; }
        public GetProductByNameQuery(string name)
        {
            Name = name;
        }
    }
}
