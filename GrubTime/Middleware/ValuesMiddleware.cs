using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace GrubTime.Middleware
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class ValuesMiddleware
    {
        private readonly RequestDelegate _next;

        public ValuesMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            //TODO: 
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
    }
}
