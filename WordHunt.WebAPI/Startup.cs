using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using WordHunt.Config;
using WordHunt.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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
                .AddEnvironmentVariables();
            configuration = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();
            services.AddSwaggerGen(s => s.SwaggerDoc("v1", new Swashbuckle.AspNetCore.Swagger.Info { Title = "WordHunt WebAPI", Version = "1.0" }));
            services.AddSingleton(configuration);

            //Add application/business logic services
            services.AddScoped<IAppConfiguration, WordHuntConfiguration>();
            services.AddScoped<IAuthConfiguration, WordHuntConfiguration>();
            services.AddScoped<ISeedConfiguration, WordHuntConfiguration>();
            services.AddScoped<IDBInitializer, WordHuntDBInitializer>();
            services.AddDbContext<WordHuntContext>(ServiceLifetime.Scoped);
            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<WordHuntContext>();

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

            app.UseIdentity();

            app.UseJwtBearerAuthentication(new JwtBearerOptions
            {
                AutomaticAuthenticate = true,
                AutomaticChallenge = true,
                TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authConfig.TokenKey)),
                    ValidateLifetime = true,
                    ValidateAudience = true,
                    ValidAudience = authConfig.Audience,
                    ValidateIssuer = true,
                    ValidIssuer = authConfig.Issuer
                }
            });

            app.UseMvc();

            app.UseSwagger();
            app.UseSwaggerUI(s => s.SwaggerEndpoint("/swagger/v1/swagger.json", "WordHunt WebAPI"));

            initializer.InitDatabase().Wait();
        }
    }
}
