using System.Collections.Generic;
using Rc.Services.Orders.Application.Handlers.DTO;

namespace Rc.Services.Orders.Application.Handlers.Queries
{
    public class GetAllOrders : IQuery<GetAllOrdersResult>
    {
        
    }
    
    public class GetAllOrdersResult
    {
        public IEnumerable<OrderDto> Orders { get; set; }
    }
}