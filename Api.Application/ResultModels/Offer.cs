using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Application.Handlers;
using Api.Application.Handlers.Queries;
using Api.Core.Const;

namespace Api.Application.ResultModels
{
    public class Offer
    {
        public Guid Id { get; set; }
        public Guid AuthorId { get; set; }
        public List<Image> Images { get; set; }
        public string Title { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public decimal Price { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public IEnumerable<string> OfferTypes { get; set; }
    }

    public class Image
    {
        public string Url { get; }
        public string ImageAlt { get; }

        public Image(string url, string imageAlt)
        {
            Url = url;
            ImageAlt = imageAlt;
        }
    }
}