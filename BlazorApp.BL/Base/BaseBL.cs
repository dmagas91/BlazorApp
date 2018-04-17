using AutoMapper;
using BlazorApp.DAL.Repository;
using BlazorApp.Utility.Mapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorApp.BL.Base
{
    public abstract class BaseBL<TViewModel, TDto, TEntity> : IBaseBL 
        where TDto : class
        where TViewModel : class
        where TEntity : class
    {

        private readonly IBaseBL _logger;
        private readonly IRepositoryService<TDto, TEntity> _repository;
        private readonly IMapper _mapper;

        public BaseBL(IRepositoryService<TDto, TEntity> repository, IMapperFactory mapperFactory, IBaseBL logger)
        {
            this._logger = logger;
            this._mapper = mapperFactory.GetMapper("BL");
            this._repository = repository;
        }

        public TDto GetDtoObject(TViewModel source)
        {
            return _mapper.Map<TViewModel, TDto>(source);
        }

        public IRepositoryService<TDto, TEntity> Repository
        {
            get
            {
                return _repository;
            }
        }
    }
}
