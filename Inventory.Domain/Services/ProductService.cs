using Inventory.Domain.CustomEntities;
using Inventory.Domain.Entities;
using Inventory.Domain.Exceptions;
using Inventory.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inventory.Domain.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<ProductService> _logger;

        public ProductService(IUnitOfWork unitOfWork, ILogger<ProductService> logger)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            var products = await _unitOfWork.ProductRepository.GetAllAsync();
            if (products == null)
            {
                _logger.LogInformation("No products records.");
                throw new BusinessException("No products records");
            }
            return products;
        }

        public async Task<Product> GetProductByIdAsync(Guid id)
        {
            var product = await _unitOfWork.ProductRepository.GetByIdAsync(id);
            if (product == null)
            {
                _logger.LogInformation("Product doesn't exist.");
                throw new BusinessException("Product doesn't exist");
            }
            return product;
        }

        public Task<Product> GetProductByNameAsync(string name)
            => Task.FromResult(_unitOfWork.ProductRepository.Queryable().FirstOrDefault(x => x.Name.Equals(name)));

        public async Task<bool> ExistProductAsync(Guid id)
        {
            var existProduct = await _unitOfWork.ProductRepository.ExistsAsync(id);
            if (!existProduct)
            {
                _logger.LogInformation("Product doesn't exist.");
                throw new BusinessException("Product doesn't exist");
            }
            return true;
        }

        public async Task<Product> InsertProductAsync(CreateProduct product)
        {
            var currentProduct = await GetProductByNameAsync(product.Name);
            if (currentProduct != null)
            {
                _logger.LogInformation("There is already a product with this name.");
                throw new BusinessException("There is already a product with this name.");
            }

            var newProduct = new Product
            {
                CreationDate = DateTime.Now,
                Name = product.Name,
                Description = product.Description,
                Sku = product.Sku,
                Price = product.Price,
                MinimunStock = product.MinimunStock,
                MaximumStock = product.MaximumStock,
                Stock = product.Stock
            };

            await _unitOfWork.ProductRepository.InsertAsync(newProduct);
            await _unitOfWork.SaveChangesAsync();
            _logger.LogInformation("product has been created.");
            return newProduct;
        }

        public async Task<bool> UpdateProductAsync(Product product)
        {
            await ExistProductAsync(product.Id);
            product.CreationDate = DateTime.Now;
            _unitOfWork.ProductRepository.UpdateAsync(product);
            _logger.LogInformation("product has been update.");
            return await _unitOfWork.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            await ExistProductAsync(id);
            await _unitOfWork.ProductRepository.DeleteAsync(id);
            _logger.LogInformation("product has been delete.");
            return await _unitOfWork.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateStockProduct(Guid id, int stockValue)
        {
            await ExistProductAsync(id);
            var currentProduct = await GetProductByIdAsync(id);
            currentProduct.Stock = stockValue;
            await UpdateProductAsync(currentProduct);
            int result = await _unitOfWork.SaveChangesAsync();
            _logger.LogInformation("product has been update stock value.");
            return result > 0;
        }
    }
}