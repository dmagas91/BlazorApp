using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlazorApp.DAL.EF
{
    public class DbService : IDbService
    {
        private readonly IServiceProvider m_ServiceProvider;
        // note here you ask to the injector for IServiceProvider
        public DbService(IServiceProvider serviceProvider)
        {
            if (serviceProvider == null)
                throw new ArgumentNullException(nameof(serviceProvider));
            m_ServiceProvider = serviceProvider;
        }

        public DbContext GetDbContext()
        {
            DbContext context;
            using (var serviceScope = m_ServiceProvider.CreateScope())
            {
                context = serviceScope.ServiceProvider.GetService<BlazorContext>();              
            }

            return context;
        }
    }
}
