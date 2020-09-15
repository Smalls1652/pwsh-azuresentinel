using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace AzSentinel.Models.Incidents
{
    public class OwnerInfo
    {
        [JsonPropertyName("assignedTo")]
        public String AssignedTo { get; set; }

        [JsonPropertyName("email")]
        public String Email { get; set; }

        [JsonPropertyName("objectId")]
        public String UserObjectId { get; set; }

        [JsonPropertyName("userPrincipalName")]
        public String UserPrincipalName { get; set; }
    }
}