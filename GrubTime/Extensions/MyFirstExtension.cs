using GrubTime.Middleware;
using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrubTime.Extensions
{
    public static class MyFirstExtension
    {
        public static IApplicationBuilder UseMyFirstMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<MyFirstMiddleware>();
        }
    }
}
