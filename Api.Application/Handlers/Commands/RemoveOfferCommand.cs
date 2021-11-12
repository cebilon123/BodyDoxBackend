using System;

namespace Api.Application.Handlers.Commands
{
    public class RemoveOfferCommand : ICommand
    {
        public Guid Id { get; }

        public RemoveOfferCommand(Guid id)
        {
            Id = id;
        }
    }
}