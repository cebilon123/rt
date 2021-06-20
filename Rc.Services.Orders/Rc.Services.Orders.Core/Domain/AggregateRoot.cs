using System.Collections.Generic;
using System.Linq;
using Rc.Services.Orders.Core.Events;

namespace Rc.Services.Orders.Core.Domain
{
    public abstract class AggregateRoot
    {
        private readonly ISet<IDomainEvent> _events = new HashSet<IDomainEvent>();
        
        public IEnumerable<IDomainEvent> Events => _events;
        
        public AggregateId Id { get; protected set; }
        public int Version { get; protected set; }

        protected void AddEvent(IDomainEvent @event)
        {
            if (!_events.Any())
                Version++;

            _events.Add(@event);
        }

        public void ClearEvents() => _events.Clear();
    }
}