using Inventory.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Inventory.Domain.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetProductsAsync();

        Task<Product> GetProductByIdAsync(Guid id);

        Task<Product> GetProductByNameAsync(string name);

        Task InsertProductAsync(Product product);

        Task<bool> UpdateProductAsync(Product product);

        Task<bool> DeleteProductAsync(Guid id);
    }
}