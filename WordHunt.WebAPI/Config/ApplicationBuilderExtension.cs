using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Owin.Builder;
using Owin;
using Owin.Security.AesDataProtectorProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordHunt.Config;

namespace WordHunt.WebAPI.Config
{
    using AppFunc = Func<IDictionary<string, object>, Task>;

    public static class ApplicationBuilderExtension
    {
        /// <summary>
        /// Jwt Bearer Authentication config
        /// </summary>
        /// <param name="app"></param>
        /// <param name="authConfig"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseJwtBearerTokenAuthentication(this IApplicationBuilder app, IAuthConfiguration authConfig)
        {
            app.UseJwtBearerAuthentication(new JwtBearerOptions
            {
                AutomaticAuthenticate = true,
                AutomaticChallenge = true,
                TokenValidationParameters = new TokenValidationParameters
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

            return app;
        }

        public static IApplicationBuilder UseCorsConfig(this IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseCors(opt =>
                {
                    opt.AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowAnyOrigin();
                });
            }
            else
            {
                app.UseCors(builder =>
                {
                    builder
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .WithOrigins("http://wordhunt.msakwa.net");

                });
            }

            return app;
        }
        
        public static IApplicationBuilder UseAppBuilder(this IApplicationBuilder app, Action<IAppBuilder> configure)
        {
            app.UseOwin(addToPipeline =>
            {
                addToPipeline(next =>
                {
                    var appBuilder = new AppBuilder();
                    appBuilder.Properties["builder.DefaultApp"] = next;

                    configure(appBuilder);

                    return appBuilder.Build<AppFunc>();
                });
            });

            return app;
        }

        public static IApplicationBuilder UseSignalR2(this IApplicationBuilder app)
        {
            app.UseAppBuilder(appBuilder => 
            {
                appBuilder.UseAesDataProtectorProvider();
                appBuilder.MapSignalR("/api/signalr", new Microsoft.AspNet.SignalR.HubConfiguration());
            });

            return app;
        }
    }
}
