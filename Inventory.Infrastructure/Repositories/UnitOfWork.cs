using Inventory.Domain.Entities;
using Inventory.Domain.Interfaces;
using Inventory.Infrastructure.Data;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Inventory.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly InventoryContext _context;
        private readonly IGenericRepository<Product> _productRepository;
        private readonly IGenericRepository<Warehouse> _warehouseRepository;
        private readonly IGenericRepository<Movement> _movementRepository;

        public UnitOfWork(InventoryContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IGenericRepository<Product> ProductRepository => _productRepository ?? new GenericRepository<Product>(_context);
        public IGenericRepository<Warehouse> WarehoueseRepository => _warehouseRepository ?? new GenericRepository<Warehouse>(_context);
        public IGenericRepository<Movement> MovementRepository => _movementRepository ?? new GenericRepository<Movement>(_context);

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            // Cleanup
            if (_context != null)
            {
                _context.Dispose();
            }
        }

        ~UnitOfWork()
        {
            Dispose(false);
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
            => await _context.SaveChangesAsync();
    }
}