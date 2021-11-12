using System.Collections;
using System.Collections.Generic;
using Api.Application.Common;
using Api.Application.ResultModels;

namespace Api.Application.Handlers.Queries
{
    public class GetClientsQuery : IQuery<ICollection<Client>>
    {
        public Pagination Pagination { get; }

        public GetClientsQuery(Pagination pagination)
        {
            Pagination = pagination;
        }
    }
}