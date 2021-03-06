﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pikit.Shared.Configuration
{
    public interface IConfigurationProperties
    {
        bool DoAuditing { get; set; }
        string FileDropDirectory { get; set; }
        string DatabaseContext { get; set; }
    }
}