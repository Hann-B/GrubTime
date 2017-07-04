using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using GrubTime.ViewModels;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.Options;
using GrubTime.Models;
using Microsoft.IdentityModel.Protocols;
using System.Net;
using System.IO;
using System.Text;
using System.Net.Http;

namespace GrubTime.Middleware
{
    /// <summary>
    /// Middleware used to talk to google
    /// </summary>
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class ValuesMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly Google _google;
        /// <summary>
        /// Middleware constructor
        /// </summary>
        /// <param name="next"></param>
        /// <param name="optionsAccessor"></param>
        public ValuesMiddleware(RequestDelegate next,
            IOptions<Google> optionsAccessor)
        {
            _next = next;
            _google = optionsAccessor.Value;
        }

        /// <summary>
        /// Using the parameters, search google for restaurants in the searched are that are open now.
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public async Task<int> Invoke(HttpContext httpContext)
        {
            if (httpContext.Request.Method.ToUpper() != "POST")
            {
                await _next(httpContext);
            }
            else
            {
                //var attr = httpContext.Items["parameters"] as NearbySearchVM;

                var attr = JsonConvert.DeserializeObject<NearbySearchVM>(httpContext.Items["parameters"].ToString());

                //inject data into google api
                var placeApiUrl = string.Format(_google.Nearby,
                    attr.Location, attr.Radius);

                //query google
                HttpWebRequest query = (HttpWebRequest)WebRequest.Create(placeApiUrl);
                WebResponse response = await query.GetResponseAsync();

                //save results
                var raw = String.Empty;
                using (var reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8, true, 1024, true))
                {
                    raw = reader.ReadToEnd();
                }

                //return to JSON
                var allresults = JsonConvert.DeserializeObject<PlacesApiQueryResponse>(raw);

                //save changes to package
                httpContext.Items.Add("results", allresults);
                httpContext.Response.ContentType = "application/json";
                await httpContext.Response.WriteAsync(JsonConvert.SerializeObject(allresults));
                //await _next(httpContext);
            }
            return 200;
        }
    }

    /// <summary>
    /// Extension method used to add the middleware to the HTTP request pipeline.
    /// </summary>
    public static class ValuesMiddlewareExtensions
    {
        public static IApplicationBuilder UseValuesMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ValuesMiddleware>();
        }
        public static IApplicationBuilder UseValuesMiddleware(this IApplicationBuilder builder, Google googleNearbyApi)
        {
            return builder.UseMiddleware<ValuesMiddleware>(
                new OptionsWrapper<Google>(googleNearbyApi));
        }
    }
}
