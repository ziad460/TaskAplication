using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Task.Core.Interfaces;

namespace Task.Core.Reposatories
{
    public class BaseReposatory<T> : IBaseRepository<T> where T : class
    {
        private readonly DbContext context;

        public BaseReposatory(DbContext _context)
        {
            context = _context;
        }
        public void AddOne(T entity)
        {
            context.Set<T>().Add(entity);
        }

        public T Find(Expression<Func<T, bool>> criteria, string[] includes = null)
        {
            IQueryable<T> query = context.Set<T>();
            if (includes != null)
            {
                foreach (var item in includes)
                {
                    if (query.Include(item) != null)
                        query = query.Include(item);
                }
            }
            return query.FirstOrDefault(criteria);
        }

        public async Task<T> FindAsync(Expression<Func<T, bool>> criteria, string[] includes = null)
        {
            IQueryable<T> query = context.Set<T>();
            if (includes != null)
            {
                foreach (var item in includes)
                {
                    if (query.Include(item) != null)
                        query = query.Include(item);
                }
            }
            return await query.FirstOrDefaultAsync(criteria);
        }

        public List<T> FindAll(Expression<Func<T, bool>> criteria, string[] includes = null)
        {
            IQueryable<T> query = context.Set<T>();
            if (includes != null)
            {
                foreach (var item in includes)
                {
                    if (query.Include(item) != null)
                        query = query.Include(item);
                }
            }
            return query.Where(criteria).ToList();
        }

        public async Task<List<T>> FindAllAsync(Expression<Func<T, bool>> criteria, string[] includes = null)
        {
            IQueryable<T> query = context.Set<T>();
            if (includes != null)
            {
                foreach (var item in includes)
                {
                    if (query.Include(item) != null)
                        query = query.Include(item);
                }
            }
            return await query.Where(criteria).ToListAsync();
        }

        public List<T> GetAll(string[] includes = null)
        {
            IQueryable<T> query = context.Set<T>();
            if (includes != null)
            {
                foreach (var item in includes)
                {
                    if (query.Include(item) != null)
                        query = query.Include(item);
                }
            }
            return query.ToList();
        }

        public async Task<List<T>> GetAllAsync(string[] include = null)
        {
            IQueryable<T> query = context.Set<T>();
            if (include != null)
            {
                foreach (var item in include)
                {
                    if (query.Include(item) != null)
                        query = query.Include(item);
                }
            }
            var x = await query.ToListAsync();
            return x;
        }

        public void UpdateOne(T entity)
        {
            context.Entry(entity).State = EntityState.Modified;
        }

        public IQueryable<T> QueryableFind(Expression<Func<T, bool>> criteria)
        {
            return context.Set<T>().Where(criteria);
        }

        public void AddList(List<T> entities)
        {
            context.Set<T>().AddRange(entities);
        }

        public void UpdateList(List<T> entities)
        {
            context.Set<T>().UpdateRange(entities);
        }

        public void Remove(T entity)
        {
            context.Set<T>().Remove(entity);
        }
    }
}
