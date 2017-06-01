using GrubTime.Models;
using Microsoft.AspNetCore.JsonPatch.Operations;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrubTime
{
    public class AddRequiredHeaderParameter: IOperationFilter
    {
        void IOperationFilter.Apply(Swashbuckle.AspNetCore.Swagger.Operation operation, OperationFilterContext context)
        {
            var param = new Param();
            param.Name = "authorization";
            param.In = "header";
            param.Description = "JWT Token";
            param.Required = false;
            param.Type = "string";
            if (operation.Parameters == null)
                operation.Parameters = new List<IParameter>();
            operation.Parameters.Add(param);
        }
    }
}
