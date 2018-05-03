using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace AXA.Models
{
    public class TokenResponse
    {
        public class Welcome
        {
            [JsonProperty("success")]
            public bool Success { get; set; }

            [JsonProperty("token")]
            public string Token { get; set; }

            [JsonProperty("account")]
            public Account Account { get; set; }

            [JsonProperty("expiration")]
            public long Expiration { get; set; }
        }

        public partial class Account
        {
            [JsonProperty("_id")]
            public string Id { get; set; }

            [JsonProperty("active")]
            public bool Active { get; set; }

            [JsonProperty("context")]
            public string Context { get; set; }

            [JsonProperty("createdAt")]
            public DateTimeOffset CreatedAt { get; set; }

            [JsonProperty("defaultLanguage")]
            public string DefaultLanguage { get; set; }

            [JsonProperty("label")]
            public string Label { get; set; }

            [JsonProperty("lastModificationAt")]
            public DateTimeOffset LastModificationAt { get; set; }

            [JsonProperty("optionalModules")]
            public OptionalModules OptionalModules { get; set; }

            [JsonProperty("plan")]
            public long Plan { get; set; }

            [JsonProperty("splashscreenVideoStatus")]
            public long SplashscreenVideoStatus { get; set; }

            [JsonProperty("supportEmail")]
            public string SupportEmail { get; set; }

            [JsonProperty("users")]
            public List<User> Users { get; set; }

            [JsonProperty("webDomain")]
            public string WebDomain { get; set; }

            [JsonProperty("migrationFromV2")]
            public bool MigrationFromV2 { get; set; }

            [JsonProperty("currentKue")]
            public string CurrentKue { get; set; }

            [JsonProperty("currentKueBackup")]
            public string CurrentKueBackup { get; set; }

            [JsonProperty("optionMultilangList")]
            public List<OptionMultilangList> OptionMultilangList { get; set; }

            [JsonProperty("whiteLabelUrl")]
            public string WhiteLabelUrl { get; set; }
        }

        public partial class OptionMultilangList
        {
            [JsonProperty("code")]
            public string Code { get; set; }

            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("nativeName")]
            public string NativeName { get; set; }

            [JsonProperty("date")]
            public DateTimeOffset Date { get; set; }

            [JsonProperty("edit")]
            public bool Edit { get; set; }
        }

        public partial class OptionalModules
        {
            [JsonProperty("multilingual")]
            public bool Multilingual { get; set; }

            [JsonProperty("duplicatePublication")]
            public bool DuplicatePublication { get; set; }

            [JsonProperty("categoryPublication")]
            public bool CategoryPublication { get; set; }

            [JsonProperty("whiteLabel")]
            public bool WhiteLabel { get; set; }

            [JsonProperty("dealer")]
            public bool Dealer { get; set; }

            [JsonProperty("smartphoneVersion")]
            public bool SmartphoneVersion { get; set; }

            [JsonProperty("desktopVersion")]
            public bool DesktopVersion { get; set; }
        }

        public partial class User
        {
            [JsonProperty("userId")]
            public string UserId { get; set; }

            [JsonProperty("permissions", NullValueHandling = NullValueHandling.Ignore)]
            public List<string> Permissions { get; set; }

            [JsonProperty("superAdmin", NullValueHandling = NullValueHandling.Ignore)]
            public bool? SuperAdmin { get; set; }
        }
    }
}
