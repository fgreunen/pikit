using Pikit.Imaging.FileSystem;
using Pikit.Services.Interfaces;
using Pikit.Services.Requests;
using Pikit.Services.Responses;
using Pikit.Shared;
using Pikit.Shared.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pikit.Services.Implementations
{
    public class ImageUploadService
        : ServiceBase,
        IImageUploadService
    {
        public string FileDropDirectory
        {
            get
            {
                return Kernel.Get<IConfigurationProperties>().FileDropDirectory;
            }

        }
        public ImageUploadResponse UploadImage(
            ImageUploadRequest request)
        {
            string filename;
            using (var uploader = new ImageUploadFacade())
            {
                filename = uploader.SaveFile(request.ImageData, FileDropDirectory, Guid.NewGuid() + ".jpeg");
            }

            return new ImageUploadResponse(filename);
        }
    }
}