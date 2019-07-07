using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UberHack.API.Entities;

namespace UberHack.API.Contracts
{
    public interface IBaseRepository<TEntity> where TEntity : Entidade
    {
        TEntity Get(int Id);
        IEnumerable<TEntity> GetAll();
        TEntity Update(TEntity entity);
        TEntity Insert(TEntity entity);
        int InsertRange(IEnumerable<TEntity> entities);
        void Delete(TEntity entity);
        IQueryable<TEntity> GetQueryable();
        int UpdateRange(IEnumerable<TEntity> entities);
    }
}
