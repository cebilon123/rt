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
                Address = order.Address.AsDocumentAddress(),
                Amount = order.Amount,
                Email = order.Email,
                Id = order.Id,
                Products = order.Products.Select(c => c.AsDocumentProduct()).ToList(),
                Version = order.Version,
                Status = order.Status
            };

        public static Address AsDocumentAddress(this Core.ValueTypes.Address address)
            => new()
            {
                City = address.City,
                Country = address.Country,
                Street = address.Street,
                ZipCode = address.ZipCode
            };

        public static Product AsDocumentProduct(this Core.ValueTypes.Product product)
            => new()
            {
                Name = product.Name,
                Quantity = product.Quantity
            };
    }
}