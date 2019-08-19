using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProvaTecnicaApi.Service.Filters
{
    public class SwaggerUIFilter : IOperationFilter
    {
        public void Apply(Swashbuckle.AspNetCore.Swagger.Operation operation, OperationFilterContext context)
        {
            string token = "";

            if (operation == null) return;

            if (operation.Parameters == null)
            {
                operation.Parameters = new List<IParameter>();
            }

            if (!context.ApiDescription.RelativePath.ToLower().EndsWith("login"))
            {
                var parameter = new NonBodyParameter
                {
                    Description = "Authorization token (Bearer)",
                    @In = "header",
                    Name = "Authorization",
                    Default = "Bearer " + token,
                    Required = true,
                    Type = "string"
                };
                operation.Parameters.Add(parameter);
            }
        }
    }
}
