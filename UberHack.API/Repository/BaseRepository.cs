using Microsoft.EntityFrameworkCore;
using Npgsql;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UberHack.API.Contracts;
using UberHack.API.Entities;

namespace UberHack.API.Repository
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity>
            where TEntity : Entidade
    {
        protected readonly DbContext _context;

        protected BaseRepository(DbContext context)
        {
            _context = context;
        }

        public TEntity Get(int Id)
        {
            return GetQueryable().Where(o => o.Id == Id).FirstOrDefault();
        }

        public void Delete(TEntity entity)
        {
            _context.Remove(entity);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _context.Set<TEntity>().AsNoTracking().ToList();
        }

        public IQueryable<TEntity> GetQueryable()
        {
            return _context.Set<TEntity>().AsNoTracking();
        }

        public TEntity Insert(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
            _context.SaveChanges();

            return entity;
        }

        public int InsertRange(IEnumerable<TEntity> entities)
        {
            _context.Set<TEntity>().AddRange(entities);
            return _context.SaveChanges();
        }

        public TEntity Update(TEntity entity)
        {
            _context.Update<TEntity>(entity);

            _context.SaveChanges();

            return entity;
        }

        public int UpdateRange(IEnumerable<TEntity> entities)
        {
            _context.UpdateRange(entities);

            return _context.SaveChanges();
        }

        public System.Data.IDbConnection GetDbConnection()
        {
            System.Data.IDbConnection dbConnection;

            dbConnection = new NpgsqlConnection("User ID=hctvaqyhesrgdr;Password=d35ab130a5d647084cd42b9d2f4317514adc39cb03855e0c00601e95b065d7e8;Host=ec2-23-21-160-38.compute-1.amazonaws.com;Port=5432;Database=d53d9j5genek5a;");

            dbConnection.Open();

            return dbConnection;
        }
    }

}
