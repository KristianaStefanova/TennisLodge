using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TennisLodge.Data.Repository.Interfaces
{
    public interface IRepository<TEntity, TKey>
    {
        TEntity? GetById(TKey id);

        TEntity? SinglesOrDefault(Func<TEntity, bool> predicate);

        TEntity? FirstOrDefault(Func<TEntity, bool> predicate);

        IEnumerable<TEntity> GetAll();

        int Count();

        IQueryable<TEntity> GetAllAttached();

        void Add(TEntity entity);

        void AddRange(IEnumerable<TEntity> entities); 

        bool Delete(TEntity entity);

        bool Update (TEntity entity); 
        
        void SaveChanges();
    }
    
}
