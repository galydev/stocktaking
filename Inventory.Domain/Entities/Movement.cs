using System;

namespace Inventory.Domain.Entities
{
    public class Movement : BaseEntity
    {
        public DateTime MovementDate { get; set; }
        public bool Type { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public Guid ProductId { get; set; }
        public Guid WarehouseId { get; set; }
        public virtual Product Product { get; set; }
        public virtual Warehouse Warehouse { get; set; }
    }
}