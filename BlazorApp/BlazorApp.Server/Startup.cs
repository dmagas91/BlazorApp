// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using BlazorApp.BL;
using BlazorApp.BL.Interfaces;
using BlazorApp.Utility;
using Microsoft.AspNetCore.Blazor.Server;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Newtonsoft.Json.Serialization;
using System.Linq;
using System.Net.Mime;

namespace BlazorApp.Server
{
    public class Startup
    {        
    
        public Startup(IConfiguration configuration, IHostingEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;

            var builder = new ConfigurationBuilder()
            .SetBasePath(Environment.ContentRootPath)
            .AddJsonFile($"appSettings.{Environment.EnvironmentName}.json");

            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        public IHostingEnvironment Environment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().AddJsonOptions(options =>
            {
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
            }); 

            services.AddSingleton<IConfiguration>(Configuration);

            services.AddResponseCompression(options =>
            {
                options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[]
                {
                    MediaTypeNames.Application.Octet,
                    WasmMediaTypeNames.Application.Wasm,
                });
            });

            services.TryAddTransient<IHttpContextAccessor, HttpContextAccessor>();

            var connectionString = Configuration["ConnectionStrings:BlazorDb"]; 

            //Initialization in Service extension BL
            services.InjectRepositoryBL();
            services.InitializeDbSettings(connectionString);
            services.InitializeAutoMapper();

            //Initialization in Service extension Utility
            services.InjectUtilityServices(Configuration);

            services.AddSingleton<IAccountsBL, AccountsBL>();

            services.AddOptions();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {

            app.UseResponseCompression();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc(routes =>
            {
                routes.MapRoute(name: "default", template: "{controller}/{action}/{id?}");
            });

            app.UseMiddleware(typeof(ErrorHandlingMiddleware));

            app.UseBlazor<Client.Program>();

        }
    }
}
