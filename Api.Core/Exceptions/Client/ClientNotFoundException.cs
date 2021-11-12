using System;

namespace Api.Core.Exceptions.Client
{
    public class ClientNotFoundException : DomainException
    {
        public override string Code => "client_not_found";
        
        public ClientNotFoundException(Guid id) : base($"Client with id: {id} not found.")
        {
        }

    }
}