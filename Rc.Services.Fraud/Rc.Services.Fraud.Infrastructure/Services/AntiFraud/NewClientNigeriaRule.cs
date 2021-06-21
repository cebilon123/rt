using Rc.Services.Fraud.Application.DTO;

namespace Rc.Services.Fraud.Infrastructure.Services.AntiFraud
{
    public class NewClientNigeriaRule : IAntiFraudRule
    {
        public bool IsValid(OrderDto orderDto)
            => true;
    }
}