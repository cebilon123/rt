using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace Api.Hubs
{
    public class Notifications : Hub
    {
        public void test()
        {
            Clients.All.SendAsync("test", "test");
        }
    }
}