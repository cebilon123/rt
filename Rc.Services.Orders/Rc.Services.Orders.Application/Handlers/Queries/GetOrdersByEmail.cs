using System.Collections.Generic;
using Rc.Services.Orders.Application.Handlers.DTO;

namespace Rc.Services.Orders.Application.Handlers.Queries
{
    public class GetOrdersByEmail : IQuery<GetOrdersByEmailResult>
    {
        public string Email { get; }

        public GetOrdersByEmail(string email)
        {
            Email = email;
        }
    }
    
    public class GetOrdersByEmailResult
    {
        public IEnumerable<OrderDto> Orders { get; set; }
    }
}