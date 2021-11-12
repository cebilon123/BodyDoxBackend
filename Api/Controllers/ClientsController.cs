using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Application.Common;
using Api.Application.Handlers;
using Api.Application.Handlers.Commands;
using Api.Application.Handlers.Queries;
using Api.Application.ResultModels;
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

        [HttpPut]
        public async Task<ActionResult> UpdateClient(UpdateClientCommand command, [FromQuery]Guid clientId)
        {
            await _commandDispatcher.SendAsync(command.WithId(clientId));
            return Ok();
        }

        [HttpGet("{id:guid}")]
        public async Task<Client> GetById(Guid id)
        {
            return await _queryDispatcher.QueryAsync(new GetClientByIdQuery(id));
        }

        [HttpGet]
        public async Task<ICollection<Client>> GetClients([FromQuery] Pagination pagination)
        {
            return await _queryDispatcher.QueryAsync(new GetClientsQuery(pagination ?? Pagination.Default));
        }
    }
}