using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrubTime.Middleware
{
    //middleware
    public class MyFirstMiddleware
    {
        //constructor
        private readonly RequestDelegate _next;
        public MyFirstMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        //HTTP request contexts
        //DI to call external source(API)
        public async Task Invoke(HttpContext httpContext)
        {
            //TODO: all work 
            await httpContext.Response.WriteAsync("Hello from MiddleWare");
            await _next.Invoke(httpContext);
            //return _next(httpContext) to pass to next handler
        }

    }
    //extension
    public static class MyFirstExtension
    {
        public static IApplicationBuilder UseMyFirstMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<MyFirstMiddleware>();
        }
    }
}
