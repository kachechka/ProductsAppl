using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.MemoryRepositories;
using Domain.Abstraction.Repositories;
using GraphiQl;
using GraphQL;
using GraphQL.Models.InputTypes;
using GraphQL.Models.Mutations;
using GraphQL.Models.Queries;
using GraphQL.Models.Schemas;
using GraphQL.Models.Types;
using GraphQL.Types;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ProductsAppl.Domain.Core.Models;

namespace Api.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IPaginationRepository<Product>, ProductRepository>();

            services
                .AddScoped<ProductType>()
                .AddScoped<ProductQuery>()
                .AddScoped<ProductMutation>()
                .AddScoped<ProductInputType>()
                .AddScoped<ISchema, ProductSchema>()
                .AddScoped<IDependencyResolver>(s => new FuncDependencyResolver(s.GetRequiredService))
                .AddScoped<IDocumentExecuter, DocumentExecuter>();


            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseGraphiQl("/graphiql");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
