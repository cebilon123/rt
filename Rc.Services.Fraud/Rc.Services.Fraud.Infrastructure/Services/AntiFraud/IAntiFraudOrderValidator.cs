using Rc.Services.Fraud.Application.DTO;

namespace Rc.Services.Fraud.Infrastructure.Services.AntiFraud
{
    public interface IAntiFraudOrderValidator
    {
        bool IsValidOrder(OrderDto orderDto);
    }
}