using Inventory.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Inventory.Domain.Test.Stubs
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
                        Description = "Bodega SantaMarta",
                        Location = "SantaMarta",
                        MaximumCapacity = 100,
                    },
                    new Warehouse
                    {
                        Id = Guid.Parse("7a56023f-4811-4a75-9768-6aaf7985bb1a"),
                        CreationDate = DateTime.Now,
                        Name = 2,
                        Description = "Bodega Pereira",
                        Location = "Pereira",
                        MaximumCapacity = 100,
                    },
                    new Warehouse
                    {
                        Id = Guid.Parse("351BA5AA-C78D-4491-931A-76603D729392"),
                        CreationDate = DateTime.Now,
                        Name = 3,
                        Description = "Bodega Manizales",
                        Location = "Manizales",
                        MaximumCapacity = 100,
                    }
                };
                return warehouses;
            }
        }

        public static IEnumerable<Warehouse> GetAllWarehouse()
            => Warehouses;

        public static bool ExistWarehouseAsync()
            => true;

        public static Warehouse InsertWarehouse(Warehouse warehouse)
        {
            warehouse.Id = Guid.NewGuid();
            return warehouse;
        }

        public static Warehouse GetProductByName(int name)
           => Warehouses.FirstOrDefault(x => x.Name.Equals(name));

        public static IQueryable<Warehouse> GetAllWarehouseQueryable()
            => Warehouses.AsQueryable();
    }
}