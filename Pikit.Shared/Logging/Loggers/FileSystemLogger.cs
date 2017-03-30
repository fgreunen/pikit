using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pikit.Shared.Logging.Loggers
{
    [LoggerAttribute("FileSystemLogger")]
    public class FileSystemLogger
        : LoggerBase
    {
        private string Dir = "";

        public FileSystemLogger()
        {
            Dir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs");
            EnsureDirectoryExists();
        }

        private string GetFileNameByLogSeverity(LogSeverity severity)
        {
            return "Log." + severity.ToString() + ".txt";
        }

        private string GetPathToFile(LogSeverity severity)
        {
            return Path.Combine(Dir, GetFileNameByLogSeverity(severity));
        }

        private void EnsureDirectoryExists()
        {
            if (!Directory.Exists(Dir))
            {
                lock (StringLocker.GetLockObject(Dir))
                {
                    if (!Directory.Exists(Dir))
                    {
                        Directory.CreateDirectory(Dir);
                    }
                }
            }
        }

        private void TruncateFileIfNecessary(LogSeverity severity)
        {
            if (DateTime.Now.Minute % 2 == 0 && DateTime.Now.Second <= 30)
            {
                var fileName = GetPathToFile(severity);
                if (File.Exists(fileName))
                {
                    lock (StringLocker.GetLockObject(fileName))
                    {
                        var length = new FileInfo(fileName).Length;
                        if (length / 1024 / 1024 > 10) // 10MB
                        {
                            var trimSize = 5 * 1024 * 1024;
                            using (MemoryStream ms = new MemoryStream(trimSize))
                            {
                                using (FileStream s = new FileStream(fileName, FileMode.Open, FileAccess.ReadWrite))
                                {
                                    s.Seek(-trimSize, SeekOrigin.End);

                                    byte[] bytes = new byte[trimSize];
                                    s.Read(bytes, 0, trimSize);
                                    ms.Write(bytes, 0, trimSize);

                                    ms.Position = 0;
                                    s.SetLength(trimSize);
                                    s.Position = 0;
                                    ms.CopyTo(s);
                                }
                            }
                        }
                    }
                }
            }
        }

        private void WriteLine(string message, LogSeverity severity)
        {
            var fileName = GetPathToFile(severity);
            lock (StringLocker.GetLockObject(fileName))
            {
                File.AppendAllLines(fileName, new List<string> { DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + " - " + message });
            }
        }

        public override void Log(string message, LogSeverity severity = LogSeverity.Information)
        {
            if (Contains(severity))
            {
                TruncateFileIfNecessary(severity);
                WriteLine(message, severity);
            }
        }
    }
}