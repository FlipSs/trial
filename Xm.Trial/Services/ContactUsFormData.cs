﻿using Newtonsoft.Json;

namespace Xm.Trial.Services
{
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