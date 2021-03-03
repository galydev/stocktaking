using Inventory.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inventory.Domain.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        Task<IEnumerable<TEntity>> GetAllAsync();

        Task<TEntity> GetByIdAsync(Guid id);

        Task<bool> ExistsAsync(Guid id);

        Task InsertAsync(TEntity entity);

        void UpdateAsync(TEntity entity);

        Task DeleteAsync(Guid id);

        IQueryable<TEntity> Queryable();
    }
}