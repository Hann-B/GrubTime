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
            services.AddMvc();
            
            services.AddDbContext<GrubTimeContext>(options => 
            options.UseSqlServer(Configuration.GetConnectionString("Database")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            var options = new JwtBearerOptions
            {
                Audience = Configuration["Auth0:ApiIdentifier"],
                Authority = $"https://{Configuration["Auth0:Domain"]}/"
            };
            app.UseJwtBearerAuthentication(options);

            //Middleware Here
            //app.Map is used to build mini pipeline for certain URL
            //when user visits ~/Search builder will run
            //app.Map(new PathString("/Search"), builder =>
            // {
            //     builder.Run(async context =>
            //     {
            //         string response = String.Format(JsonConvert.SerializeObject("Google:Nearby"));
            //         context.Response.StatusCode = StatusCodes.Status200OK;
            //         context.Response.ContentType = "application/json";
            //         context.Response.ContentLength = response.Length;
            //         await context.Response.WriteAsync(response);
            //         //to continue to next handler
            //         //return next();
            //         //else pipeline is short circuited
            //     });

            // });

            //app.MapWhen is conditional
            //when condition is met middleware is ran 
            //ie: ~/Search

            //app.Run = end of the line middleware

            app.UseMvc();

        }
    }
}
