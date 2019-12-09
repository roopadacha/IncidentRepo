using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{

    public interface IGenericRepository<TEntity> : IDisposable
                    where TEntity : class 
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<IEnumerable<TEntity>> GetAllWithChildAsync(params Expression<Func<TEntity, object>>[] includes);
        Task<TEntity> GetByIdAsync(int id);
        Task<TEntity> GetByIdWithChildAsync(int id, params Expression<Func<TEntity, object>>[] includes);
        Task CreateAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(TEntity entity);
    }
}

