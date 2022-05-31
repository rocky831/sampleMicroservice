using AVEVA.PA.DataAccess;
using AVEVA.PA.Exceptions;
using AVEVA.PA.MicroserviceTemplate.Core.IRepositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AVEVA.PA.MicroserviceTemplate.Infrastructure.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        protected PaDbContext Context;
        protected DbSet<TEntity> DbSet;

        public GenericRepository(PaDbContext context)
        {
            this.Context = context;
            this.DbSet = this.Context.Set<TEntity>();
        }
        public virtual async Task<TEntity> GetByIdAsync(int id)
        {
            var entity= await this.DbSet.FindAsync(id);
            if (entity == null)
            {
                throw new NotFoundException(id);
            }
            return entity;
        }

        public virtual async Task<IReadOnlyCollection<TEntity>> GetAllAsync()
        {
            return await this.DbSet.AsNoTracking().ToListAsync();
        }

        public virtual async Task<TEntity> AddAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException($"{nameof(entity)} entity must not be null");
            }
            await this.DbSet.AddAsync(entity);
            return entity;
        }

        public virtual async Task<TEntity> UpdateAsync(TEntity entity, object key)
        {
            var exist = await this.Context.Set<TEntity>().FindAsync(key);

            if (exist == null)
                throw new NotFoundException(Convert.ToInt32(key));
            this.Context.Entry(exist).CurrentValues.SetValues(entity);
            return exist;
        }

        public virtual async Task<bool> DeleteAsync(int id)
        {
            var entityToDelete = await this.DbSet.FindAsync(id);

            if (entityToDelete == null)
            {
                throw new NotFoundException(Convert.ToInt32(id));
            }
            await this.Delete(entityToDelete);
            return true;
        }

        private Task Delete(TEntity entityToDelete)
        {
            if (this.Context.Entry(entityToDelete).State == EntityState.Detached)
            {
                this.DbSet.Attach(entityToDelete);
            }
            
            DbSet.Remove(entityToDelete);
            return Task.CompletedTask;
        }
        public virtual async Task<IReadOnlyCollection<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await this.DbSet.AsNoTracking().Where(predicate).ToListAsync();
        }
    }
}
