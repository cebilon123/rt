using System.Linq;
using System.Threading.Tasks;
using Rc.Services.Orders.Application.Handlers.DTO;
using Rc.Services.Orders.Core.Repositories;

namespace Rc.Services.Orders.Application.Handlers.Queries.Handlers
{
    public class GetOrdersByEmailQueryHandler : IQueryHandler<GetOrdersByEmail, GetOrdersByEmailResult>
    {
        private readonly IOrderRepository _orderRepository;

        public GetOrdersByEmailQueryHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<GetOrdersByEmailResult> HandleAsync(GetOrdersByEmail query)
            => new()
            {
                Orders = (await _orderRepository.GetAsyncByEmail(query.Email)).Select(o => o.ToDto())
            };
    }
}