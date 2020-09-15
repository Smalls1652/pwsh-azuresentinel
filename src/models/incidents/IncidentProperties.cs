using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace AzSentinel.Models.Incidents
{
    public class Properties
    {
        [JsonPropertyName("title")]
        public String Title { get; set; }

        [JsonPropertyName("description")]
        public String Description { get; set; }

        [JsonPropertyName("owner")]
        public OwnerInfo Owner { get; set; }

        [JsonPropertyName("createdTimeUtc")]
        public DateTime CreatedTime { get; set; }

        [JsonPropertyName("firstActivityTimeUtc")]
        public DateTime FirstActivityTime { get; set; }

        [JsonPropertyName("lastActivityTimeUtc")]
        public DateTime LastActivityTime { get; set; }

        [JsonPropertyName("lastModifiedTimeUtc")]
        public DateTime LastModifiedTime { get; set; }

        [JsonPropertyName("incidentNumber")]
        public Int64 IncidentNumber { get; set; }

        [JsonPropertyName("incidentUrl")]
        public Uri IncidentUrl { get; set; }

        [JsonPropertyName("AdditionalDats")]
        public AdditionalData AdditionalData { get; set; }

        [JsonPropertyName("classification")]
        public String Classification { get; set; }

        [JsonPropertyName("classificationComment")]
        public String ClassificationComment { get; set; }

        [JsonPropertyName("classificationReason")]
        public String ClassificationReason { get; set; }

        [JsonPropertyName("labels")]
        public List<String> Labels { get; set; }

        [JsonPropertyName("relatedAnalyticRuleIds")]
        public List<String> RelatedAnalyticRuleIds { get; set; }
    }
}