using Inventory.Application.Wrappers;
using MediatR;
using System;

namespace Inventory.Application.Commands.Products
{
    public class DeleteProductCommand : IRequest<ApiResponse<bool>>
    {
        public Guid Id { get; set; }
        public DeleteProductCommand(Guid id)
        {
            Id = id;
        }
    }
}
