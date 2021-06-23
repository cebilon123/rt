using System.Linq;
using System.Threading.Tasks;
using Rc.Services.Fraud.Application.DTO;
using Rc.Services.Fraud.Application.Services;

namespace Rc.Services.Fraud.Infrastructure.Services.AntiFraud
{
    public class AmountHigherThanAverage : IAntiFraudRule
    {
        private readonly IOrdersApi _api;

        public AmountHigherThanAverage(IOrdersApi api)
        {
            _api = api;
        }

        public async Task<bool> IsValid(OrderDto orderDto)
        {
            var orders = await _api.FetchAll();

            if (!orders.Any())
                return false;

            return !IsInvalid(orderDto.Amount, orders.Average(o => o.Amount));
        }

        private static bool IsInvalid(decimal orderAmount, decimal amountAverage)
            => orderAmount * 5 > amountAverage;
    }
}