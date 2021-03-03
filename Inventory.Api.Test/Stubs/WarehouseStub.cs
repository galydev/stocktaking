using Inventory.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Inventory.Api.Test.Stubs
{
    public static class WarehouseStub
    {
        private static List<Warehouse> Warehouses
        {
            get
            {
                List<Warehouse> warehouses = new List<Warehouse>
                {
                    new Warehouse
                    {
                        Id = Guid.Parse("4d8830e2-465c-4a54-ad02-f875073c85dc"),
                        CreationDate = DateTime.Now,
                        Name = 1,
                        Description = "Bodega Medellin",
                        Location = "Medellin",
                        MaximumCapacity = 100,
                    },
                    new Warehouse
                    {
                        Id = Guid.Parse("7a56023f-4811-4a75-9768-6aaf7985bb1a"),
                        CreationDate = DateTime.Now,
                        Name = 2,
                        Description = "Bodega Cali",
                        Location = "Cali",
                        MaximumCapacity = 100,
                    },
                };
                return warehouses;
            }
        }

        public static IEnumerable<Warehouse> GetAllWarehouse()
        {
            return Warehouses;
        }

        public static bool ExistWarehouseAsync(Guid id)
            => Warehouses.Any(e => e.Id == id);
    }
}