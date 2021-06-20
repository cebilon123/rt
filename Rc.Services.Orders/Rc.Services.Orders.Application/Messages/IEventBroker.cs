using System.Threading.Tasks;

namespace Rc.Services.Orders.Application.Messages
{
    public interface IEventBroker
    {
        Task PublishAsync<TEvent>(TEvent @event) where TEvent : class, IEvent;
    }
}