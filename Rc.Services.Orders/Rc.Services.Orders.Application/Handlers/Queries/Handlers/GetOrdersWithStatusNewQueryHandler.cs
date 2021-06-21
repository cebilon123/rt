using System.Linq;
using System.Threading.Tasks;
using Rc.Services.Orders.Application.Handlers.DTO;
using Rc.Services.Orders.Core.Consts;
using Rc.Services.Orders.Core.Repositories;

namespace Rc.Services.Orders.Application.Handlers.Queries.Handlers
{
    public class GetOrdersWithStatusNewQueryHandler : IQueryHandler<GetOrdersWithStatusNew, GetOrdersWithStatusNewResult>
    {
        private readonly IOrderRepository _repository;

        public GetOrdersWithStatusNewQueryHandler(IOrderRepository repository)
        {
            _repository = repository;
        }

        public async Task<GetOrdersWithStatusNewResult> HandleAsync(GetOrdersWithStatusNew query)
            => new()
            {
                Orders = (await _repository.GetAsyncByStatus(OrderStatus.New)).ToList().Select(o => o.ToDto())
            };
    }
}