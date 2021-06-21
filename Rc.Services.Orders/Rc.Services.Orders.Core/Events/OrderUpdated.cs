using Rc.Services.Orders.Core.Domain;

namespace Rc.Services.Orders.Core.Events
{
    public class OrderUpdated : IDomainEvent
    {
        public Order Order { get; }

        public OrderUpdated(Order order)
        {
            Order = order;
        }
    }
}