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
        private readonly Type _serviceType;
        private readonly string _endpointPath;
        //private readonly MessageEncoder _messageEncoder;

        public MyFirstMiddleware(RequestDelegate next, Type seriveType, string path)
        {
            _next = next;
            _serviceType = seriveType;
            _endpointPath = path;
        }
        
        //HTTP request contexts
        public async Task Invoke(HttpContext httpContext)
        {
            Console.WriteLine("Request");

            //call the next middleware delegate
            await _next.Invoke(httpContext);
        }
    }
}
