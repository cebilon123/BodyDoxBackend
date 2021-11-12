using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Api.Application.ResultModels;
using Api.Core.Const;

namespace Api.Application.Handlers.Commands
{
    public class CreateOfferCommand : ICommand
    {
        [JsonIgnore] private List<CreateOfferImage> Images { get; set; }
        public string Title { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public decimal Price { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public List<OfferType> OfferTypes { get; set; }

        public CreateOfferCommand()
        {
            Images = new List<CreateOfferImage>();
        }

        public void AddImage(CreateOfferImage image)
        {
            Images.Add(image);
        }

        [JsonIgnore] public IReadOnlyList<CreateOfferImage> ImagesReadonly => Images;
    }

    public class CreateOfferImage
    {
        public byte[] ImageBytes { get; set; }

        public CreateOfferImage(byte[] imageBytes)
        {
            ImageBytes = imageBytes;
        }
    }
}