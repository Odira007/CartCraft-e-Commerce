using CartCraft.Domain.Models;
using CartCraft.Infrastructure.DbContexts;
using CartCraft.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CartCraft.Infrastructure.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CartCraftDbContext _context;

        public UnitOfWork(CartCraftDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task CommitAsync()//CancellationToken cancellationToken = default)
        {
            //var updatedConcurrencyAwareEntries = _context.ChangeTracker.Entries()
            //    .Where(x => x.State == EntityState.Modified).OfType<BaseEntity>();

            //foreach (var entry in updatedConcurrencyAwareEntries)
            //    entry.ConcurrencyStamp = Guid.NewGuid().ToString();

            await _context.SaveChangesAsync();
        }

        public IGenericRepository<T> Repository<T>() where T : BaseEntity
        {
            return new GenericRepository<T>(_context);
        }
    }
}
