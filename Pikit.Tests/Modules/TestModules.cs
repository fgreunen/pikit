﻿using Moq;
using Ninject.Modules;
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
            Bind<IConfigurationProperties>().To<ConfigurationProperties>();
        }
    }
}