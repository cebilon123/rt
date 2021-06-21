using System.Linq;
using System.Threading.Tasks;
using Rc.Services.Fraud.Application.DTO;
using Rc.Services.Fraud.Application.Services;

namespace Rc.Services.Fraud.Infrastructure.Services.AntiFraud
{
    public class NewClientNigeriaRule : IAntiFraudRule
    {
        private readonly IOrdersApi _ordersApi;

        public NewClientNigeriaRule(IOrdersApi ordersApi)
        {
            _ordersApi = ordersApi;
        }

        public async Task<bool> IsValid(OrderDto orderDto)
        {
            var clientOrders = await _ordersApi.GetOrdersForEmail(orderDto.Email);

            return !IsInvalid(clientOrders.Count(), orderDto.Address.Country, orderDto.Amount);
        }

        private static bool IsInvalid(int ordersCount, string country, decimal amount)
            => ordersCount == 0 && country == "Nigeria" && amount > 1000;
    }
}