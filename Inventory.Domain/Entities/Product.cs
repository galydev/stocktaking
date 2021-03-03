using System;
using System.Collections.Generic;

namespace Inventory.Domain.Entities
{
    public class Product : BaseEntity
    {
        public Product()
        {
            Movements = new HashSet<Movement>();
        }

        public DateTime CreationDate { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Sku { get; set; }
        public decimal Price { get; set; }
        public int MinimunStock { get; set; }
        public int MaximumStock { get; set; }
        public int? Stock { get; set; }
        public virtual ICollection<Movement> Movements { get; set; }
    }
}