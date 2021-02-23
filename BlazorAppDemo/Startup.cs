using BlazorAppDemo.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorAppDemo.DB;
using Microsoft.EntityFrameworkCore;
using BlazorAppDemo.Interfaces;
using BlazorAppDemo.Services;
using BlazorAppDemo.Models;

namespace BlazorAppDemo
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor();

            //Adds CosmosDB DBContext
            //services.AddDbContext<productDBContext>(options => options.UseCosmos( accountEndpoint: Configuration["CosmosDB:URI"], accountKey: Configuration["CosmosDB:CS"], databaseName: Configuration["CosmosDB:Name"]));

            //Adds InMemory DBContext incl. DBSeeding
            var dbOptions = new DbContextOptionsBuilder<productDBContext>()
                .UseInMemoryDatabase(databaseName: Configuration["InMemoryDB:Name"])
                .Options;
            SeedInMemoryDB(dbOptions);
            services.AddDbContext<productDBContext>(options => options.UseInMemoryDatabase(databaseName: Configuration["InMemoryDB:Name"]));

            services.AddSingleton<WeatherForecastService>();
            services.AddScoped<IProductService, ProductService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }

        private void SeedInMemoryDB(DbContextOptions<productDBContext> dbOptions)
        {
            var ctx = new productDBContext(dbOptions);
            var product = new DevProduct
            {
                Name = "Coding Duck",
                Description = "Quack quack",
                Amount = 9,
            };
            var comment = new DevProductComment
            {
                Author = "Someone",
                Content = "Some content!",
                Date = "today"
            };
            product.Comments.Add(comment);
            ctx.Products.Add(product);
            ctx.SaveChanges();
        }
    }
}
