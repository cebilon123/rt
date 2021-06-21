using System.Collections.Generic;
using System.Threading.Tasks;
using Rc.Services.Fraud.Application.Events;

namespace Rc.Services.Fraud.Application.Services
{
    public interface IMessageBroker
    {
        Task PublishAsync(params IEvent[] @event);
        Task PublishAsync(IEnumerable<IEvent> events);
    }
}