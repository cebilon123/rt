﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace Api.Infrastructure.Repositories
{
    public interface IMongoGenericRepository<TEntity, in TIdentifiable>
        where TEntity : class, IIdentifiable<TIdentifiable>
    {
        IMongoCollection<TEntity> Collection { get; }
        Task<TEntity> GetAsync(TIdentifiable id);
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate);
        Task<IReadOnlyList<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);
        Task AddAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task UpdateAsync(TEntity entity, Expression<Func<TEntity, bool>> predicate);
        Task DeleteAsync(TIdentifiable id);
        Task DeleteAsync(Expression<Func<TEntity, bool>> predicate);
        Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate);
    }
}