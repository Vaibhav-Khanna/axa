using System;
using Newtonsoft.Json;

namespace AXA.Models
{
    public class SignUpResponse
    {
        [JsonProperty("createdAt")]
        public DateTimeOffset CreatedAt { get; set; }

        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        [JsonProperty("lastName")]
        public string LastName { get; set; }

        [JsonProperty("company")]
        public string Company { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("language")]
        public string Language { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }

        [JsonProperty("insertKind")]
        public long InsertKind { get; set; }

        [JsonProperty("status")]
        public long Status { get; set; }

        [JsonProperty("_id")]
        public string Id { get; set; }
    }
}
