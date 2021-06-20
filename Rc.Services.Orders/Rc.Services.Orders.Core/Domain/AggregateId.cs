using System;
using Rc.Services.Orders.Core.Exceptions;

namespace Rc.Services.Orders.Core.Domain
{
    public class AggregateId : IEquatable<AggregateId>
    {
        public Guid Value { get; }

        public AggregateId() : this(Guid.NewGuid())
        {
            
        }

        public AggregateId(Guid value)
        {
            if (value == Guid.Empty)
                throw new InvalidAggregateIdException(value);
            
            Value = value;
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public bool Equals(AggregateId other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Value.Equals(other.Value);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((AggregateId) obj);
        }

        public static implicit operator Guid(AggregateId id) => id.Value;
        public static implicit operator AggregateId(Guid id) => new(id);

        public override string ToString() => Value.ToString();
    }
}