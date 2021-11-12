using System.Threading.Tasks;
using Api.Application.Handlers;
using Api.Application.Handlers.Commands;
using Api.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [CookiesAuthorize]
    public class ClientsController : ControllerBase
    {
        private readonly IQueryDispatcher _queryDispatcher;
        private readonly ICommandDispatcher _commandDispatcher;

        public ClientsController(IQueryDispatcher queryDispatcher, ICommandDispatcher commandDispatcher)
        {
            _queryDispatcher = queryDispatcher;
            _commandDispatcher = commandDispatcher;
        }

        [HttpPost]
        public async Task<ActionResult> AddClient(CreateClientCommand clientCommand)
        {
            await _commandDispatcher.SendAsync(clientCommand);
            return Ok();
        }
    }
}