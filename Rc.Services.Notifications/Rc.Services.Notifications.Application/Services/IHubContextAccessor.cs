using Api.Application.Handlers.Events.External;
using Microsoft.AspNetCore.SignalR;

namespace Api.Application.Services
{
    public interface IHubContextAccessor
    {
        IHubContext<Hub> NotificationHub { get; }
    }
}