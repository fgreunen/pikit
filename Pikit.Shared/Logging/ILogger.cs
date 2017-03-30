namespace Pikit.Shared.Logging
{
    internal interface ILogger
    {
        void Log(string message, LogSeverity severity = LogSeverity.Information);
        void AddSeverity(LogSeverity severity);
    }
}
