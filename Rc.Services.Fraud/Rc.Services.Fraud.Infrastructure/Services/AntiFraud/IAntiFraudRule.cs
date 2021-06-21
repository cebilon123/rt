using Rc.Services.Fraud.Application.DTO;

namespace Rc.Services.Fraud.Infrastructure.Services.AntiFraud
{
    public interface IAntiFraudRule
    {
        bool IsValid(OrderDto orderDto);
    }
}