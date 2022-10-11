using DingTalk.Robot;
using log4net.Core;
using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using Newtonsoft.Json.Serialization;

namespace log4net.Appender
{
    public class DingTalkRobotAppender : AppenderSkeleton
    {
        public RobotSettings? RobotSettings { get; set; }
        private readonly JsonSerializerSettings JsonSetting = new()
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        };
        private readonly MediaTypeHeaderValue MediaTypeHeader = new("application/json");
        private readonly HttpClient httpClient = new();
        protected override void Append(LoggingEvent loggingEvent)
        {
            var textMsg = new TextMsg();
            var sb = new StringBuilder();
            using (var sr = new StringWriter(sb))
            {
                Layout.Format(sr, loggingEvent);

                if (Layout.IgnoresException && loggingEvent.ExceptionObject != null)
                {
                    sr.Write(loggingEvent.GetExceptionString());
                }
                textMsg.Text = new TextContent { Content = sr.ToString() };
            }

            var content = new StringContent(JsonConvert.SerializeObject(textMsg, Formatting.Indented, JsonSetting));
            content.Headers.ContentType = MediaTypeHeader;
            try
            {
                httpClient.PostAsync(RobotSettings?.WebhookAddr, content);
            }
            catch (Exception ex)
            {
                ErrorHandler.Error($"Post to {RobotSettings?.WebhookAddr}", ex);
            }
        }
        protected override void OnClose()
        {
            httpClient.Dispose();
            base.OnClose();
        }
    }
}
