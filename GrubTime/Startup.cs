using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using GrubTime.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using GrubTime.Middleware;
using Microsoft.AspNetCore.Routing;
using Microsoft.DotNet.PlatformAbstractions;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace GrubTime
{
    public class Startup
    {

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    builder => builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials());
            });

            services.AddMvc();

            string domain = $"https://{Configuration["Auth0:Domain"]}/";
            services.AddAuthorization(o =>
            {
                o.AddPolicy("read",
                policy => policy.Requirements.Add(new HasScopeRequirement("read", domain)));
                o.AddPolicy("create",
                policy => policy.Requirements.Add(new HasScopeRequirement("create", domain)));
            });


            services.AddOptions();
            services.Configure<Google>(Configuration.GetSection("Google"));

            services.AddRouting();

            services.AddDbContext<GrubTimeContext>(options =>
            options.UseSqlServer(Configuration["Database:DefaultConnection"]));

            //Auth0 object to be injected
            //services.Configure<Auth0Settings>(Configuration.GetSection("Auth0"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            var Jwtoptions = new JwtBearerOptions
            {
                Audience = Configuration["Auth0:ApiIdentifier"],
                Authority = $"https://{Configuration["Auth0:Domain"]}/",
                Events = new JwtBearerEvents
                {
                    OnTokenValidated = context =>
                    {
                        //context.Ticket.Principle.Identity
                        var claimsIdentity = context.Ticket.Principal.Identity as ClaimsIdentity;
                        if (claimsIdentity != null)
                        {
                            string userId = claimsIdentity.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
                            string name = claimsIdentity.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
                        }
                        return Task.FromResult(0);
                    }
                }
            };
            app.UseJwtBearerAuthentication(Jwtoptions);

            app.UseCors("AllowAll");

            //Middleware
            var GoogleApi = Configuration.GetSection("Google").Get<Google>();
            //Search Middelware
            app.UseReqParseMiddleware();
            // app.UseValuesMiddleware();
            app.UseValuesMiddleware(GoogleApi);

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Places}/{action=Post}/{id?}");
            });
        }

    }
}
