using System.Collections.Generic;
using Rc.Services.Orders.Application.Events;
using Rc.Services.Orders.Core.Events;

namespace Rc.Services.Orders.Application.Services
{
    public interface IDomainEventToEventMapper
    {
        IEvent Map(IDomainEvent domainEvent);
        IEnumerable<IEvent> Map(IEnumerable<IDomainEvent> domainEvents);
    }
}