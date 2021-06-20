using Rc.Services.Orders.Core.Exceptions;

namespace Rc.Services.Orders.Core.ValueTypes
{
    public struct Product
    {
        public string Name { get; }
        public int Quantity { get; }

        public Product(string name, int quantity)
        {
            if (string.IsNullOrEmpty(name) || quantity <= 0)
                throw new InvalidProductException(name ?? "", quantity);
            
            Name = name;
            Quantity = quantity;
        }
    }
}