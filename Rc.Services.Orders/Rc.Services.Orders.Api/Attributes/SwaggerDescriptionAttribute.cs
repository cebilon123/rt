using System;

namespace Rc.Services.Orders.Api.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class SwaggerDescriptionAttribute : Attribute
    {
        public string Description { get; }

        public SwaggerDescriptionAttribute(string description)
        {
            Description = description;
        }
    }
}