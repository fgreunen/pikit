using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pikit.Shared.Logging.Loggers
{
    [LoggerAttribute("EventLogLogger")]
    public class EventLogLogger
        : LoggerBase
    {
        private string EventLogSource;

        public EventLogLogger()
        {
            EventLogSource = ConfigurationManager.AppSettings["AppName"];
            EventLogSource = EventLogSource ?? Process.GetCurrentProcess().ProcessName;
        }

        private void EnsureExists()
        {
            if (!EventLog.SourceExists(EventLogSource))
                EventLog.CreateEventSource(EventLogSource, "Application");
        }

        public override void Log(string message, LogSeverity severity = LogSeverity.Information)
        {
            if (Contains(severity))
            {
                EnsureExists();
                switch (severity)
                {
                    case LogSeverity.Unknown:
                    case LogSeverity.Information:
                        EventLog.WriteEntry(EventLogSource, message, EventLogEntryType.Information, 1);
                        break;
                    case LogSeverity.Warning:
                        EventLog.WriteEntry(EventLogSource, message, EventLogEntryType.Warning, 1);
                        break;
                    case LogSeverity.Error:
                    case LogSeverity.Fatal:
                        EventLog.WriteEntry(EventLogSource, message, EventLogEntryType.Error, 1);
                        break;
                    default:
                        break;
                }
            }
        }
    }
}