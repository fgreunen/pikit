using Moq;
using Ninject.Modules;
using Pikit.Services.Implementations;
using Pikit.Services.Interfaces;
using Pikit.Shared.Configuration;
using Pikit.Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pikit.Tests.Modules
{
    public class TestModules
        : NinjectModule
    {
        public override void Load()
        {
            Bind<IConfigurationProperties>().To<ConfigurationProperties>().InSingletonScope();
            Bind<IImageUploadService>().To<ImageUploadService>();
            Bind<IPostService>().To<PostService>();
        }
    }
}