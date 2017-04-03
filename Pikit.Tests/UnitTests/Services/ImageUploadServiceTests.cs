using NUnit.Framework;
using Pikit.Services.Interfaces;
using Pikit.Shared;
using Pikit.Shared.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pikit.Tests.UnitTests.Services
{
    [TestFixture]
    public class ImageUploadServiceTests
        : UnitTestBase
    {
        [Test]
        public void UploadImage_FileExists_On_LocalFileSystem()
        {
            var imageData = new byte[] { 1, 2, 3 };
            var imageUploadService = Kernel.Get<IImageUploadService>();
            var result = imageUploadService.UploadImage(new Pikit.Services.Requests.ImageUploadRequest
            {
                ImageData = imageData
            });

            FileAssert.Exists(result.ResourceUrl);

            var uploadedImageData = File.ReadAllBytes(result.ResourceUrl);

            CollectionAssert.AreEqual(imageData, uploadedImageData);
        }
    }
}