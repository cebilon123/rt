using System.Threading.Tasks;

namespace Rc.Services.Fraud.Application.Messages
{
    public interface IEventHandler<in TEvent> where TEvent : class, IEvent
    {
        Task HandleAsync(TEvent @event);
    }
}