using System;

namespace Inventory.Application.DTOs
{
    public class RemoveProductWarehouseDto
    {
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