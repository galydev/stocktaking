using System;

namespace Inventory.Domain.CustomEntities
{
    public class RemoveProductWarehouse
    {
        public int Quantity { get; set; }

        public decimal Price { get; set; }

        public Guid ProductId { get; set; }

        public Guid WarehouseId { get; set; }
    }
}