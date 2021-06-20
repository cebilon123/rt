namespace Rc.Services.Orders.Core.Exceptions
{
    public class InvalidProductException : DomainException
    {
        public override string Code => "invalid_product";

        public string Name { get; }
        public int Amount { get; }

        public InvalidProductException(string name, int amount) : base($"Invalid product: " +
                                                                       $"{nameof(Name)}: {name}" +
                                                                       $"{nameof(Amount)}: {name}")
        {
            Amount = amount;
            Name = name;
        }

        public override string ToString()
            => $"{nameof(Name)}: {Name}" +
               $"{nameof(Amount)}: {Amount}";
    }
}