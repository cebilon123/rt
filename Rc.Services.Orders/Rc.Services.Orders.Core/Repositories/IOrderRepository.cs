using System.Threading.Tasks;
using Rc.Services.Orders.Core.Domain;

namespace Rc.Services.Orders.Core.Repositories
{
    public interface IOrderRepository
    {
        Task Insert(Order order);
        Task<Order> GetAsync(AggregateId id);
    }
}