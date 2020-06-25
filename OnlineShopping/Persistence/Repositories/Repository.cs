using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OnlineShopping.Core.Repositories;

namespace OnlineShopping.Persistence.Repositories
{
    public class Repository<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : class
    {
        private readonly DbContext _context;

        public Repository(DbContext context)
        {
            _context = context;
        }

        public void Add(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
        }

        public void AddRange(ICollection<TEntity> entities)
        {
            _context.Set<TEntity>().AddRange(entities);
        }

        public void Delete(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
        }

        public void DeleteRange(ICollection<TEntity> entities)
        {
            _context.Set<TEntity>().RemoveRange(entities);
        }

        public TEntity Get(TKey id, string[] navigation)
        {
            var query = _context.Set<TEntity>();
            foreach (var i in navigation)
            {
                query.Include(i);
            }

            return query.Find(id);
        }

        public IEnumerable<TEntity> GetAll(string[] navigation)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();
            foreach (var i in navigation)
            {
                query = query.Include(i);
            }

            return query;
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate, string[] navigation)
        {

            IQueryable<TEntity> query = _context.Set<TEntity>();
            foreach (var i in navigation)
            {
                query = query.Include(i);
            }

            return query.Where(predicate);
        }

        public int Count(Expression<Func<TEntity, bool>> predicate)
        {
            int count;
            count = predicate == null ? _context.Set<TEntity>().Count() : _context.Set<TEntity>().Count(predicate);
            return count;
        }
    }
}
