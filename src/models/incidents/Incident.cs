using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace AzSentinel.Models.Incidents
{
    public class Incident
    {
        [JsonPropertyName("etag")]
        public String Etag { get; set; }

        [JsonPropertyName("id")]
        public String ResourceId { get; set; }

        [JsonPropertyName("name")]
        public String ResourceName { get; set; }

        [JsonPropertyName("type")]
        public String ResourceType { get; set; }

        [JsonPropertyName("properties")]
        public Properties IncidentProperties { get; set; }
    }
}