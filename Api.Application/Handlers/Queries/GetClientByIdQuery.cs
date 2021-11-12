using System;
using Api.Application.ResultModels;

namespace Api.Application.Handlers.Queries
{
    public class GetClientByIdQuery : IQuery<Client>
    {
        public Guid Id { get; }

        public GetClientByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}