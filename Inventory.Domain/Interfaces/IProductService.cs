using Inventory.Domain.CustomEntities;
using Inventory.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Inventory.Domain.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetProductsAsync();

        Task<Product> GetProductByIdAsync(Guid id);

        Task<Product> GetProductByNameAsync(string name);

        Task<bool> ExistProductAsync(Guid id);

        Task<Product> InsertProductAsync(CreateProduct product);

        Task<bool> UpdateProductAsync(Product product);

        Task<bool> DeleteAsync(Guid id);

        Task<bool> UpdateStockProduct(Guid id, int stockValue);
    }
}