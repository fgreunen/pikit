using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace Pikit.Shared.Logging
{
    public class Logger
        : DisposableBase
    {
        private List<ILogger> Loggers { get; set; }

        #region Singleton
        private static Logger instance;
        private static object locker = new object();
        public static Logger Instance
        {
            get
            {

                if (instance == null)
                {
                    lock (locker)
                    {
                        if (instance == null)
                        {
                            instance = new Logger();
                        }
                    }
                }

                return instance;
            }
        }

        private Logger()
        {
            LoadLoggersFromAppConfig();
        }
        #endregion

        private void LoadLoggersFromAppConfig()
        {
            Loggers = new List<ILogger>();
            var keys = ConfigurationManager.AppSettings.AllKeys;
            var loggerKeys = keys.Where(x => x.StartsWith("Logger:"));
            var failures = new List<string>();

            var assemblies = AppDomain.CurrentDomain.GetAssemblies();

            var loggersAvailable =
                from a in assemblies
                from t in a.GetTypes()
                let attributes = t.GetCustomAttributes(typeof(LoggerAttribute), true)
                where attributes != null && attributes.Length > 0
                select new { Type = t, Attributes = attributes.Cast<LoggerAttribute>() };

            loggerKeys.ToList().ForEach(x =>
            {
                try
                {
                    var splitted = x.Split(':');
                    if (splitted.Length < 2)
                    {
                        throw new ArgumentException("Config entry invalid.");
                    }

                    var loggerName = splitted[1];
                    var loggerAvailable = loggersAvailable.FirstOrDefault(y => y.Attributes.Any(z => z.LoggerName == loggerName));
                    if (loggerAvailable == null)
                    {
                        throw new ArgumentException(string.Format("Invalid Logger type. {0}", loggerName));
                    }

                    var logger = Loggers.FirstOrDefault(y => y.GetType() == loggerAvailable.Type);
                    if (logger == null)
                    {
                        logger = (ILogger)Activator.CreateInstance(loggerAvailable.Type);
                        Loggers.Add(logger);
                    }

                    if (ConfigurationManager.AppSettings[x] == "*" && splitted.Length == 2)
                    {
                        foreach (LogSeverity severity in Enum.GetValues(typeof(LogSeverity)))
                        {
                            logger.AddSeverity(severity);
                        }
                    }
                    else
                    {
                        LogSeverity severity;
                        if (Enum.TryParse<LogSeverity>(splitted[2], out severity)
                            && Enum.IsDefined(typeof(LogSeverity), severity))
                        {
                            logger.AddSeverity(severity);
                        }
                        else
                        {
                            throw new ArgumentException("Invalid Logger severity type.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                    failures.Add(x);
                }
            });

            if (failures.Count > 0)
            {
                LogError("Logging config load failures: " + string.Join(",", failures));
            }
        }

        #region Public
        public void LogInformation(
            string message)
        {
            Loggers.ForEach(l =>
            {
                try
                {
                    l.Log(message, LogSeverity.Information);
                }
                catch (Exception ex) { Debug.WriteLine(ex); }
            });
        }

        public void LogWarning(
            string message)
        {
            Loggers.ForEach(l =>
            {
                try
                {
                    l.Log(message, LogSeverity.Warning);
                }
                catch (Exception ex) { Debug.WriteLine(ex); }
            });
        }

        public void LogError(
            string message)
        {
            Loggers.ForEach(l =>
            {
                try
                {
                    l.Log(message, LogSeverity.Error);
                }
                catch (Exception ex) { }
            });
        }

        public void LogError(
            Exception ex)
        {
            LogError(ex.ToString());
        }

        public void LogFatal(
            string message)
        {
            Loggers.ForEach(l =>
            {
                try
                {
                    l.Log(message, LogSeverity.Fatal);
                }
                catch (Exception ex) { Debug.WriteLine(ex); }
            });
        }

        public void LogFatal(
            Exception ex)
        {
            LogFatal(ex.ToString());
        }

        public void LogDebug(
            string message)
        {
            Loggers.ForEach(l =>
            {
                try
                {
                    l.Log(message, LogSeverity.Debug);
                }
                catch (Exception ex) { Debug.WriteLine(ex); }
            });
        }
        #endregion
    }
}