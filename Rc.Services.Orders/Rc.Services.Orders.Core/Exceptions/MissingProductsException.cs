namespace Rc.Services.Orders.Core.Exceptions
{
    public class MissingProductsException : DomainException
    {
        public override string Code => "missing_products";

        public MissingProductsException() : base("Products are missing.")
        {
        }
    }
}