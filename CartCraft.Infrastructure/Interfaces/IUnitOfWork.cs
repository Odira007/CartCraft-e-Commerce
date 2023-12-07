using CartCraft.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CartCraft.Infrastructure.Interfaces
{
    public interface IUnitOfWork
    {
        IGenericRepository<T> Repository<T>() where T : BaseEntity;
        Task CommitAsync();//CancellationToken cancellationToken);
    }
}
