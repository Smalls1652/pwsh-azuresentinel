using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace AzSentinel.Models.Incidents
{
    public class AdditionalData
    {
        [JsonPropertyName("alertProductNames")]
        public List<String> AlertProductNames { get; set; }

        [JsonPropertyName("alertsCount")]
        public Int64 AlertsCount { get; set; }

        [JsonPropertyName("bookmarksCount")]
        public Int64 BookmarksCount { get; set; }

        [JsonPropertyName("commentsCount")]
        public Int64 CommentsCount { get; set; }

        [JsonPropertyName("tactics")]
        public List<String> Tactics { get; set; }
    }
}