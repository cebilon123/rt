using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Rc.Services.Orders.Infrastructure.Repositories.Documents
{
    public class OrderDocument : IIdentifiable<Guid>
    {
        public Guid Id { get; set; }
        public int Version { get; set; }
        public string Email { get; set; }
        public decimal Amount { get; set; }
        public Address Address { get; set; }
        public string Status { get; set; }

        public List<Product> Products { get; set; }
    }

    public class Address
    {
        public string Street { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public int ZipCode { get; set; }
    }

    public class Product
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
    }
}