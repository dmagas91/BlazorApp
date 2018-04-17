using AutoMapper;
using BlazorApp.DAL.EF;
using BlazorApp.DAL.Mappings;
using BlazorApp.DAL.Models;
using BlazorApp.DAL.Repository;
using BlazorApp.Shared.ModelsDTO;
using BlazorApp.Utility.Mapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorApp.DAL
{
    public static class IServiceCollectionExtension
    {
        public static IServiceCollection InitializeDbSettingsDal(this IServiceCollection services, string connectionString)
        {

            services.AddDbContext<BlazorContext>(options => options.UseSqlServer(connectionString));
            // add identity
            var builder = services.AddIdentityCore<User>(o =>
            {
                // configure identity options
                o.Password.RequireDigit = false;
                o.Password.RequireLowercase = false;
                o.Password.RequireUppercase = false;
                o.Password.RequireNonAlphanumeric = false;
                o.Password.RequiredLength = 6;
            });
            builder = new IdentityBuilder(builder.UserType, typeof(IdentityRole), builder.Services);
            builder.AddEntityFrameworkStores<BlazorContext>().AddDefaultTokenProviders();


            services.AddSingleton<IDbService,DbService>();

            return services;
        }

        public static IServiceCollection InitializeAutoMapper(this IServiceCollection services, MapperFactory mapperFactory)
        {
            var config = new AutoMapper.MapperConfiguration(cfg =>
            {
                var profile = (Profile)Activator.CreateInstance(typeof(ApplicationUserDTOToUser));
                cfg.AddProfile(profile);
            });

            var mapperDAL = config.CreateMapper();

            mapperFactory.Mappers.Add("DAL", mapperDAL);            
            services.AddSingleton<IMapperFactory>(mapperFactory);

            return services;
        }

        public static IServiceCollection InjectRepositoryDAL(this IServiceCollection services)
        {
            services.AddSingleton<IRepositoryService<ApplicationUserDTO, User>, AccountRepository>();
            return services;
        }
    }
}
