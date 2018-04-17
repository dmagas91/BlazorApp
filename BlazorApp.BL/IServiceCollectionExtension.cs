using AutoMapper;
using BlazorApp.BL.Base;
using BlazorApp.BL.Interfaces;
using BlazorApp.BL.Mappings;
using BlazorApp.DAL;
using BlazorApp.DAL.Models;
using BlazorApp.DAL.Repository;
using BlazorApp.Utility.Mapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorApp.BL
{
    public static class IServiceCollectionExtension
    {
        public static IServiceCollection InjectRepositoryBL(this IServiceCollection services)
        {
            var logger = new LoggerBL();
            services.AddSingleton<IBaseBL>(logger);

            

            return services.InjectRepositoryDAL();
        }

        public static IServiceCollection InitializeDbSettings(this IServiceCollection services, string connectionString)
        {
            //Initialization in Service extension DAL
            return services.InitializeDbSettingsDal(connectionString);            
        }

        public static IServiceCollection InitializeAutoMapper(this IServiceCollection services)
        {
            #region AutoMapper Initialization
            var mapperFactory = new MapperFactory();
            var config = new AutoMapper.MapperConfiguration(cfg =>
            {
                var profileReg = (Profile)Activator.CreateInstance(typeof(RegistrationViewModelToApplicationUserDTO));
                cfg.AddProfile(profileReg);
                var profileLogin = (Profile)Activator.CreateInstance(typeof(LoginViewModelToApplicationUserDTO));
                cfg.AddProfile(profileLogin);
                
            });

            var mapperBL = config.CreateMapper();

            mapperFactory.Mappers.Add("BL", mapperBL);

            return services.InitializeAutoMapper(mapperFactory); 
            #endregion
        }
    }
}
