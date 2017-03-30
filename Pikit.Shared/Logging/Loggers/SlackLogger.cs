using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Pikit.Shared.Logging.Loggers
{
    [LoggerAttribute("SlackLogger")]
    public class SlackLogger
        : LoggerBase
    {
        private const string WEBHOOK_KEY = "SlackLogger.WebhookUrl";

        private readonly string _webhookUrl;
        private readonly HttpClient _httpClient = new HttpClient();
        private bool _isValid = false;

        public SlackLogger()
        {
            if (ConfigurationManager.AppSettings.AllKeys.Any(x => x == WEBHOOK_KEY))
            {
                _webhookUrl = ConfigurationManager.AppSettings[WEBHOOK_KEY];

                _isValid = true;
            }
        }

        private void SendMessageAsync(
            string message,
            string channel = null,
            string username = null)
        {
            var payload = new
            {
                text = message,
                channel,
                username,
            };
            var serializedPayload = JsonConvert.SerializeObject(payload);

            Task.Factory.StartNew(() =>
            {
                try
                {
                    _httpClient.PostAsync(
                        _webhookUrl,
                        new StringContent(serializedPayload, Encoding.UTF8, "application/json"));
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }
            });
        }

        private string GetSeverityText(
            LogSeverity severity)
        {
            var prefix = "";

            switch (severity)
            {
                case LogSeverity.Unknown:
                    prefix = ":question: ";
                    break;
                case LogSeverity.Information:
                    prefix = ":information_source: ";
                    break;
                case LogSeverity.Warning:
                    prefix = ":heavy_exclamation_mark: ";
                    break;
                case LogSeverity.Error:
                    prefix = ":bangbang: ";
                    break;
                case LogSeverity.Fatal:
                    prefix = ":bangbang: ";
                    break;
                case LogSeverity.Debug:
                    prefix = ":white_circle: ";
                    break;
                default:
                    break;
            }

            return prefix + severity.ToString();
        }

        public override void Log(string message, LogSeverity severity = LogSeverity.Information)
        {
            if (_isValid && Contains(severity))
            {
                var builtMessage = string.Format("{1} -> *{0}*", System.AppDomain.CurrentDomain.FriendlyName, GetSeverityText(severity));
                builtMessage += string.Format("\n\t\tMessage: {0}\n", message);

                SendMessageAsync(builtMessage);
            }
        }
    }
}