using System.Linq;
using Microsoft.OpenApi.Models;
using Rc.Services.Notifications.Api.Attributes;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Rc.Services.Notifications.Api.Helpers.Swagger
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