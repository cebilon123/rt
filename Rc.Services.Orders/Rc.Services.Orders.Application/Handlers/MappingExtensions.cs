using Rc.Services.Orders.Application.Handlers.Commands;
using Rc.Services.Orders.Core.ValueTypes;

namespace Rc.Services.Orders.Application.Handlers
{
    public static class MappingExtensions
    {
        public static Address ToValueTypeAddress(this CreateOrder.CustomerAddress customerAddress)
            => new(customerAddress.Street, customerAddress.Country, customerAddress.City,
                customerAddress.ZipCode);

        public static Product ToValueTypeProduct(this CreateOrder.Product product)
            => new(product.Name, product.Quantity);
    }
}