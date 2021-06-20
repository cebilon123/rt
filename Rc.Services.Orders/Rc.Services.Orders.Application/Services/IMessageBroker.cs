using System.Collections.Generic;
using System.Threading.Tasks;
using Rc.Services.Orders.Application.Events;

namespace Rc.Services.Orders.Application.Services
{
    public interface IMessageBroker
    {
        Task PublishAsync(params IEvent[] @event);
        Task PublishAsync(IEnumerable<IEvent> events);
    }
}