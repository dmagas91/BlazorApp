using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using BlazorApp.DAL.EF;
using System.Linq;
using BlazorApp.Utility.Mapper;

namespace BlazorApp.DAL.Repository
{

    public abstract class RepositoryService<M, T> : IRepositoryService<M, T> where T : class
    {
        private readonly IDbService _dbService;
        private DbSet<T> _objectSet;
        private readonly IMapper _mapper;
        private DbContext _dbContext;

        public DbSet<T> ObjectSet
        {
            get
            {
                return _objectSet;
            }
        }

        public T GetEntityObject(M dtoObject)
        {
            return _mapper.Map<M, T>(dtoObject);
        }
        public abstract Expression<Func<T, bool>> SearchFilters(T obj);
        public abstract Expression<Func<T, bool>> GetFilters();

        public RepositoryService(IDbService dbService, IMapperFactory mapperFactory)
        {
            _mapper = mapperFactory.GetMapper("DAL");
            _dbService = dbService;
            _objectSet = GetObjectSet<T>();            
        }

        public RepositoryService(IDbService dbService, IMapperFactory mapperFactory, BlazorContext dbContext)
        {
            _mapper = mapperFactory.GetMapper("DAL");
            _dbService = dbService;
            _dbContext = dbContext;
            _objectSet = GetObjectSet<T>();


        }

        public DbSet<TEntity> GetObjectSet<TEntity>()
           where TEntity : class
        {
            return _dbContext.Set<TEntity>();
        }

        public IQueryable<T> GetQueryable()
        {
            return this.ObjectSet.AsQueryable<T>();
        }

        public M GetSingle(System.Linq.Expressions.Expression<Func<T, bool>> whereCondition)
        {
            var model = GetQueryable().Where(whereCondition).FirstOrDefault();
            return _mapper.Map<T, M>(model);

        }

        public T GetSingleEntity(Expression<Func<T, bool>> whereCondition)
        {
            return this.ObjectSet.Where(whereCondition).FirstOrDefault<T>();
        }

        public void Add(M entity)
        {
            T model = _mapper.Map<M, T>(entity);
            this.ObjectSet.Add(model);
        }

        public void Remove(M entity)
        {
            T model = _mapper.Map<M, T>(entity);
            this.ObjectSet.Remove(model);
        }

        public void Insert(M entity)
        {
            T model = _mapper.Map<M, T>(entity);
            this.ObjectSet.Add(model);
            _dbContext.SaveChanges();
        }

        public void Delete(M entity)
        {
            T model = _mapper.Map<M, T>(entity);
            this.ObjectSet.Remove(model);
            _dbContext.SaveChanges();
        }

        public void Update(M entity)
        {
            try
            {
                T model = _mapper.Map<M, T>(entity);
                T tEntity = GetSingleEntity(SearchFilters(model));
                this.ObjectSet.Attach(tEntity);

                _dbContext.Entry(tEntity).CurrentValues.SetValues(model);
                _dbContext.Entry(tEntity).State = EntityState.Modified;
                _dbContext.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<M> GetAll(System.Linq.Expressions.Expression<Func<T, bool>> whereCondition)
        {
            return GetQueryable().Where(whereCondition).Select(_mapper.Map<T, M>).ToList();
        }

        public List<M> GetAll()
        {
            return GetQueryable().Select(Mapper.Map<T, M>).ToList();
        }

        public IQueryable<T> Query(System.Linq.Expressions.Expression<Func<T, bool>> whereCondition)
        {
            return GetQueryable().Where(whereCondition).AsQueryable();
        }

        public long Count(System.Linq.Expressions.Expression<Func<T, bool>> whereCondition)
        {
            return GetQueryable().Where(whereCondition).Count();
        }

        public long Count()
        {
            return GetQueryable().Count();
        }
    }
}

    
