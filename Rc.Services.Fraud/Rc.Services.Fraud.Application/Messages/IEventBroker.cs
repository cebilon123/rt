using System.Threading.Tasks;

namespace Rc.Services.Fraud.Application.Messages
{
    public interface IEventBroker
    {
        Task PublishAsync<TEvent>(TEvent @event) where TEvent : class, IEvent;
    }
}