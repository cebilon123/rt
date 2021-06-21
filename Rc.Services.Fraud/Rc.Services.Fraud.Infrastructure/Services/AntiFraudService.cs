using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Rc.Services.Fraud.Application.DTO;
using Rc.Services.Fraud.Application.Services;
using Rc.Services.Fraud.Infrastructure.Services.AntiFraud;
using Rc.Services.Orders.Core.Consts;

namespace Rc.Services.Fraud.Infrastructure.Services
{
    public class AntiFraudService : IAntiFraudService
    {
        private readonly IAntiFraudOrderValidator _orderValidator;
        private readonly IOrdersApi _ordersApi;
        private readonly ILogger<AntiFraudService> _logger;

        public AntiFraudService(IAntiFraudOrderValidator orderValidator, IOrdersApi ordersApi, ILogger<AntiFraudService> logger)
        {
            _orderValidator = orderValidator;
            _ordersApi = ordersApi;
            _logger = logger;
        }

        public async Task ValidateOrder(OrderDto order)
            => await _orderValidator.IsValidOrder(order);

        public async Task ValidateNewOrders()
        {
            var orders = await _ordersApi.GetOrdersWithStatusNew();
            
            if(!orders.Any())
                return;

            foreach (var order in orders)
            {
                _logger.LogInformation($"Validating order: {order.Id}");
                var isValid = await _orderValidator.IsValidOrder(order);
                _logger.LogInformation($"Validating result for order: {order.Id} result: {isValid}");

                await _ordersApi.SetOrderStatus(isValid ? OrderStatus.Accepted : OrderStatus.Suspended, order.Id);
            }
        }
    }
}