using System;

namespace Inventory.Domain.CustomEntities
{
    public class MoveProducts
    {
        public int Quantity { get; set; }
        public Guid ProductId { get; set; }
        public decimal Price { get; set; }
        public Guid CurrentWarehouseId { get; set; }
        public Guid NewWarehouseId { get; set; }
    }
}