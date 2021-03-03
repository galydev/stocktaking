using Inventory.Application.DTOs;
using Inventory.Application.Wrappers;
using MediatR;
using System;

namespace Inventory.Application.Commands.Warehouses
{
    public class CreateWarehouseCommand : IRequest<ApiResponse<WarehouseDto>>
    {        

        /// <summary>
        /// Creation Time
        /// </summary>
        public DateTime CreationDate { get; set; }

        /// <summary>
        /// Warehouse name
        /// </summary>
        public int Name { get; set; }

        /// <summary>
        /// Warehouse description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Warehouse location
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// Warehouse Maximun capacity
        /// </summary>
        public int MaximumCapacity { get; set; }
    }
}
