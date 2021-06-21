using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Rc.Services.Fraud.Application.DTO;

namespace Rc.Services.Fraud.Infrastructure.Services.AntiFraud
{
    public class AntiFraudOrderValidator : IAntiFraudOrderValidator
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public AntiFraudOrderValidator(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        public async Task<bool> IsValidOrder(OrderDto orderDto)
        {
            using var scope = _scopeFactory.CreateScope();
            var rules = scope.ServiceProvider.GetServices<IAntiFraudRule>();

            foreach (var rule in rules)
            {
                if (!await rule.IsValid(orderDto))
                    return false;
            }

            return true;
        }
            
    }
}