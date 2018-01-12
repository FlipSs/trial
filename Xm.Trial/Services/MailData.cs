using Newtonsoft.Json;

namespace Xm.Trial.Services
{
    public class MailData
    {
        [JsonProperty]
        public string MailAddress { get; private set; }

        [JsonProperty]
        public string MailTo { get; private set; }

        [JsonProperty]
        public string Password { get; private set; }

        [JsonProperty]
        public string Host { get; private set; }

        [JsonProperty]
        public int Port { get; private set; }
    }
}