using KUSYS.Domain.Entities.Common;
using System.Linq.Expressions;

namespace KUSYS.Application.Abstracts
{
    /// <summary>
    /// generic repository interface metodları tanımlandı
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        Task<int> AddAsync(TEntity entity);
        Task<int> UpdateAsync(TEntity entity);

        Task<int> DeleteAsync(long id);
        Task<int> DeleteAsync(TEntity entity);
        IQueryable<TEntity> AsQueryable();

        Task<List<TEntity>> GetAll();

        Task<TEntity> GetByIdAsync(long id, params Expression<Func<TEntity, object>>[] includes);

        Task<TEntity> GetSingleAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes);

    }
}
