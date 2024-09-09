using Microsoft.EntityFrameworkCore;
using Biblioteca.Entities;
using Biblioteca.Persistence;
using System.Linq.Expressions;

namespace Biblioteca.Repositories
{
    public abstract class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : EntityBase
    {
        protected readonly DbContext context;

        public RepositoryBase(DbContext context)
        {
            this.context = context;
        }
        public virtual async Task<ICollection<TEntity>> GetAsync()
        {
            return await context.Set<TEntity>()
                .AsNoTracking()
                .ToListAsync();
        }
        public async Task<ICollection<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await context.Set<TEntity>()
                .Where(predicate)
                .AsNoTracking()
                .ToListAsync();
        }
        public async Task<ICollection<TEntity>> GetAsync<TKey>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TKey>> orderBy)
        {
            return await context.Set<TEntity>()
                .Where(predicate)
                .OrderBy(orderBy)
                .AsNoTracking()
                .ToListAsync();
        }
        public virtual async Task<TEntity?> GetAsync(int id)
        {
            return await context.Set<TEntity>()
                //.AsNoTracking()
                //.FirstOrDefaultAsync(x => x.Id == id);
                .FindAsync(id);
        }
        public virtual async Task<int> AddAsync(TEntity entity)
        {
            await context.Set<TEntity>()
                .AddAsync(entity);
            await context.SaveChangesAsync();
            return entity.Id;
        }
        public virtual async Task UpdateAsync()
        {
            await context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var item = await GetAsync(id);
            if(item is not null)
            {
                item.Estado = false;
                await UpdateAsync();
            }
        }
    }
}
