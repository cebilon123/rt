using System.Collections.Generic;

namespace Rc.Services.Orders.Application.Handlers.Commands
{
    public class CreateOrder : ICommand
    {
        public string Email { get; set; }
        public decimal Amount { get; set; }
        public CustomerAddress Address { get; set; }
        public IEnumerable<Product> Products { get; set; }

        public class Product
        {
            public string Name { get; set; }
            public int Quantity { get; set; }
        }

        public class CustomerAddress
        {
            public string Street { get; set; }
            public string Country { get; set; }
            public string City { get; set; }
            public int ZipCode { get; set; }
        }
    }
}