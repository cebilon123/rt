using Rc.Services.Orders.Core.Domain;

namespace Rc.Services.Orders.Core.Exceptions
{
    public class InvalidAddressException : DomainException
    {
        public override string Code => "invalid_address";

        public string Street { get; }
        public string Country { get; }
        public string City { get; }
        public int ZipCode { get; }

        public InvalidAddressException(string street, string country, string city, int zipCode) : base(
            "Invalid address: " + $"{nameof(Country)}: {country}" +
            $"{nameof(City)}: {city}" +
            $"{nameof(Street)}: {street}" +
            $"{nameof(ZipCode)}: {zipCode}")
        {
            Street = street;
            Country = country;
            City = city;
            ZipCode = zipCode;
        }

        public override string ToString()
            => $"{nameof(Country)}: {Country}" +
               $"{nameof(City)}: {City}" +
               $"{nameof(Street)}: {Street}" +
               $"{nameof(ZipCode)}: {ZipCode}";
    }
}