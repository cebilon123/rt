using System.Linq;
using Rc.Services.Orders.Core.Domain;
using Rc.Services.Orders.Core.ValueTypes;

namespace Rc.Services.Orders.Application.Handlers.DTO
{
    public static class Mappings
    {
        public static OrderDto ToDto(this Order order)
            => new()
            {
                Amount = order.Amount,
                Id = order.Id,
                Products = order.Products.Select(p => p.ToDto()),
                Status = order.Status,
                Email = order.Email,
                Address = order.Address.ToDto()
            };

        public static ProductDto ToDto(this Product product)
            => new()
            {
                Name = product.Name,
                Quantity = product.Quantity
            };

        public static AddressDto ToDto(this Address address)
            => new()
            {
                City = address.City,
                Country = address.Country,
                Street = address.Street,
                ZipCode = address.ZipCode
            };
    }
}