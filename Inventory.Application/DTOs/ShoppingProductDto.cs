using System;

namespace Inventory.Application.DTOs
{
    public class ShoppingProductDto
    {
        /// <summary>
        /// Product Id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Product Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Product Quantity Stock
        /// </summary>
        public int Stock { get; set; }

        /// <summary>
        /// Shopping Quantity
        /// </summary>
        public int ShoppingQuantity { get; set; }

        /// <summary>
        /// Total Price Shopping
        /// </summary>
        public decimal TotalPriceShopping { get; set; }
    }
}