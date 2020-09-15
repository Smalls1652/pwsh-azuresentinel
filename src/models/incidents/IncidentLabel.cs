using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace AzSentinel.Models.Incidents
{
    public class Label
    {
        [JsonPropertyName("labelName")]
        public String LabelName { get; set; }

        [JsonPropertyName("labelType")]
        public String LabelType { get; set; }
    }
}