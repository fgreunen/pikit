using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pikit.Shared.Logging
{
    public enum LogSeverity
    {
        Unknown = -1,
        Information = 0,
        Warning = 1,
        Error = 2,
        Fatal = 3,
        Debug = 4
    }
}
