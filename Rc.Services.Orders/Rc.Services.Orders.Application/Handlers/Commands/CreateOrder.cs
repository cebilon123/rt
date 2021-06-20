using System.Collections.Generic;
using System.Linq;

namespace Rc.Services.Orders.Application.Handlers.Commands
{
    public class CreateOrder : ICommand, IValidatable
    {
        public string Email { get; set; }
        public decimal Amount { get; set; }
        public CustomerAddress Address { get; set; }
        public IEnumerable<Product> Products { get; set; }

        public class Product : IValidatable
        {
            public string Name { get; set; }
            public int Quantity { get; set; }

            public bool IsValid()
                => !string.IsNullOrEmpty(Name) && Quantity > 0;
        }

        public class CustomerAddress : IValidatable
        {
            public string Street { get; set; }
            public string Country { get; set; }
            public string City { get; set; }
            public int ZipCode { get; set; }

            public bool IsValid()
                => !string.IsNullOrEmpty(Street) && !string.IsNullOrEmpty(Country) && !string.IsNullOrEmpty(City) &&
                   ZipCode > 0;
        }

        public bool IsValid()
            => !string.IsNullOrEmpty(Email) && Amount >= 0 && Address.IsValid() && Products.All(p => p.IsValid());
    }
}