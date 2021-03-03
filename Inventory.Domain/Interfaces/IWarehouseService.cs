using Inventory.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Inventory.Domain.Interfaces
{
    public interface IWarehouseService
    {
        Task<IEnumerable<Warehouse>> GetWarehousesAsync();

        Task<Warehouse> CreateWarehousesAsync(Warehouse warehouse);

        Task<bool> ExistWarehouseAsync(Guid id);

        Task<Warehouse> GetWarehouseByNameAsync(int name);
    }
}