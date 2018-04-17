using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorApp.Utility.Mapper
{
    public class MapperFactory : IMapperFactory
    {
        public Dictionary<string, IMapper> Mappers { get; set; } = new Dictionary<string, IMapper>();
        public IMapper GetMapper(string mapperName)
        {
            return Mappers[mapperName];
        }
    }
}
