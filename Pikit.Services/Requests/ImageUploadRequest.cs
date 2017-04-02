using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pikit.Services.Requests
{
    public class ImageUploadRequest
        : BaseRequest
    {
        public byte[] ImageData { get; set; }

        public ImageUploadRequest()
            : base()
        {
        }
    }
}