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
    /// <summary>
    /// Middleware used to read in parameters from the user
    /// </summary>
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class ReqParseMiddleware
    {
        private readonly RequestDelegate _next;
        /// <summary>
        /// Middleware constructor
        /// </summary>
        /// <param name="next"></param>
        public ReqParseMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// Read coordinates and radius to set up search parameters
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

                    //store data
                    httpContext.Items.Add("parameters", bodyStr);
                }
                await _next(httpContext);
            }
            return 200;
        }
    }

    /// <summary>
    /// Extension method used to add the middleware to the HTTP request pipeline.
    /// </summary>
    public static class ReqParseMiddlewareExtensions
    {
        public static IApplicationBuilder UseReqParseMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ReqParseMiddleware>();
        }
    }
}
