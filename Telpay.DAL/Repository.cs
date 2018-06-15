using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using Telpay.Business;

namespace Telpay.DAL
{
    public class Repository<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : class, IDbEntity<TKey> where TKey : struct
    {
        protected readonly BaseDbContext _dbContext;

        public Repository(BaseDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public TEntity Get(TKey id)
        {
            return Filter(((TEntity x) => x.Id), id).FirstOrDefault();
        }

        public IList<TEntity> GetAll()
        {
            return _dbContext.Set<IDbEntity<TKey>>()
                .Cast<TEntity>()
                .ToList();
        }

        public void Save(TEntity entity)
        {
            _dbContext.Entry<IDbEntity<TKey>>(entity).State = EntityState.Added;
        }

        public void Update(TEntity entity)
        {
            _dbContext.Entry<IDbEntity<TKey>>(entity).State = EntityState.Modified;
        }


        public void Delete(TEntity entity)
        {
            _dbContext.Entry<IDbEntity<TKey>>(entity).State = EntityState.Deleted;
        }

        public void Delete(TKey id)
        {
            TEntity entity = Get(id);
            if (entity != null)
            {
                Delete(entity);
            }
        }

        public void Commit()
        {
            _dbContext.SaveChangesAsync();
        }

        public async Task CommitAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        private IQueryable<TEntity> Filter<T, U>(Expression<Func<T, U>> property, TKey value)
        {
            IQueryable<TEntity> dbSet = _dbContext.Set<TEntity>();
            Expression equalExpression = BuildEqualExpression(property, value);
            var lambda = Expression.Lambda<Func<TEntity, bool>>(equalExpression, new ParameterExpression[] { property.Parameters.Single() });

            return dbSet.Where(lambda);
        }

        private Expression BuildEqualExpression<T, U>(Expression<Func<T, U>> property, TKey value)
        {
            var memberExpression = property.Body as MemberExpression;
            if (memberExpression == null || !(memberExpression.Member is PropertyInfo))
            {
                throw new ArgumentException("Property expected", "property");
            }

            Expression left = property.Body;
            Expression right = Expression.Constant(value, typeof(TKey));

            return Expression.Equal(left, right);
        }
    }
}
