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
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                    });
            });


            services.AddMvc();

            services.AddOptions();
            services.Configure<Google>(Configuration.GetSection("Google"));

            services.AddRouting();

            services.AddDbContext<GrubTimeContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("Database")));

            //Auth0 object to be injected
            services.Configure<Auth0Settings>(Configuration.GetSection("Auth0"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IOptions<Auth0Settings> auth0Settings)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            var Jwtoptions = new JwtBearerOptions
            {
                Audience = Configuration["Auth0:ApiIdentifier"],
                Authority = $"https://{Configuration["Auth0:Domain"]}/"
            };
            app.UseJwtBearerAuthentication(Jwtoptions);

            app.UseCors("AllowAll");

            //Add OIDC middleware
            //var AuthOptions = new OpenIdConnectOptions("Auth0")
            //{
            //    Authority = $"https://{auth0Settings.Value.Domain}",

            //    ClientId = auth0Settings.Value.ClientId,
            //    ClientSecret = auth0Settings.Value.ClientSecret,

            //    //do not automatically authenticate and challenge
            //    AutomaticAuthenticate = false,
            //    AutomaticChallenge = false,

            //    ResponseType = "code",

            //    //Allowed Callback URL added in Auth0 dashboard??
            //    CallbackPath = new PathString("/signin-auth0"),

            //    ClaimsIssuer = "Auth0",

            //    SaveTokens = true,

            //    Events = new OpenIdConnectEvents
            //    {
            //        OnTicketReceived = context =>
            //        {
            //            //Enable return on User.Identity.Name 
            //            var identity = context.Principal.Identity as ClaimsIdentity;
            //            if (identity != null)
            //            {
            //                if (!context.Principal.HasClaim(c => c.Type == ClaimTypes.Name) &&
            //                identity.HasClaim(c => c.Type == "name"))
            //                    identity.AddClaim(new Claim(ClaimTypes.Name, identity.FindFirst("name").Value));

            //                if (context.Properties.Items.ContainsKey(".TokenNames"))
            //                {
            //                    string[] tokenNames = context.Properties.Items[".TokenNames"].Split(';');

            //                    foreach (var tokenName in tokenNames)
            //                    {
            //                        string tokenValue = context.Properties.Items[$".Token.{tokenName}"];

            //                        identity.AddClaim(new Claim(tokenName, tokenValue));
            //                    }
            //                }
            //            }
            //            return Task.CompletedTask;
            //        },
            //        //logout redirection
            //        OnRedirectToIdentityProviderForSignOut = (context) =>
            //        {
            //            var logoutUri = $"https://{auth0Settings.Value.Domain}/v2/logout?client_id={auth0Settings.Value.ClientId}";

            //            var postLogoutUri = context.Properties.RedirectUri;
            //            if (!string.IsNullOrEmpty(postLogoutUri))
            //            {
            //                if (postLogoutUri.StartsWith("/"))
            //                {
            //                    var request = context.Request;
            //                    postLogoutUri = request.Scheme + "://" + request.Host + request.PathBase + postLogoutUri;
            //                }
            //                logoutUri += $"&returnTo={Uri.EscapeDataString(postLogoutUri)}";
            //            }

            //            context.Response.Redirect(logoutUri);
            //            context.HandleResponse();

            //            return Task.CompletedTask;
            //        }
            //    }
            //};
            //AuthOptions.Scope.Clear();
            //AuthOptions.Scope.Add("openid");
            //AuthOptions.Scope.Add("name");
            //AuthOptions.Scope.Add("email");
            //AuthOptions.Scope.Add("picture");
            //app.UseOpenIdConnectAuthentication(AuthOptions);

            //Middleware
            //read request attain values
            app.UseReqParseMiddleware();

            //query api, write response
            app.UseValuesMiddleware();
            var GoogleNearbyApi = Configuration.GetSection("Google").Get<Google>();
            app.UseValuesMiddleware(GoogleNearbyApi);

            //beautify response
            //app.UseResponseMiddleware();

            //app.Map is used to build mini pipeline for certain URL
            //app.MapWhen is conditional
            //when condition is met middleware is ran 
            //ie: ~/Search
            //app.Run = end of the line middleware

            app.UseMvc();

        }

    }
}
