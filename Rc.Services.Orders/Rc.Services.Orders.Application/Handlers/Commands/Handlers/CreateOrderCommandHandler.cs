using System.Linq;
using System.Threading.Tasks;
using Rc.Services.Orders.Application.Events.External;
using Rc.Services.Orders.Application.Services;
using Rc.Services.Orders.Core.Domain;
using Rc.Services.Orders.Core.Exceptions;
using Rc.Services.Orders.Core.Repositories;

namespace Rc.Services.Orders.Application.Handlers.Commands.Handlers
{
    public class CreateOrderCommandHandler : ICommandHandler<CreateOrder>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IEventProcessor _eventProcessor;
        private readonly IMessageBroker _broker;

        public CreateOrderCommandHandler(IOrderRepository orderRepository, IEventProcessor eventProcessor,
            IMessageBroker broker)
        {
            _orderRepository = orderRepository;
            _eventProcessor = eventProcessor;
            _broker = broker;
        }

        public async Task HandleAsync(CreateOrder command)
        {
            if (!command.IsValid())
            {
                await _broker.PublishAsync(new OrderInvalidated());
                throw new InvalidOrderException();
            }

            var order = Order.Create(command.Email, command.Amount, command.Address.ToValueTypeAddress(),
                command.Products.Select(p => p.ToValueTypeProduct()));

            await _orderRepository.Insert(order);
            await _eventProcessor.ProcessAsync(order.Events);
        }
    }
}