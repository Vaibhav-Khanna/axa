using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace AXA.Models
{
    public partial class LoginResponse
    {
        [JsonProperty("_id")]
        public string Id { get; set; }

        [JsonProperty("categoriesBlocked")]
        public List<string> CategoriesBlocked { get; set; }

        [JsonProperty("categoriesNotifBlocked")]
        public List<string> CategoriesNotifBlocked { get; set; }

        [JsonProperty("createdAt")]
        public DateTimeOffset CreatedAt { get; set; }

        [JsonProperty("deviceRegister")]
        public DeviceRegister DeviceRegister { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        [JsonProperty("insertKind")]
        public long InsertKind { get; set; }

        [JsonProperty("language")]
        public string Language { get; set; }

        [JsonProperty("lastConnection")]
        public DateTimeOffset LastConnection { get; set; }

        [JsonProperty("lastName")]
        public string LastName { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }

        [JsonProperty("status")]
        public long Status { get; set; }
    }

    public partial class DeviceRegister
    {
        [JsonProperty("ios")]
        public List<Android> Ios { get; set; }

        [JsonProperty("android")]
        public List<Android> Android { get; set; }

        [JsonProperty("web")]
        public List<Android> Web { get; set; }
    }

    public partial class Android
    {
        [JsonProperty("device")]
        public string Device { get; set; }

        [JsonProperty("activatedAt")]
        public DateTimeOffset ActivatedAt { get; set; }
    }
}
