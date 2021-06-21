using System.Threading.Tasks;
using Api.Application.Handlers.Events.External;

namespace Api.Application.Services
{
    public interface INotificationHub
    {
        Task Notifiy(Notification notification);
    }
}