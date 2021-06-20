using System.Collections.Generic;
using System.Linq;
using Rc.Services.Orders.Application.Events;
using Rc.Services.Orders.Application.Services;
using Rc.Services.Orders.Core.Events;

namespace Rc.Services.Orders.Infrastructure.Services
{
    public class DomainEventToEventMapper : IDomainEventToEventMapper
    {
        public IEnumerable<IEvent> Map(IEnumerable<IDomainEvent> domainEvents)
            => domainEvents.Select(Map);

        public IEvent Map(IDomainEvent domainEvent)
            => domainEvent switch
            {
                _ => null
            };

    }
}