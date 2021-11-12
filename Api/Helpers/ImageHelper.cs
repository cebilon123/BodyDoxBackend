using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace Api.Helpers
{
    public static class ImageHelper
    {
        private static readonly List<string> ImageContentTypes = new()
        {
            "image/jpg",
            "image/jpeg",
            "image/pjpeg",
            "image/png",
            "image/x-png"
        };

        public static bool IsImage(IFormFile file)
        {
            return ImageContentTypes.Contains(file.ContentType);
        }
    }
}