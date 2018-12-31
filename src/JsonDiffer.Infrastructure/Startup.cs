using System;
using System.Collections.Generic;
using System.Reflection;
using JsonDiffer.Application.Behavior;
using JsonDiffer.Domain;
using JsonDiffer.Domain.Entities;
using JsonDiffer.Infrastructure.InputFormattter;
using JsonDiffer.Infrastructure.Repositories;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
namespace JsonDiffer.Infrastructure
{
    public class Startup
    {
        private const string _swaggerJsonPath = "/swagger/v1/swagger.json";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();
            services.AddSwaggerGen(options =>
            {
                options.DescribeAllEnumsAsStrings();
                options.SwaggerDoc("v1", new Swashbuckle.AspNetCore.Swagger.Info
                {
                    Title = "JsonDiffer HTTP API",
                    Version = "v1",
                    Description = "JSON Diff Service HTTP API",
                    TermsOfService = "Terms Of Service"
                });
            });
            services.AddMvc(o => o.InputFormatters.Insert(0, new RawRequestBodyFormatter()));
            services.AddSingleton<ICollection<DiffJson>,List<DiffJson>>();
            services.AddTransient<IDiffRepository, DiffRepository>();
            services.AddMediatR(typeof(Startup).GetTypeInfo().Assembly);
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidatorBehavior<,>));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
            app.UseSwagger()
               .UseSwaggerUI(c =>
               {
                   c.SwaggerEndpoint(_swaggerJsonPath, "JsonDiffer API V1");
                   c.RoutePrefix = string.Empty;
               }
            );
        }
    }
    
}
