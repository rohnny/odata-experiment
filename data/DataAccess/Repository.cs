using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NLog;

namespace data.DataAccess
{
    public class Repository<TEntity> : IRepository<TEntity>
        where TEntity : class
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();
        private readonly AppDbContext _dbContext;
        private readonly DbSet<TEntity> _dbSet;

        public Repository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<TEntity>();
        }

        public Task<List<TEntity>> GetAsync(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            return Get(filter, orderBy, includeProperties).ToListAsync();
        }

        public IQueryable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<TEntity> query = _dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split(
                         new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query);
            }

            return query;
        }

        public async Task<TEntity> GetByIdAsync(object id)
        {
            var entity = await _dbSet.FindAsync(id);

            if (entity == null)
            {
                Logger.Debug($"{typeof(TEntity).Name} with id {id} not found");
            }

            return entity;
        }

        public void Insert(TEntity entity)
        {
            _dbSet.Add(entity);
        }

        public void Delete(TEntity entity)
        {
            if (_dbContext.Entry(entity).State == EntityState.Detached)
            {
                _dbSet.Attach(entity);
            }

            _dbSet.Remove(entity);
        }

        public void Delete(object id)
        {
            var entity = _dbSet.Find(id);
            _dbSet.Remove(entity);
        }

        public void Update(object id, TEntity entity)
        {
            var entityInDb = _dbSet.Find(id);
            _dbContext.Entry(entityInDb).CurrentValues.SetValues(entity);

            // Ensure properties marked with [Editable(false)] do not get saved back on update.
            foreach (var item in typeof(TEntity).GetProperties()
                         .Where(p => p.GetCustomAttributes<EditableAttribute>().Any(e => !e.AllowEdit)))
            {
                _dbContext.Entry(entityInDb).Property(item.Name).IsModified = false;
            }
        }
    }
}
