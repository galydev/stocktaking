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
    public class MovementService : IMovementService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<MovementService> _logger;

        public MovementService(IUnitOfWork unitOfWork, ILogger<MovementService> logger)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<Movement> InsertProductWareahouse(Movement movement)
        {
            var product = await _unitOfWork.ProductRepository.GetByIdAsync(movement.ProductId);

            var currentTotalStock = TotalStock();
            var futureStock = FutureValue(currentTotalStock, movement);
            if (futureStock > 10000)
            {
                _logger.LogInformation("No more products can be loaded, as they exceed the established threshold. For logistical reasons there is no place to store them.");
                throw new BusinessException("No more products can be loaded, as they exceed the established threshold. For logistical reasons there is no place to store them.");
            }

            var currentStockProductValue = CurrentStockProductValue(product.Id);
            var futureStockProductValue = FutureValue(currentStockProductValue, movement);
            if (product.MaximumStock > futureStockProductValue)
            {
                movement.Id = Guid.NewGuid();
                movement.MovementDate = DateTime.UtcNow;
                await _unitOfWork.MovementRepository.InsertAsync(movement);
                int result = await _unitOfWork.SaveChangesAsync();
                _logger.LogInformation("movement has been created in warehouse.");

                if (result > 0)
                {
                    product.Stock = futureStockProductValue;
                    _unitOfWork.ProductRepository.UpdateAsync(product);
                    await _unitOfWork.SaveChangesAsync();
                    _logger.LogInformation("product stock has been updated.");
                }
            }
            else
            {
                _logger.LogInformation($"Only { product.MaximumStock } units of the selected product can be loaded.");
               throw new BusinessException($"Only { product.MaximumStock } units of the selected product can be loaded.");
            }

            return movement;
        }

        public async Task<bool> RemoveProductsOfWarehouse(RemoveProductWarehouse productWarehouse)
        {
            var residuaryQuantity = _unitOfWork.MovementRepository.Queryable()
                                        .Where(f => f.ProductId.Equals(productWarehouse.ProductId) && f.WarehouseId.Equals(productWarehouse.WarehouseId))
                                        .Sum(x => x.Type ? x.Quantity : -x.Quantity);

            if (residuaryQuantity > productWarehouse.Quantity)
            {
                Movement movement = new Movement
                {
                    Type = false,
                    Quantity = productWarehouse.Quantity,
                    ProductId = productWarehouse.ProductId,
                    WarehouseId = productWarehouse.WarehouseId
                };
                await InsertProductWareahouse(movement);
                _logger.LogInformation("product has been entered into warehouse.");
                return true;
            }
            _logger.LogInformation($"It is not possible to remove, the warehouse only contains { residuaryQuantity } of this product");
            throw new BusinessException($"It is not possible to remove, the warehouse only contains { residuaryQuantity } of this product");
        }

        public async Task<IEnumerable<Movement>> MoveProductOtherWarehouse(MoveProducts moveProducts)
        {
            List<Movement> movements = new List<Movement>();

            var productsWarehouse = _unitOfWork.MovementRepository
                                                .Queryable()
                                                .Where(f => f.ProductId.Equals(moveProducts.ProductId) && f.WarehouseId.Equals(moveProducts.CurrentWarehouseId))
                                                .ToList();

            if (productsWarehouse.Count > 0)
            {
                var residuaryQuantity = productsWarehouse.Sum(x => x.Type ? x.Quantity : -x.Quantity);
                if (residuaryQuantity > moveProducts.Quantity)
                {
                    movements.Add(await InsertProductWareahouse(new Movement
                    {
                        Type = true,
                        ProductId = moveProducts.ProductId,
                        Price = moveProducts.Price,
                        WarehouseId = moveProducts.NewWarehouseId,
                        Quantity = moveProducts.Quantity
                    }));

                    movements.Add(await InsertProductWareahouse(new Movement
                    {
                        Type = false,
                        ProductId = moveProducts.ProductId,
                        Price = moveProducts.Price,
                        WarehouseId = moveProducts.CurrentWarehouseId,
                        Quantity = moveProducts.Quantity
                    }));

                    return movements;
                }
            }
            _logger.LogInformation("product not found in the selected warehouse");
            throw new BusinessException("product not found in the selected warehouse");
        }

        public async Task<ShoppingProduct> GetTotalShoppingProduct(Guid productId)
        {
            var movementsProduct = _unitOfWork.MovementRepository
                                                .Queryable()
                                                .Where(f => f.ProductId.Equals(productId) && !f.Type)
                                                .ToList();
            if (movementsProduct.Count > 0)
            {
                var product = await _unitOfWork.ProductRepository.GetByIdAsync(productId);

                var shoppingProduct = new ShoppingProduct
                {
                    Id = productId,
                    Name = product.Name,
                    Stock = product.Stock,
                    ShoppingQuantity = movementsProduct.Sum(x => x.Quantity),
                    TotalPriceShopping = movementsProduct.Sum(x => x.Quantity * x.Price)
                };
                return shoppingProduct;
            }
            _logger.LogInformation("The product is out of stock and purchases");
            throw new BusinessException("The product is out of stock and purchases");
        }

        #region Private Methods

        private int CurrentStockProductValue(Guid productId)
            => _unitOfWork.MovementRepository.Queryable().Where(f => f.ProductId.Equals(productId)).Sum(x => x.Type ? x.Quantity : -x.Quantity);

        private int TotalStock()
             => _unitOfWork.MovementRepository.Queryable().Sum(x => x.Type ? x.Quantity : -x.Quantity);

        private int CurrentStockWarehouseValue(Guid warehouseId)
           => _unitOfWork.MovementRepository.Queryable().Where(f => f.WarehouseId.Equals(warehouseId)).Sum(x => x.Type ? x.Quantity : -x.Quantity);

        private int FutureValue(int currentValue, Movement movement)
            => currentValue + (movement.Type ? movement.Quantity : -movement.Quantity);

        #endregion Private Methods
    }
}