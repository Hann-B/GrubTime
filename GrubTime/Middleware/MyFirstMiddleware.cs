using GrubTime.Services;
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
        private readonly ServiceDescription _service;
        //private readonly MessageEncoder _messageEncoder;

        public MyFirstMiddleware(RequestDelegate next, Type seriveType, string path)
        {
            _next = next;
            _service = new ServiceDescription(seriveType);
            _endpointPath = path;
        }
        
        //HTTP request contexts
        public async Task Invoke(HttpContext httpContext)
        {
            if (httpContext.Request.Path.Equals(_endpointPath, StringComparison.Ordinal))
            {
                //Read request message
                
            }
            else
            {
                await _next(httpContext);
            }
        }
    }
}
