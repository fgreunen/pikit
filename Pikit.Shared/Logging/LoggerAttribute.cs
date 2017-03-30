using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pikit.Shared.Logging
{
    [AttributeUsage(AttributeTargets.Class)]
    public class LoggerAttribute
        : Attribute
    {
        public string LoggerName { get; set; }

        public LoggerAttribute(
            string loggerName)
        {
            LoggerName = loggerName;
        }
    }
}
