using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace data.DataAccess
{
    public interface IRepository<TEntity>
        where TEntity : class
    {
        Task<List<TEntity>> GetAsync(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "");

        IQueryable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "");

        Task<TEntity> GetByIdAsync(object id);

        void Insert(TEntity entity);

        void Delete(TEntity entity);
        void Delete(object id);
        void Update(object id, TEntity entity);
    }
}
