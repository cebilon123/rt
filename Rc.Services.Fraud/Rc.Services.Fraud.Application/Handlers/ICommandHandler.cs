using System.Threading.Tasks;

namespace Rc.Services.Fraud.Application.Handlers
{
    public interface ICommandHandler<in TCommand> where TCommand : class, ICommand
    {
        Task HandleAsync(TCommand command);
    }
}