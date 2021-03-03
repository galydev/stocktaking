using Inventory.Application.DTOs;
using Inventory.Application.Wrappers;
using MediatR;
using System;

namespace Inventory.Application.Commands.Movements
{
    public class CreateMovementCommand : IRequest<ApiResponse<MovementDto>>
    {

        /// <summary>
        /// Movement id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Creation Time
        /// </summary>
        public DateTime MovementDate { get; set; }

        /// <summary>
        /// Type movement in/out
        /// </summary>
        public bool Type { get; set; }

        /// <summary>
        /// Quantity product
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// price shopping product
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Product id
        /// </summary>
        public Guid ProductId { get; set; }

        /// <summary>
        /// Warehouse id
        /// </summary>
        public Guid WarehouseId { get; set; }
    }
}
