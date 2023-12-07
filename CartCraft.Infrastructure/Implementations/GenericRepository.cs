using CartCraft.Domain.Models;
using CartCraft.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CartCraft.Infrastructure.Implementations
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly DbSet<T> _dbSet;

        public GenericRepository(DbContext dbContext)
        {
            _dbSet = dbContext.Set<T>();
        }

        public async Task AddAsync(T entity)
        {
            entity.Id = Guid.NewGuid().ToString();
            entity.CreatedAt = DateTime.Now;
            entity.UpdatedAt = DateTime.Now;

            await _dbSet.AddAsync(entity);
        }

        public IQueryable<T> GetAll(Expression<Func<T, bool>> filter = null, List<string> includes = null)
        {
            IQueryable<T> query = _dbSet;

            if (includes != null)
            {
                foreach (var include in includes) 
                    query.Include(include);
            }

            if (filter != null)
                return query.Where(filter);
            return query;
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> filter, List<string> includes = null)
        {
            IQueryable<T> query = _dbSet;

            if (includes != null)
            {
                foreach (var include in includes)
                    query.Include(include);
            }
            return await query.Where(filter).FirstOrDefaultAsync();
        }

        public void Update(T entity)
        {
            entity.UpdatedAt = DateTime.Now;
            _dbSet.Update(entity);
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }
    }
}
