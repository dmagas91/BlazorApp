using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace BlazorApp.DAL.Repository
{
    public interface IRepositoryService<M, T>        
    {
        M GetSingle(Expression<Func<T, bool>> whereCondition);

        void Add(M entity);

        void Delete(M entity);

        void Update(M entity);

        List<M> GetAll(Expression<Func<T, bool>> whereCondition);

        List<M> GetAll();

        IQueryable<T> Query(Expression<Func<T, bool>> whereCondition);

        long Count(Expression<Func<T, bool>> whereCondition);

        long Count();
    }
}
