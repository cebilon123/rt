namespace Rc.Services.Orders.Application.Handlers
{
    public static class Extensions
    {
        public static bool IsValid<T>(T obj) where T : class, IValidatable
            => obj.IsValid();
    }
}