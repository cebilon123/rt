using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Rc.Services.Fraud.Application.DTO;
using Rc.Services.Fraud.Application.Services;

namespace Rc.Services.Fraud.Infrastructure.Services
{
    public class OrdersApi : IOrdersApi
    {
        private readonly HttpClient _client = new ();
        private readonly string _orderApiAddress;
        
        public OrdersApi(IConfiguration configuration)
        {
            _orderApiAddress = configuration["OrdersApi"];
        }
        
        public async Task<IEnumerable<OrderDto>> GetOrdersWithStatusNew()
        {
            var response = await _client.GetAsync($"{_orderApiAddress}new");

            if (!response.IsSuccessStatusCode)
                return new List<OrderDto>();

            var content = await response.Content.ReadAsStringAsync();
            
            if(string.IsNullOrEmpty(content))
                return new List<OrderDto>();
            
            return JsonConvert.DeserializeObject<List<OrderDto>>(content);
        }

        public Task<IEnumerable<OrderDto>> GetOrdersForEmail(string email)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<OrderDto>> FetchAll()
        {
            throw new System.NotImplementedException();
        }
    }
}