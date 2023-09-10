using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Task.Core.Interfaces;
using Task.Core.Reposatories;

namespace Greenz.Data
{
    public class UnityOfWork<TContext> : IUnityOfWork , IUnityOfWork<TContext> where TContext : DbContext
    {
        private Dictionary<Type, object> _repositories;

        public TContext Context { get; }

        public UnityOfWork(TContext context)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
        }


        public IBaseRepository<TEntity> GetRepository<TEntity>() where TEntity : class
        {
            if (_repositories == null) _repositories = new Dictionary<Type, object>();

            var type = typeof(TEntity);
            if (!_repositories.ContainsKey(type)) _repositories[type] = new BaseReposatory<TEntity>(Context);
            return (IBaseRepository<TEntity>)_repositories[type];
        }

        public int Complete()
        {
            return Context.SaveChanges();
        }

        public async Task<int> CompleteAsync()
        {
            return await Context.SaveChangesAsync();
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}
