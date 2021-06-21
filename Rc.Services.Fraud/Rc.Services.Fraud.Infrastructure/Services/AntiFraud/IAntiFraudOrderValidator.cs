using System.Threading.Tasks;
using Rc.Services.Fraud.Application.DTO;

namespace Rc.Services.Fraud.Infrastructure.Services.AntiFraud
{
    public interface IAntiFraudOrderValidator
    {
        Task<bool> IsValidOrder(OrderDto orderDto);
    }
}