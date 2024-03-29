﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
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

        public async Task<Order> GetAsync(AggregateId id)
        {
            var result = await _repository.GetAsync(o => o.Id == id);
            return result?.AsEntity();
        }

        public async Task<IEnumerable<Order>> GetAsyncByStatus(string status)
            => (await _repository.FindAsync(o => o.Status == status)).Select(c => c.AsEntity());

        public async Task<IEnumerable<Order>> GetAsyncByEmail(string email)
            => (await _repository.FindAsync(o => o.Email == email)).ToList().Select(c => c.AsEntity());

        public async Task<IEnumerable<Order>> GetAll()
            => (await _repository.Collection.AsQueryable().ToListAsync()).Select(c => c.AsEntity());

        public async Task UpdateAsync(Order order)
            => await _repository.UpdateAsync(order.AsDocument());
    }
}