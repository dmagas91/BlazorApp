using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorApp.Utility.Mapper
{
    public interface IMapperFactory
    {
        IMapper GetMapper(string mapperName = "");
    }
}
