using System;
using System.Collections.Generic;
using Rc.Services.Orders.Application.Handlers.DTO;

namespace Rc.Services.Orders.Application.Handlers.Queries
{
    public class GetOrdersWithStatusNew : IQuery<GetOrdersWithStatusNewResult>
    {
        
    }
    
    public class GetOrdersWithStatusNewResult
    {
        public IEnumerable<OrderDto> Orders { get; set; }
    }
}