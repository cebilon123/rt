using System.Collections.Generic;
using System.Threading.Tasks;
using Rc.Services.Orders.Core.Events;

namespace Rc.Services.Orders.Application.Services
{
    public interface IEventProcessor
    {
        Task ProcessAsync(IEnumerable<IDomainEvent> events);
    }
}