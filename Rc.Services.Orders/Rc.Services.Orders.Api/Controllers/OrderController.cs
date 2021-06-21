using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Rc.Services.Orders.Api.Attributes;
using Rc.Services.Orders.Application.Handlers;
using Rc.Services.Orders.Application.Handlers.Commands;
using Rc.Services.Orders.Application.Handlers.DTO;
using Rc.Services.Orders.Application.Handlers.Queries;

namespace Rc.Services.Orders.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IQueryDispatcher _queryDispatcher;

        public OrderController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher)
        {
            _commandDispatcher = commandDispatcher;
            _queryDispatcher = queryDispatcher;
        }
        
        [HttpPost]
        [SwaggerDescription("Returns ok, which means the command was accepted")]
        public async Task<ActionResult> CreateOrder(CreateOrder createOrder)
        {
            await _commandDispatcher.SendAsync(createOrder);
            return Ok();
        }

        [HttpGet]
        [Route("new")]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetOrdersWithStatusNew()
            => Ok((await _queryDispatcher.QueryAsync(new GetOrdersWithStatusNew())).Orders);
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetOrdersByEmail([FromQuery]string email)
            => Ok((await _queryDispatcher.QueryAsync(new GetOrdersByEmail(email))).Orders);
        
        [HttpGet]
        [Route("all")]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetAllOrders()
            => Ok((await _queryDispatcher.QueryAsync(new GetAllOrders())).Orders);

        [HttpPost]
        [Route("status")]
        public async Task<ActionResult> SetStatus(SetOrderStatus command)
        {
            await _commandDispatcher.SendAsync(command);
            return Ok();
        }
    }
}