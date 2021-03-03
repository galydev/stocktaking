using System;

namespace Inventory.Application.DTOs
{
    public class ProductDto
    {
        /// <summary>
        /// Identy
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Creation Time
        /// </summary>
        public DateTime CreationDate { get; set; }

        /// <summary>
        /// Product name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Product descripcion
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        ///Product sku
        /// </summary>
        public string Sku { get; set; }

        /// <summary>
        /// Product price unit
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Product minimun stock
        /// </summary>
        public int MinimunStock { get; set; }

        /// <summary>
        /// Product maximun stock
        /// </summary>
        public int MaximumStock { get; set; }

        /// <summary>
        /// product stock movements
        /// </summary>
        public int? Stock { get; set; }
    }
}