using Inventory.Domain.Entities;
using Inventory.Domain.Interfaces;
using Inventory.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inventory.Infrastructure.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        protected InventoryContext _context;
        protected DbSet<TEntity> _entities;

        public GenericRepository(InventoryContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _entities = context.Set<TEntity>();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
            => await _entities.ToListAsync();

        public async Task<TEntity> GetByIdAsync(Guid id)
            => await _entities.FindAsync(id);

        public async Task<bool> ExistsAsync(Guid id)
            => await _entities.AnyAsync(e => e.Id == id);

        public async Task InsertAsync(TEntity entity)
            => await _entities.AddAsync(entity);

        public void UpdateAsync(TEntity entity)
            => _context.Entry(entity).State = EntityState.Modified;

        public async Task DeleteAsync(Guid id)
        {
            TEntity currentEntity = await GetByIdAsync(id);
            _entities.Remove(currentEntity);
        }

        public virtual IQueryable<TEntity> Queryable() => _entities;
    }
}