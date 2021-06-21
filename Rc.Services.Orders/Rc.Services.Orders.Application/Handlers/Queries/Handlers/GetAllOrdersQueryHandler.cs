using System.Linq;
using System.Threading.Tasks;
using Rc.Services.Orders.Application.Handlers.DTO;
using Rc.Services.Orders.Core.Repositories;

namespace Rc.Services.Orders.Application.Handlers.Queries.Handlers
{
    public class GetAllOrdersQueryHandler : IQueryHandler<GetAllOrders, GetAllOrdersResult>
    {
        private readonly IOrderRepository _orderRepository;

        public GetAllOrdersQueryHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<GetAllOrdersResult> HandleAsync(GetAllOrders query)
            => new()
            {
                Orders = (await _orderRepository.GetAll()).Select(c => c.ToDto())
            };
    }
}