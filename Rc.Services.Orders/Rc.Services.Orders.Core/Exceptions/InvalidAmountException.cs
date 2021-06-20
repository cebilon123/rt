namespace Rc.Services.Orders.Core.Exceptions
{
    public class InvalidAmountException : DomainException
    {
        public override string Code => "invalid_order";

        public decimal Amount { get; }

        public InvalidAmountException(decimal amount) : base($"Amount {amount} is invalid.")
        {
        }
    }
}