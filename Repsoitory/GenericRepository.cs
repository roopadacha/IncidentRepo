using Microsoft.EntityFrameworkCore;
using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly IncidentDbContext incidentDbContext;
        private DbSet<TEntity> DbEntity;

        public GenericRepository(IncidentDbContext incidentDbContext)
        {
            this.incidentDbContext = incidentDbContext;
            this.DbEntity = incidentDbContext.Set<TEntity>();
        }
        public async Task CreateAsync(TEntity entity)
        {
            await this.DbEntity.AddAsync(entity);
            await incidentDbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(TEntity entity)
        {
            this.DbEntity.Remove(entity);
            await this.incidentDbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await this.DbEntity.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAllWithChildAsync(params Expression<Func<TEntity, object>>[] includes)
        {
            var query = getEntityWithIncludes(includes);
            return await query.AsNoTracking().ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await this.DbEntity
                        .AsNoTracking()
                        .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<TEntity> GetByIdWithChildAsync(int id, params Expression<Func<TEntity, object>>[] includes)
        {
            var query = getEntityWithIncludes(includes);

            return await query
                        .AsNoTracking()
                        .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task UpdateAsync(TEntity entity)
        {
            this.incidentDbContext.Entry(entity).State = EntityState.Modified;
            this.DbEntity.Update(entity);
            var updatedCount = await incidentDbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    incidentDbContext.Dispose();
                }
            }
            this.disposed = true;
        }

        private IQueryable<TEntity> getEntityWithIncludes(params Expression<Func<TEntity, object>>[] includes)
        {
            var query = this.DbEntity.AsQueryable();
            if (includes != null && includes.Any())
            {
                foreach (Expression<Func<TEntity, object>> includeProperty in includes)
                {
                    query = query.Include<TEntity, object>(includeProperty);
                }
            }

            return query;
        }
    }
}
