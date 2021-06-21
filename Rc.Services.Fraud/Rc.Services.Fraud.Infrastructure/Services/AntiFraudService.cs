using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Rc.Services.Fraud.Application.DTO;
using Rc.Services.Fraud.Application.Services;
using Rc.Services.Fraud.Infrastructure.Services.AntiFraud;

namespace Rc.Services.Fraud.Infrastructure.Services
{
    public class AntiFraudService : IAntiFraudService
    {
        private readonly IAntiFraudOrderValidator _orderValidator;

        public AntiFraudService(IAntiFraudOrderValidator orderValidator)
        {
            _orderValidator = orderValidator;
        }

        public async Task ValidateOrder(OrderDto order)
            => await _orderValidator.IsValidOrder(order);
    }
}