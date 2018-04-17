using BlazorApp.DAL.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp.DAL.EF
{
    public class BlazorContext : IdentityDbContext<User>
    {
        public BlazorContext(DbContextOptions<BlazorContext> options) : base(options) { }

        public DbSet<WeatherForecast> WeatherForecasts { get; set; }
        public DbSet<User> ApplicationUsers { get; set; }
        public DbSet<LogRecord> LogRecords { get; set; }

    }
}
