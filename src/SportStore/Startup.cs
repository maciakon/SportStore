﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SportStore.Models;

namespace SportStore
{
    public class Startup
    {
        IConfigurationRoot Configuration;

        public Startup(IHostingEnvironment env)
        {
            Configuration = new ConfigurationBuilder().SetBasePath(env.ContentRootPath).AddJsonFile("appsettings.json").Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options => 
                options.UseSqlite(Configuration["Data:SportStoreProducts:ConnectionString"]));
            services.AddTransient<IProductsRepository, EFProductsRepository>();
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseStatusCodePages();
                app.UseStaticFiles();
                app.UseMvc(routes => {

                    routes.MapRoute(
                        name: null, 
                        template:"{category}/Page{page:int}", 
                        defaults: new {Controller = "Product", action="List"});

                    routes.MapRoute(
                        name: null, 
                        template:"Page{page:int}", 
                        defaults: new {Controller = "Product", action="List", page="1"});
                    
                    routes.MapRoute(
                        name: null, 
                        template:"{category}", 
                        defaults: new {Controller = "Product", action="List", page="1"});


                    routes.MapRoute(
                        name: null, 
                        template:"", 
                        defaults: new {Controller = "Product", action="List", page="1"});

                    routes.MapRoute(
                        name:null, 
                        template:"{controller=Product}/{action=List}/{id?}");});
            }
            SeedData.EnsurePopulated(app);
        }
    }
}
