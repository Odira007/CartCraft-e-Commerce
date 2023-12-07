using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CartCraft.Infrastructure.Interfaces
{
    public interface IGenericRepository<T>
    {
        Task AddAsync(T entity);
        IQueryable<T> GetAll(Expression<Func<T, bool>> filter = null, List<string> includes = null);
        Task<T> GetAsync(Expression<Func<T, bool>> filter, List<string> includes = null);
        void Update(T entity);
        void Delete(T entity);
    }
}
