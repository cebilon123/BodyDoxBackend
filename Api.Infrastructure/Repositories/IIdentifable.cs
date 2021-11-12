namespace Api.Infrastructure.Repositories
{
    public interface IIdentifiable<out T>
    {
        T Id { get; }
    }
}