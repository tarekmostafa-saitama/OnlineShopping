using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace OnlineShopping.Core.Repositories
{
    public interface IRepository<TEntity, TKey> where TEntity : class
    {
        void Add(TEntity entity);
        void AddRange(ICollection<TEntity> entities);
        void Delete(TEntity entity);
        void DeleteRange(ICollection<TEntity> entities);
        TEntity Get(TKey id, string[] navigation);
        IEnumerable<TEntity> GetAll(string[] navigation);
        
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate, string[] navigation);
        int Count(Expression<Func<TEntity, bool>> predicate);
    }
}
