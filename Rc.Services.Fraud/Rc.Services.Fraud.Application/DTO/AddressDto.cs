namespace Rc.Services.Orders.Application.Handlers.DTO
{
    public class AddressDto
    {
        public string Street { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public int ZipCode { get; set; }
    }
}