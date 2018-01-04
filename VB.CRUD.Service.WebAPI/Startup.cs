using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using VB.CRUD.CrossCutting.IoC;

namespace VB.CRUD.Service.WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        IServiceProvider serviceProvider;

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddSwaggerGen(options =>
            {
                options.DescribeAllEnumsAsStrings();
                options.SwaggerDoc("v1", new Swashbuckle.AspNetCore.Swagger.Info
                {
                    Title = "VB CRUD",
                    Version = "v1",
                    Description = "Meu primeiro CRUD utilizando Mongo"
                });
            });

            ContainerBuilder container = new ContainerBuilder();
            container.Populate(services);

            container.RegisterModule(new ServiceModule(
                Configuration.GetSection("MongoDB").GetValue<string>("ConnectionString"),
                Configuration.GetSection("MongoDB").GetValue<string>("Database")));

            serviceProvider = new AutofacServiceProvider(container.Build());

            return serviceProvider;

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {

            ConfigurarLog(app, env, loggerFactory);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();

            app.UseSwagger(c => {
                c.PreSerializeFilters.Add((swagger, httpReq) =>
                {
                    swagger.Host = Configuration.GetSection("Server").GetValue<string>("Header");
                    swagger.BasePath = Configuration.GetSection("Server").GetValue<string>("BasePath");
                });
            })
            .UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("v1/swagger.json", "My API V1");
            });

        }

        private void ConfigurarLog(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {

            var appname = this.GetType().FullName.Replace($".{this.GetType().Name}", "");

            Log.Logger = new LoggerConfiguration()
              .MinimumLevel.Debug()
              .WriteTo.RollingFile($"logs/{appname}-{{Hour}}.txt")
              .CreateLogger();

            Serilog.Debugging.SelfLog.Enable(Console.Error);
            loggerFactory.AddSerilog();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                loggerFactory.AddConsole();
                loggerFactory.AddDebug();
            }
        }

    }
}
