using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Rc.Services.Orders.Api.Attributes;
using Rc.Services.Orders.Application.Handlers;
using Rc.Services.Orders.Application.Handlers.Commands;

namespace Rc.Services.Orders.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly ICommandDispatcher _commandDispatcher;

        public OrderController(ICommandDispatcher commandDispatcher)
        {
            _commandDispatcher = commandDispatcher;
        }
        
        [HttpPost]
        [SwaggerDescription("Returns ok, which means the command was accepted")]
        public async Task<ActionResult> CreateOrder(CreateOrder createOrder)
        {
            await _commandDispatcher.SendAsync(createOrder);
            return Ok();
        }
    }
}