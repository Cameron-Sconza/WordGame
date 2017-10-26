using Repository.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class BaseRepository<TEntity> : IDisposable where TEntity : class
    {
        protected RepositoryContext Context;
        protected DbSet<TEntity> DbSet;

        public BaseRepository(RepositoryContext context)
        {
            Context = context;
            DbSet = Context.Set<TEntity>();
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            return DbSet.ToList();
        }

        public virtual TEntity GetByID(int id)
        {
            return DbSet.Find(id);
        }

        public virtual void Add(TEntity entity)
        {
            if (entity != null)
            {
                DbSet.Add(entity);
            }
        }
        public virtual void AddBulk(List<TEntity> entity)
        {
            if (entity != null)
            {
                DbSet.AddRange(entity);
            }
        }
        public virtual void Update(TEntity entity)
        {
            if (entity != null)
            {
                Context.Set<TEntity>().AddOrUpdate(entity);
            }
        }

        public virtual void Delete(int id)
        {
            TEntity entity = DbSet.Find(id);
            DbSet.Remove(entity);
        }

        public void Save()
        {
            Context.SaveChanges();
        }

        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (Context != null)
                {
                    Context.Dispose();
                    Context = null;
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
    }
}
