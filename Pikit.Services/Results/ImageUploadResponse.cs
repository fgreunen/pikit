using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pikit.Services.Responses
{
    public class ImageUploadResponse
        : BaseResponse
    {
        public string ResourceUrl { get; set; }

        public ImageUploadResponse(
            string resourceUrl)
            : base()
        {
            ResourceUrl = resourceUrl;
        }
    }
}