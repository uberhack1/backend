using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UberHack.API.Contracts
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity> UpdateAsync(TEntity entity);
        TEntity Update(TEntity entity);
        Task<TEntity> InsertAsync(TEntity entity);
        TEntity Insert(TEntity entity);
        int InsertRange(IEnumerable<TEntity> entities);
        void Delete(TEntity entity);
        IQueryable<TEntity> GetQueryable();
        int UpdateRange(IEnumerable<TEntity> entities);
    }
}
