using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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
            Configuration = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json")
                .Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // Use SQL Database if in Azure, otherwise, use SQLite
            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Production")
            {
                services.AddDbContext<ApplicationDbContext>(options => 
                    options.UseSqlServer(Configuration.GetConnectionString("ProductsDb")));
                services.AddDbContext<AppIdentityDbContext>(options => 
                    options.UseSqlServer(Configuration.GetConnectionString("IdentityDb")));
            }
            else
            {
                services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlite(Configuration["Data:SportStoreProducts:ConnectionString"]));
                services.AddDbContext<AppIdentityDbContext>(options =>
                    options.UseSqlite(Configuration["Data:SportStoreIdentity:ConnectionString"]));
            }
            
            services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<AppIdentityDbContext>();
            services.AddTransient<IProductsRepository, EFProductsRepository>();
            services.AddTransient<IOrdersRepository, EFOrdersRepository>();
            services.AddScoped<Cart>(sp => SessionCart.GetCart(sp));
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddMvc();
            services.AddMemoryCache();
            services.AddSession();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseStatusCodePages();
            }
            else
            {
                app.UseDeveloperExceptionPage();
                //app.UseExceptionHandler("/Error");/
            }
            app.UseStaticFiles();
            app.UseSession();
            app.UseAuthentication();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "Error",
                    template: "Error",
                    defaults: new { Controller = "Error", action = "Error" });

                routes.MapRoute(
                    name: null,
                    template: "{category}/Page{page:int}",
                    defaults: new { Controller = "Product", action = "List" });

                routes.MapRoute(
                    name: null,
                    template: "Page{page:int}",
                    defaults: new { Controller = "Product", action = "List", page = "1" });

                routes.MapRoute(
                    name: null,
                    template: "{category}",
                    defaults: new { Controller = "Product", action = "List", page = "1" });


                routes.MapRoute(
                    name: null,
                    template: "",
                    defaults: new { Controller = "Product", action = "List", page = "1" });

                routes.MapRoute(
                    name: null,
                    template: "{controller=Product}/{action=List}/{id?}");
            });

            SeedData.EnsurePopulated(app);
            EntitySeedData.EnsurePopulated(app);
        }
    }
}
