using Inventory.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Inventory.Domain.Interfaces
{
    public interface IWarehouseRepository
    {
        Task<IEnumerable<Warehouse>> GetWarehousesAsync();

        Task<Warehouse> GetWarehouseByIdAsync(Guid id);

        Task<Warehouse> GetWarehouseByNameAsync(string name);

        Task InsertWarehouseAsync(Warehouse warehouse);

        Task<bool> UpdateWarehouseAsync(Warehouse warehouse);

        Task<bool> DeleteWarehouseAsync(Guid id);
    }
}