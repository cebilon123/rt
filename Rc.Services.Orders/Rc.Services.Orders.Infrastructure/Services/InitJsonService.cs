using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Rc.Services.Orders.Application.Handlers;
using Rc.Services.Orders.Application.Handlers.Commands;
using Rc.Services.Orders.Application.Services;
using Rc.Services.Orders.Core.Consts;
using Rc.Services.Orders.Core.Domain;
using Rc.Services.Orders.Core.Repositories;

namespace Rc.Services.Orders.Infrastructure.Services
{
    public class InitJsonService : IInitJsonService
    {
        private const string ConfigurationInitJsonKey = "InitJson";
        private const string FileName = "OrdersMock.json";

        private readonly IOrderRepository _repository;
        private readonly IConfiguration _configuration;

        public InitJsonService(IOrderRepository repository, IConfiguration configuration)
        {
            _repository = repository;
            _configuration = configuration;
        }

        public async Task Init()
        {
            if (!bool.Parse(_configuration[ConfigurationInitJsonKey]))
                return;

            if (!File.Exists(FileName))
                throw new FileNotFoundException(
                    $"{FileName} not found. Create one based on task's last page.");

            var createOrders =
                JsonConvert.DeserializeObject<IEnumerable<CreateOrder>>(await File.ReadAllTextAsync(FileName));

            var orders = createOrders.Select(co => Order.Create(co.Email, co.Amount, co.Address.ToValueTypeAddress(),
                co.Products.Select(p => p.ToValueTypeProduct())));

            foreach (var order in orders)
            {
                order.SetStatus(OrderStatus.Accepted);

                await _repository.Insert(order);
            }
        }
    }
}