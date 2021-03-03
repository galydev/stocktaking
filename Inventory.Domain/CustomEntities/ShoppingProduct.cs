using System;

namespace Inventory.Domain.CustomEntities
{
    public class ShoppingProduct
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int? Stock { get; set; }
        public int ShoppingQuantity { get; set; }
        public decimal TotalPriceShopping { get; set; }
    }
}