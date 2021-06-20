using System.Linq;
using Microsoft.OpenApi.Models;
using Rc.Services.Orders.Api.Attributes;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Rc.Services.Orders.Api.Helpers.Swagger
{
    public class ApplySwaggerDescriptionFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (context.ApiDescription.CustomAttributes()
                    .FirstOrDefault(a => a.GetType() == typeof(SwaggerDescriptionAttribute)) is
                SwaggerDescriptionAttribute
                attr)
                operation.Description = attr.Description;
        }
    }
}