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
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class ValuesMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly Google _google;

        public ValuesMiddleware(RequestDelegate next,
            IOptions<Google> optionsAccessor)
        {
            _next = next;
            _google = optionsAccessor.Value;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            var attr = httpContext.Items["parameters"] as NearbySearchVM;
            

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
            var results = JsonConvert.DeserializeObject<PlacesApiQueryResponse>(raw);

            //save changes to package
            httpContext.Items.Add("results", results);

            await _next(httpContext);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
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
