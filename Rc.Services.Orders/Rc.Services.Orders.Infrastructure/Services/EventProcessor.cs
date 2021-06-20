using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Rc.Services.Orders.Application.Services;
using Rc.Services.Orders.Core.Events;

namespace Rc.Services.Orders.Infrastructure.Services
{
    public class EventProcessor : IEventProcessor
    {
        private readonly IMessageBroker _messageBroker;
        private readonly IDomainEventToEventMapper _eventMapper;
        private readonly ILogger<EventProcessor> _logger;

        public EventProcessor(IMessageBroker messageBroker, IDomainEventToEventMapper eventMapper,
            ILogger<EventProcessor> logger)
        {
            _messageBroker = messageBroker;
            _eventMapper = eventMapper;
            _logger = logger;
        }

        public async Task ProcessAsync(IEnumerable<IDomainEvent> events)
        {
            _logger.LogTrace("Processing domain events.");
            if (events is null)
                return;

            var integrationEvents = _eventMapper.Map(events);
            if (integrationEvents is null || !integrationEvents.Any())
                return;

            _logger.LogTrace("Processing integration events.");
            await _messageBroker.PublishAsync(integrationEvents);
        }
    }
}