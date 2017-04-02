using Pikit.Shared.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pikit.Tests
{
    public class ConfigurationProperties
        : IConfigurationProperties
    {
        public bool DoAuditing { get; set; }
        public string FileDropDirectory { get; set; }

        public ConfigurationProperties()
        {
            DoAuditing = false;
            FileDropDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"FileDropDirectory");
        }
    }
}