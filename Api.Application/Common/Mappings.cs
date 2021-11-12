using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Api.Application.ResultModels;
using Api.Core.Const;
using Offer = Api.Core.Domain.Offer;

namespace Api.Application.Common
{
    public static class Mappings
    {
        public static ResultModels.Offer AsResultModel(this Offer offer)
        {
            return offer != null
                ? new ResultModels.Offer
                {
                    Id = offer.Id,
                    AuthorId = offer.AuthorId,
                    City = offer.City,
                    Images = offer.Images.AsResultModelCollection(),
                    Latitude = offer.Latitude,
                    Longitude = offer.Longitude,
                    Price = offer.Price,
                    Street = offer.Street,
                    Title = offer.Title,
                    OfferTypes = offer.OfferTypes.Select(t => Enum.GetName(typeof(OfferType), t))
                }
                : null;
        }

        public static List<ResultModels.Offer> AsResultModelCollection(this IEnumerable<Offer> offers)
        {
            return offers.Select(offer => offer.AsResultModel()).ToList();
        }

        public static Image AsResultModel(this Core.ValueTypes.Image image)
        {
            return new(image.Url, image.ImageAlt);
        }

        public static List<Image> AsResultModelCollection(this IEnumerable<Core.ValueTypes.Image> images)
        {
            return images.Select(image => image.AsResultModel()).ToList();
        }
    }
}