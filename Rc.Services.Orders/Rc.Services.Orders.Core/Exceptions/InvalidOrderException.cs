namespace Rc.Services.Orders.Core.Exceptions
{
    public class InvalidOrderException : DomainException
    {
        public override string Code => "invalid_order";
        public InvalidOrderException() : base("Invalid order")
        {
        }

    }
}