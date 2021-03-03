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
    public class WarehouseService : IWarehouseService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<WarehouseService> _logger;

        public WarehouseService(IUnitOfWork unitOfWork, ILogger<WarehouseService> logger)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<IEnumerable<Warehouse>> GetWarehousesAsync()
        {
            var warehouses = await _unitOfWork.WarehoueseRepository.GetAllAsync();
            if (warehouses == null)
            {
                _logger.LogInformation("No warehouse records.");
                throw new BusinessException("No warehouse records");
            }
            return warehouses;
        }

        public async Task<bool> ExistWarehouseAsync(Guid id)
        {
            var existWarehouse = await _unitOfWork.WarehoueseRepository.ExistsAsync(id);
            if (!existWarehouse)
            {
                _logger.LogInformation("Warehouse doesn't exist.");
                throw new BusinessException("Warehouse doesn't exist");
            }
            return true;
        }

        public async Task<Warehouse> CreateWarehousesAsync(Warehouse warehouse)
        {
            var currentProduct = await GetWarehouseByNameAsync(warehouse.Name);
            if (currentProduct != null)
            {
                _logger.LogInformation("There is already a warehouse with this name.");
                throw new BusinessException("There is already a warehouse with this name.");
            }

            warehouse.CreationDate = DateTime.Now;
            await _unitOfWork.WarehoueseRepository.InsertAsync(warehouse);
            await _unitOfWork.SaveChangesAsync();
            _logger.LogInformation("product has been created.");
            return warehouse;
        }

        public Task<Warehouse> GetWarehouseByNameAsync(int name)
            => Task.FromResult(_unitOfWork.WarehoueseRepository.Queryable().FirstOrDefault(x => x.Name.Equals(name)));
    }
}