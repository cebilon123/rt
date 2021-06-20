using System;

namespace Rc.Services.Orders.Core.Exceptions
{
    public class InvalidAggregateIdException : DomainException
    {
        public override string Code => "invalid_aggregateId";
        public Guid AggregateId { get; }
        
        public InvalidAggregateIdException(Guid aggregateId) : base($"Invalid aggregateId: {aggregateId}")
        {
            AggregateId = aggregateId;
        }

    }
}