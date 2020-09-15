using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace AzSentinel.Models.Incidents
{
    public class IncidentList
    {
        [JsonPropertyName("nextLink")]
        public Uri NextLink { get; set; }

        [JsonPropertyName("value")]
        public List<Incident> Value { get; set; }
    }
}