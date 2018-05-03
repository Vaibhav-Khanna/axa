using System;
using System.Collections.Generic;
using AXA.DataStore.Abstraction;
using Newtonsoft.Json;

namespace AXA.Models.DataObjects
{
    public class Publication : BaseDataObject
    {
       
        [JsonProperty("author")]
        public string Author { get; set; }

        [JsonProperty("browserCurrency")]
        public string BrowserCurrency { get; set; }

        [JsonProperty("category")]
        public string Category { get; set; }

        [JsonProperty("countLike")]
        public double? CountLike { get; set; }

        [JsonProperty("createdAt")]
        public DateTimeOffset CreatedAt { get; set; }

        [JsonProperty("defaultInAppCode")]
        public string DefaultInAppCode { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("documentDate")]
        public DateTimeOffset DocumentDate { get; set; }

        [JsonProperty("edition")]
        public string Edition { get; set; }

        [JsonProperty("excerptPages")]
        public string ExcerptPages { get; set; }

        [JsonProperty("free") ]
        public double? Free { get; set; }

        [JsonProperty("imageMode")]
        public bool? ImageMode { get; set; }

        [JsonProperty("keywords")]
        public object Keywords { get; set; }

        [JsonProperty("language")]
        public string Language { get; set; }

        [JsonProperty("lastConversion")]
        public DateTimeOffset LastConversion { get; set; }

        [JsonProperty("lastModificationAt")]
        public DateTimeOffset LastModificationAt { get; set; }

        [JsonProperty("marketingDescription")]
        public string MarketingDescription { get; set; }

        [JsonProperty("publicationStatus")]
        public string PublicationStatus { get; set; }

        [JsonProperty("publishedAt")]
        public DateTimeOffset PublishedAt { get; set; }

        [JsonProperty("summary")]
        public List<Summary> Summary { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("visibility")]
        public string Visibility { get; set; }

        [JsonProperty("desktopCurrency")]
        public string DesktopCurrency { get; set; }

        [JsonProperty("number")]
        public long? Number { get; set; }

        [JsonProperty("displayAsMonopage")]
        public bool? DisplayAsMonopage { get; set; }

        [JsonProperty("subCategory")]
        public string SubCategory { get; set; }

        [JsonProperty("price")]
        public string Price { get; set; }

        [JsonProperty("specialEdition")]
        public bool? SpecialEdition { get; set; }
    }

    public class Summary
    {
        [JsonProperty("numPage")]
        public long NumPage { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("excerpt")]
        public string Excerpt { get; set; }

        [JsonProperty("_id")]
        public string Id { get; set; }
    }

}
