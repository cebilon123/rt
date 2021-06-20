using Rc.Services.Orders.Core.Domain;

namespace Rc.Services.Orders.Core.Events
{
    public class OrderCreated : IDomainEvent
    {
        public Order Order { get; }

        public OrderCreated(Order order)
            => Order = order;
    }
}