using System;
using System.Collections.Generic;

namespace Inventory.Domain.Entities
{
    public class Warehouse : BaseEntity
    {
        public Warehouse()
        {
            Movements = new HashSet<Movement>();
        }

        public DateTime CreationDate { get; set; }
        public int Name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public int MaximumCapacity { get; set; }
        public virtual ICollection<Movement> Movements { get; set; }
    }
}