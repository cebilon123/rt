using System.Threading.Tasks;
using Rc.Services.Orders.Application.Services;
using Rc.Services.Orders.Core.Repositories;

namespace Rc.Services.Orders.Application.Handlers.Commands.Handlers
{
    public class SetOrderStatusCommandHandler : ICommandHandler<SetOrderStatus>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IEventProcessor _eventProcessor;

        public SetOrderStatusCommandHandler(IOrderRepository orderRepository, IEventProcessor eventProcessor)
        {
            _orderRepository = orderRepository;
            _eventProcessor = eventProcessor;
        }
        
        public async Task HandleAsync(SetOrderStatus command)
        {
            var order = await _orderRepository.GetAsync(command.Id);
            
            if(order is null)
                return;

            order.SetStatus(command.Status);
            await _orderRepository.UpdateAsync(order);
            await _eventProcessor.ProcessAsync(order.Events);
        }
    }
}