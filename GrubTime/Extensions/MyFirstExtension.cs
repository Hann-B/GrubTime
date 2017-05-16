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
        public static IApplicationBuilder UseMyFirstMiddleware<T>(this IApplicationBuilder builder, string path)
        {
            return builder.UseMiddleware<MyFirstMiddleware>(typeof(T), path);
        }
    }
}
