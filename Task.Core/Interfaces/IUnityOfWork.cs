using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Task.Core.Interfaces
{
    public interface IUnityOfWork : IDisposable
    {
        IBaseRepository<TEntity> GetRepository<TEntity>() where TEntity : class;

        int Complete();
        Task<int> CompleteAsync();
    }

    public interface IUnityOfWork<TContext> : IUnityOfWork where TContext : DbContext
    {
        TContext Context { get; }
    }
}
