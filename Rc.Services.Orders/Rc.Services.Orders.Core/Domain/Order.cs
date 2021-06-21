using System;
using System.Collections.Generic;
using System.Linq;
using Rc.Services.Orders.Core.Events;
using Rc.Services.Orders.Core.Exceptions;
using Rc.Services.Orders.Core.ValueTypes;

namespace Rc.Services.Orders.Core.Domain
{
    public class Order : AggregateRoot
    {
        public string Email { get; }
        public decimal Amount { get; }
        public Address Address { get; }
        public string Status { get; private set; }

        private ISet<Product> _products = new HashSet<Product>();

        public IEnumerable<Product> Products
        {
            get => _products;
            private set => _products = new HashSet<Product>(value);
        }

        public Order(Guid id, string email, decimal amount, Address address, IEnumerable<Product> products, string status = "new", int version = 0)
        {
            ValidateProducts(products);

            if (amount < 0)
                throw new InvalidAmountException(amount);
            
            Email = email;
            Amount = amount;
            Address = address;
            Products = products;
            Version = version;
            Status = status;
            Id = id;
        }

        public void SetStatus(string status)
        {
            Status = status;
            AddEvent(new OrderUpdated(this));
        }

        private static void ValidateProducts(IEnumerable<Product> products)
        {
            if (products is null || !products.Any())
                throw new MissingProductsException();

            if (products.Where(p => !p.IsValid()).ToList() is var invalidProducts && invalidProducts.Any())
                throw new InvalidProductException(invalidProducts.First().Name, invalidProducts.First().Quantity);
        }

        public static Order Create(string email, decimal amount, Address address, IEnumerable<Product> products)
        {
            var order = new Order(Guid.NewGuid(),email, amount, address, products);
            order.AddEvent(new OrderCreated(order));
            return order;
        }
    }
}