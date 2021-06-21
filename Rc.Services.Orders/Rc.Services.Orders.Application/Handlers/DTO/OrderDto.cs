using System;
using System.Collections.Generic;

namespace Rc.Services.Orders.Application.Handlers.DTO
{
    public class OrderDto
    {
        public Guid Id { get; set; }
        public string Status { get; set; }
        public decimal Amount { get; set; }
        public string Email { get; set; }
        public IEnumerable<ProductDto> Products { get; set; }
    }
}