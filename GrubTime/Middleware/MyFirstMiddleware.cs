using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrubTime.Middleware
{
    public class MyFirstMiddleware
    {
        //constructor
        private readonly RequestDelegate _next;

        public MyFirstMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        
        //HTTP request contexts
        public async Task Invoke(HttpContext httpContext)
        {
            await httpContext.Response.WriteAsync("Hello from MiddleWare");
            await _next.Invoke(httpContext);
        }
    }
}
