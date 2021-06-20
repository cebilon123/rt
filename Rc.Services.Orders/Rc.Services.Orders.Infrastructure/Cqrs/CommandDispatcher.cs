using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Rc.Services.Orders.Application.Handlers;

namespace Rc.Services.Orders.Infrastructure.Cqrs
{
    public class CommandDispatcher : ICommandDispatcher
    {
        private readonly IServiceScopeFactory _serviceFactory;

        public CommandDispatcher(IServiceScopeFactory serviceFactory)
        {
            _serviceFactory = serviceFactory;
        }

        public async Task SendAsync<T>(T command) where T : class, ICommand
        {
            using (IServiceScope scope = _serviceFactory.CreateScope())
            {
                await scope.ServiceProvider.GetRequiredService<ICommandHandler<T>>().HandleAsync(command);
            }
        }
    }
}