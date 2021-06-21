using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Rc.Services.Fraud.Application.DTO;

namespace Rc.Services.Fraud.Application.Services
{
    public interface IOrdersApi
    {
        Task<IEnumerable<OrderDto>> GetOrdersWithStatusNew();
        Task<IEnumerable<OrderDto>> GetOrdersForEmail(string email);
        Task<IEnumerable<OrderDto>> FetchAll();
        Task SetOrderStatus(string status, Guid orderId);
    }
}