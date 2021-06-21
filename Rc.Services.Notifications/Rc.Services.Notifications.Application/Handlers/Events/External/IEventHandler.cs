using System.Threading.Tasks;

namespace Api.Application.Handlers.Events.External
{
    public interface IEventHandler<in TEvent> where TEvent: class, IEvent
    {
        Task OnEvent(TEvent @event);
    }
}