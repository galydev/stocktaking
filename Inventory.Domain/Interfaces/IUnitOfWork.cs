using Inventory.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Inventory.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Product> ProductRepository { get; }
        IGenericRepository<Warehouse> WarehoueseRepository { get; }
        IGenericRepository<Movement> MovementRepository { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}