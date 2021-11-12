using System;

namespace Api.Core.ValueTypes
{
    public class Image : IEquatable<Image>
    {
        public string Url { get; }
        public string ImageAlt { get; }

        public Image(string url, string imageAlt)
        {
            Url = url;
            ImageAlt = imageAlt;
        }

        public bool Equals(Image other)
        {
            return other != null && other.Url == Url && other.ImageAlt == ImageAlt;
        }
    }
}