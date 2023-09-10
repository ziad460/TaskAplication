using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Task.Core.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        List<T> GetAll(string[] includes = null);

        Task<List<T>> GetAllAsync(string[] include = null);

        void AddOne(T entity);

        void AddList(List<T> entities);

        void UpdateOne(T entity);

        void UpdateList(List<T> entities);

        T Find(Expression<Func<T, bool>> criteria , string[] includes = null);

        List<T> FindAll(Expression<Func<T, bool>> criteria, string[] includes = null);

        Task<T> FindAsync(Expression<Func<T, bool>> criteria, string[] includes = null);

        Task<List<T>> FindAllAsync(Expression<Func<T, bool>> criteria, string[] includes = null);

        IQueryable<T> QueryableFind(Expression<Func<T, bool>> criteria);

        void Remove(T entity);
    }
}
