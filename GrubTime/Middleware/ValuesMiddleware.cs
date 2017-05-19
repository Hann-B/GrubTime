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
            var data = httpContext.Items["parameters"];

            if (httpContext.Items.ContainsKey(data))
            {
                var attr = (NearbySearchVM)data;

                //inject data into google api
                var placeApiUrl = string.Format(_google.Nearby, 
                    attr.Longitude, attr.Latitude, attr.Radius);

                //query google
                var query = WebRequest.Create(placeApiUrl);
                var response = query.GetResponseAsync();
                var raw = String.Empty;
                using (var reader = new StreamReader(response.wannGetResponseStream()))
                {
                    raw = reader.ReadToEnd();
                }
                var results = JsonConvert.DeserializeObject<PlacesApiQueryResponse>(raw);
                httpContext.Items.Add("parameters", results);

            }
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
