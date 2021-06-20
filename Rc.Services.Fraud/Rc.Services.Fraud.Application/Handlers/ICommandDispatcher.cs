using System.Threading.Tasks;

namespace Rc.Services.Fraud.Application.Handlers
{
    public interface ICommandDispatcher
    {
        Task SendAsync<T>(T command) where T : class, ICommand;
    }
}