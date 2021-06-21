using System.Threading.Tasks;
using Rc.Services.Fraud.Application.DTO;

namespace Rc.Services.Fraud.Application.Services
{
    public interface IAntiFraudService
    {
        Task ValidateOrder(OrderDto order);
    }
}