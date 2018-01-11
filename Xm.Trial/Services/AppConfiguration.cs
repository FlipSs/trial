using Newtonsoft.Json;
using System.IO;

namespace Xm.Trial.Services
{
    public class AppConfiguration : IAppConfiguration
    {
        [JsonProperty("MailData")]
        public MailData EmailData { get; private set; }

        [JsonProperty("ContactUsFormData")]
        public ContactUsFormData ContactUsData { get; private set; }


        public AppConfiguration(string jsonConfigPath)
        {
            JsonConvert.PopulateObject(File.ReadAllText(jsonConfigPath), this);
        }
    }

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

    public class ContactUsFormData
    {
        [JsonProperty]
        public string MsgBody { get; private set; }

        [JsonProperty]
        public string MsgSubject { get; private set; }

        [JsonProperty]
        public string FilesFolder { get; private set; }
    }
}