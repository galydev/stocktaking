using System;

namespace Inventory.Application.DTOs
{
    public class MoveProductsDto
    {
        /// <summary>
        /// Product Quantity
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Product Id
        /// </summary>
        public Guid ProductId { get; set; }

        /// <summary>
        /// Price shopping product
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Current Warehouse Id
        /// </summary>
        public Guid CurrentWarehouseId { get; set; }

        /// <summary>
        /// New Warehouse Id
        /// </summary>
        public Guid NewWarehouseId { get; set; }
    }
}