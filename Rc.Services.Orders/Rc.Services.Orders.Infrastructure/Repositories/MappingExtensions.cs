using System.Linq;
using Rc.Services.Orders.Core.Domain;
using Rc.Services.Orders.Infrastructure.Repositories.Documents;

namespace Rc.Services.Orders.Infrastructure.Repositories
{
    public static class MappingExtensions
    {
        public static OrderDocument AsDocument(this Order order)
            => new()
            {
                Address = order.Address.AsDocument(),
                Amount = order.Amount,
                Email = order.Email,
                Id = order.Id,
                Products = order.Products.Select(c => c.AsDocument()).ToList(),
                Version = order.Version,
                Status = order.Status
            };

        public static Order AsEntity(this OrderDocument orderDocument)
            => new(orderDocument.Id,orderDocument.Email, orderDocument.Amount, orderDocument.Address.AsEntity(),
                orderDocument.Products.Select(c => c.AsEntity()),
                orderDocument.Status, orderDocument.Version);

        public static Address AsDocument(this Core.ValueTypes.Address address)
            => new()
            {
                City = address.City,
                Country = address.Country,
                Street = address.Street,
                ZipCode = address.ZipCode
            };

        public static Core.ValueTypes.Address AsEntity(this Address address)
            => new(address.Street, address.Country, address.City, address.ZipCode);

        public static Product AsDocument(this Core.ValueTypes.Product product)
            => new()
            {
                Name = product.Name,
                Quantity = product.Quantity
            };

        public static Core.ValueTypes.Product AsEntity(this Product product)
            => new(product.Name, product.Quantity);
    }
}