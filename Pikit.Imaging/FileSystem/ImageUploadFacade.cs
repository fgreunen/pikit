using Pikit.Shared;
using Pikit.Shared.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pikit.Imaging.FileSystem
{
    public class ImageUploadFacade
        : DisposableBase
    {
        public string SaveFile(
            byte[] imageData,
            string directory,
            string filename)
        {
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            var qualifiedFilename = Path.Combine(directory, filename);
            File.WriteAllBytes(qualifiedFilename, imageData);
            return qualifiedFilename;
        }
    }
}