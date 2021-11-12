using System;
using System.Collections.Generic;
using Api.Core.Const;

namespace Api.Application.Handlers.Commands
{
    public class UpdateOfferCommand : ICommand
    {
        public Guid Id { get; }
        public UpdateOfferData UpdateOfferDataData { get; }

        public UpdateOfferCommand(Guid id, UpdateOfferData updateOfferDataData)
        {
            Id = id;
            UpdateOfferDataData = updateOfferDataData;
        }

        public class UpdateOfferData
        {
            public string Title { get; set; }
            public string City { get; set; }
            public string Street { get; set; }
            public decimal Price { get; set; }
            public List<OfferType> OfferTypes { get; set; }
        }
    }
}