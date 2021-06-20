using System.Threading.Tasks;
using Rc.Services.Orders.Core.Exceptions;

namespace Rc.Services.Orders.Application.Handlers.Commands.Handlers
{
    public class CreateOrderCommandHandler : ICommandHandler<CreateOrder>
    {
        public async Task HandleAsync(CreateOrder command)
        {
            if (!command.IsValid())
                throw new InvalidOrderException();
            
        }
    }
}