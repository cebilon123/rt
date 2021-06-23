using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Rc.Services.Orders.Application.Services;
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
                    $"{FileName} not found. Create one based on task's last page (with extra key-pair: status = \"accepted\"");
        }
    }
}