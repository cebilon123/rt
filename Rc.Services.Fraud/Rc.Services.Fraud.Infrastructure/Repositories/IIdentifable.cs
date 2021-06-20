namespace Rc.Services.Fraud.Infrastructure.Repositories
{
    public interface IIdentifiable<out T>
    {
        T Id { get; }
    }
}