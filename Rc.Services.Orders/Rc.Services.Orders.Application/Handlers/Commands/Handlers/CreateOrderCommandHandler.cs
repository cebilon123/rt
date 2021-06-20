using System.Threading.Tasks;

namespace Rc.Services.Orders.Application.Handlers.Commands.Handlers
{
    public class CreateOrderCommandHandler : ICommandHandler<CreateOrder>
    {
        public async Task HandleAsync(CreateOrder command)
        {
            throw new System.NotImplementedException();
        }
    }
}