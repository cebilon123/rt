namespace Rc.Services.Orders.Core.ValueTypes
{
    public static class Extensions
    {
        public static bool IsValid(this Product product)
            => product.Quantity > 0 && !string.IsNullOrEmpty(product.Name);
    }
}