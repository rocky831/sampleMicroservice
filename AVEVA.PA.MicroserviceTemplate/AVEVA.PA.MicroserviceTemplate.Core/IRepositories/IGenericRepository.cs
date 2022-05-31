using System.Linq.Expressions;

namespace AVEVA.PA.MicroserviceTemplate.Core.IRepositories
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task<IReadOnlyCollection<TEntity>> GetAllAsync();
        Task<TEntity> GetByIdAsync(int id);
        Task<TEntity> AddAsync(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity, object key);
        Task<bool> DeleteAsync(int id);
        Task<IReadOnlyCollection<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);
    }
}
