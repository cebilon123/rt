using Rc.Services.Orders.Core.Exceptions;

namespace Rc.Services.Orders.Core.ValueTypes
{
    public struct Address
    {
        public string Street { get; }
        public string Country { get; }
        public string City { get; }
        public int ZipCode { get; }

        public Address(string street, string country, string city, int zipCode)
        {
            if (string.IsNullOrEmpty(street) || string.IsNullOrEmpty(country) || string.IsNullOrEmpty(city) ||
                zipCode == 0)
                throw new InvalidAddressException(street ?? "", country ?? "", city ?? "", zipCode);

            Street = street;
            Country = country;
            City = city;
            ZipCode = zipCode;
        }
    }
}