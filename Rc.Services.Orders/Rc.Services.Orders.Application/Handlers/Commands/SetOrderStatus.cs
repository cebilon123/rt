using System;

namespace Rc.Services.Orders.Application.Handlers.Commands
{
    public class SetOrderStatus : ICommand
    {
        public Guid Id { get; set; }
        public string Status { get; set; }
    }
}