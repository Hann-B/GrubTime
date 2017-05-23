using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.IO;
using Newtonsoft.Json.Linq;
using Microsoft.ApplicationInsights.DataContracts;
using GrubTime.ViewModels;
using Microsoft.AspNetCore.Http.Internal;
using System.Text;
using GrubTime.Models;

namespace GrubTime.Middleware
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class ReqParseMiddleware
    {
        private readonly RequestDelegate _next;

        public ReqParseMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            //TODO: Parse request coming for values of location and radius
            var request = string.Empty;
            using (var newRequest = new MemoryStream())
            {
                //read request
                var bodyStr = "";
                var req = httpContext.Request;

                // Allows repeated use of the stream in ASP.Net Core
                req.EnableRewind();

                // Arguments: Stream, Encoding, detect encoding, buffer size 
                // AND, the most important: keep stream opened
                using (StreamReader reader
                          = new StreamReader(req.Body, Encoding.UTF8, true, 1024, true))
                {
                    bodyStr = reader.ReadToEnd();
                }

                // Rewind, so the core is not lost when it looks to the body for the request
                req.Body.Position = 0;

                //assign data
                var data = JsonConvert.DeserializeObject<NearbySearchVM>(bodyStr);

                //store data
                httpContext.Items.Add("parameters", data);
            }
            await _next(httpContext);

            return httpContext.Items["results"];

            //var list = httpContext.Items["results"] as PlacesApiQueryResponse;
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class ReqParseMiddlewareExtensions
    {
        public static IApplicationBuilder UseReqParseMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ReqParseMiddleware>();
        }
    }
}
