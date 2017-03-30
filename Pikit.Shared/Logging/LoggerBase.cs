using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pikit.Shared.Logging
{
    public abstract class LoggerBase
        : ILogger
    {
        private HashSet<LogSeverity> severities;

        public LoggerBase()
        {
            severities = new HashSet<LogSeverity>();
        }

        public void AddSeverity(LogSeverity severity)
        {
            severities.Add(severity);
        }

        protected bool Contains(LogSeverity severity)
        {
            return severities.Contains(severity);
        }

        public abstract void Log(string message, LogSeverity severity = LogSeverity.Information);
    }
}