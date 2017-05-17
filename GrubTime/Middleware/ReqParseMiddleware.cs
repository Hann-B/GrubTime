using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.IO;

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

        public Task Invoke(HttpContext httpContext)
        {
            //TODO: Parse request coming for values of location and radius
            return _next(httpContext);
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
