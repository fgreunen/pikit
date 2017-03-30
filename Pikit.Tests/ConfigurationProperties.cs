using Pikit.Shared.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pikit.Tests
{
    public class ConfigurationProperties
        : IConfigurationProperties
    {
        public bool DoAuditing
        {
            get { return false; }
        }
    }
}
