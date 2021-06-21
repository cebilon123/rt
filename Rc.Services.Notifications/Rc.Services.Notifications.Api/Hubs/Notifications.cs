using Microsoft.AspNetCore.SignalR;

namespace Rc.Services.Notifications.Api.Hubs
{
    public class Notifications : Hub
    {
        public void test()
        {
            Clients.All.SendAsync("test", "test");
        }
    }
}