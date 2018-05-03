namespace AXA.Models
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using AXA.DataStore.Abstraction;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public class Configuration : BaseDataObject
    {
       
        [JsonProperty("applicationName")]
        public string ApplicationName { get; set; }

        [JsonProperty("digitalPushApiKey")]
        public string DigitalPushApiKey { get; set; }

        [JsonProperty("splashscreen")]
        public Splashscreen Splashscreen { get; set; }

        [JsonProperty("menus")]
        public List<ConfigurationMenu> Menus { get; set; }

        [JsonProperty("interstitielType")]
        public string InterstitielType { get; set; }

        [JsonProperty("interstitielLink")]
        public string InterstitielLink { get; set; }

        [JsonProperty("theme")]
        public string Theme { get; set; }

        [JsonProperty("customTheme")]
        public bool CustomTheme { get; set; }

        [JsonProperty("lastModificationAds")]
        public DateTimeOffset LastModificationAds { get; set; }

        [JsonProperty("dashboardConfig")]
        public DashboardConfig DashboardConfig { get; set; }

        [JsonProperty("viewerMenu")]
        public ViewerMenu ViewerMenu { get; set; }
    }

    public class DashboardConfig
    {
        [JsonProperty("step2")]
        public bool Step2 { get; set; }

        [JsonProperty("step3")]
        public bool Step3 { get; set; }

        [JsonProperty("step1")]
        public bool Step1 { get; set; }
    }

    public class ConfigurationMenu
    {
        [JsonProperty("lang")]
        public string Lang { get; set; }

        [JsonProperty("menu")]
        public List<MenuMenu> Menu { get; set; }
    }

    public class MenuMenu
    {
        [JsonProperty("active")]
        public bool Active { get; set; }

        [JsonProperty("action")]
        public string Action { get; set; }

        [JsonProperty("label")]
        public string Label { get; set; }

        [JsonProperty("option")]
        public string Option { get; set; }
    }

    public class Splashscreen
    {
        [JsonProperty("backgroundColor")]
        public string BackgroundColor { get; set; }

        [JsonProperty("texture")]
        public string Texture { get; set; }

        [JsonProperty("logo")]
        public Logo Logo { get; set; }

        [JsonProperty("lastUpdate")]
        public DateTimeOffset LastUpdate { get; set; }
    }

    public class Logo
    {
        [JsonProperty("active")]
        public bool Active { get; set; }

        [JsonProperty("size")]
        public string Size { get; set; }

        [JsonProperty("effect")]
        public string Effect { get; set; }
    }

    public class ViewerMenu
    {
        [JsonProperty("hasFacebook")]
        public bool HasFacebook { get; set; }

        [JsonProperty("hasTwitter")]
        public bool HasTwitter { get; set; }

        [JsonProperty("hasGoogle")]
        public bool HasGoogle { get; set; }

        [JsonProperty("hasLinkedin")]
        public bool HasLinkedin { get; set; }

        [JsonProperty("hasEmail")]
        public bool HasEmail { get; set; }

        [JsonProperty("hasDownload")]
        public bool HasDownload { get; set; }

        [JsonProperty("hasLike")]
        public bool HasLike { get; set; }

        [JsonProperty("hasPrint")]
        public bool HasPrint { get; set; }
    }
}
