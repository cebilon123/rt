using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Rc.Services.Orders.Core.Domain;
using Rc.Services.Orders.Core.Repositories;
using Rc.Services.Orders.Infrastructure.Repositories.Documents;

namespace Rc.Services.Orders.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IMongoGenericRepository<OrderDocument, Guid> _repository;

        public OrderRepository(IMongoGenericRepository<OrderDocument, Guid> repository)
        {
            _repository = repository;
        }

        public Task Insert(Order order)
            => _repository.AddAsync(order.AsDocument());

        public Task<Order> GetAsync(AggregateId id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<Order>> GetAsyncByStatus(string status)
            => (await _repository.FindAsync(o => o.Status == status)).Select(c => c.AsEntity());
    }
}