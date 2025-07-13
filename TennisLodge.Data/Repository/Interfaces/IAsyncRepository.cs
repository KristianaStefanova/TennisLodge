using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TennisLodge.Data.Repository.Interfaces
{
    public interface IAsyncRepository<TEntity, TKey>
    {
        ValueTask<TEntity?> GetByIdAsync(TKey id);

        Task<TEntity?> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);

        Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);

        Task<int> CountAsync();

        Task<IEnumerable<TEntity>> GetAllAsync();

        Task AddAsync(TEntity entity);

        Task AddRangeAsync(IEnumerable<TEntity> entities);

        Task<bool> DeleteAsync(TEntity entity);

        Task<bool> UpdateAsync(TEntity entity);

        Task SaveChangesAsync();
    }
}
