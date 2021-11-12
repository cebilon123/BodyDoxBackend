using System.Threading.Tasks;

namespace Api.Application.Handlers
{
    public interface ICommandDispatcher
    {
        Task SendAsync<T>(T command) where T : class, ICommand;
    }
}