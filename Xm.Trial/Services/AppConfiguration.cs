using Newtonsoft.Json;
using System.IO;
using Xm.Trial.Services.AppConfigurationData;

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
}