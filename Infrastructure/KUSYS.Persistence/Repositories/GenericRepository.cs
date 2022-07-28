using KUSYS.Application.Abstracts;
using KUSYS.Domain.Entities.Common;
using KUSYS.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace KUSYS.Persistence.Repositories
{
    /// <summary>
    /// IGenericRepository implementasyonu ile oluşan metodlar concrete ediliyor.
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly KUSYSContext dbContext;


        protected DbSet<TEntity> entity => dbContext.Set<TEntity>();

        public GenericRepository(KUSYSContext dbContext)
        {
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        #region Insert Methods

        public virtual async Task<int> AddAsync(TEntity entity)
        {
            await this.entity.AddAsync(entity);
            return await dbContext.SaveChangesAsync();
        }

        #endregion

        #region Update Methods

        public virtual async Task<int> UpdateAsync(TEntity entity)
        {
            this.entity.Attach(entity);
            dbContext.Entry(entity).State = EntityState.Modified;

            return await dbContext.SaveChangesAsync();
        }

        #endregion

        #region Delete Methods

        public virtual Task<int> DeleteAsync(TEntity entity)
        {
            if (dbContext.Entry(entity).State == EntityState.Detached)
            {
                this.entity.Attach(entity);
            }

            this.entity.Remove(entity);

            return dbContext.SaveChangesAsync();
        }

        public virtual Task<int> DeleteAsync(long id)
        {
            var entity = this.entity.Find(id);
            return DeleteAsync(entity);
        }

        #endregion


        #region Get Methods

        public virtual IQueryable<TEntity> AsQueryable() => entity.AsQueryable();
        public virtual async Task<List<TEntity>> GetAll()
        {
            return await entity.ToListAsync();
        }
        public virtual async Task<TEntity> GetSingleAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = entity;

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            query = ApplyIncludes(query, includes);

            return await query.SingleOrDefaultAsync();

        }
        public virtual async Task<TEntity> GetByIdAsync(long id, params Expression<Func<TEntity, object>>[] includes)
        {
            TEntity found = await entity.FindAsync(id);

            if (found == null)
                return null;

            foreach (Expression<Func<TEntity, object>> include in includes)
            {
                dbContext.Entry(found).Reference(include).Load();
            }

            return found;
        }

        #endregion
        #region SaveChanges Methods

        public Task<int> SaveChangesAsync()
        {
            return dbContext.SaveChangesAsync();
        }

        public int SaveChanges()
        {
            return dbContext.SaveChanges();
        }

        #endregion


        private static IQueryable<TEntity> ApplyIncludes(IQueryable<TEntity> query, params Expression<Func<TEntity, object>>[] includes)
        {
            if (includes != null)
            {
                foreach (var includeItem in includes)
                {
                    query = query.Include(includeItem);
                }
            }

            return query;
        }
    }
}
