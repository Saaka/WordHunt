using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using WordHunt.Config;
using WordHunt.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using WordHunt.WebAPI.Config;
using Swashbuckle.AspNetCore.Swagger;
using WordHunt.Data.Initializer;
using WordHunt.WebAPI.Filters;
using WordHunt.Base;

namespace WordHunt.WebAPI
{
    public class Startup
    {
        private IConfigurationRoot configuration;

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile("dbsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile("token.json", optional: false, reloadOnChange: true)
                .AddJsonFile("seed.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddJsonFile($"dbsettings.{env.EnvironmentName}.json", optional: true)
                .AddJsonFile($"token.{env.EnvironmentName}.json", optional: true)
                .AddJsonFile($"seed.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            configuration = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();
            services.AddSwaggerGen(s => s.SwaggerDoc("v1",
                new Info { Title = "WordHunt WebAPI", Version = "1.0" }));
            services.AddSingleton(configuration);
            services.RegisterIdentity();
            services.RegisterContext();
            services.AddCors();
            services.RegisterPolicies();

            //Add application/business logic services
            services.ConfigureMappings();
            services.ConfigureBaseServices();
            services.RegisterWebApiDependencies();
            services.ConfigureDataServices();
            services.ConfigureWordHuntServices();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app,
            IHostingEnvironment env,
            ILoggerFactory loggerFactory,
            IDBInitializer initializer,
            IAuthConfiguration authConfig)
        {
            loggerFactory.AddConsole(configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseCorsConfig(env)
                .UseIdentity()
                .UseJwtBearerTokenAuthentication(authConfig)
                .UseMiddleware<ExceptionHandlingMiddleware>()
                .UseSignalR2()
                .UseMvc()
                .UseSwagger()
                .UseSwaggerUI(s => s.SwaggerEndpoint("/swagger/v1/swagger.json", "WordHunt WebAPI"));

            initializer.InitDatabase().Wait();
        }
    }
}
