using System.Threading.Tasks;

namespace Api.Application.Messages
{
    public interface IEventBroker
    {
        Task PublishAsync<TEvent>(TEvent @event) where TEvent : class, IEvent;
    }
}