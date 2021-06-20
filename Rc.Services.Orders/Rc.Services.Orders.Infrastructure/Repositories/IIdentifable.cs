namespace Rc.Services.Orders.Infrastructure.Repositories
{
    public interface IIdentifiable<out T>
    {
        T Id { get; }
    }
}