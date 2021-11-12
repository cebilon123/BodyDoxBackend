using System.Collections.Generic;
using System.Linq;
using Api.Application.ResultModels;

namespace Api.Application.Common
{
    public static class Mappings
    {
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