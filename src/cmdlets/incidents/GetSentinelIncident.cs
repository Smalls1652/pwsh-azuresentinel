using System;
using System.Management.Automation;
using System.Net.Http;
using System.Text.Json;
using Microsoft.Identity.Client;

namespace AzSentinel.PowerShell
{
    using AzSentinel.Models.Incidents;

    [Cmdlet(VerbsCommon.Get, "SentinelIncident", DefaultParameterSetName = "AllIncidents")]
    public class GetSentinelIncident : AzSentinelCmdlet
    {
        [Parameter(Position = 0, ParameterSetName = "SingleIncident")]
        public Guid IncidentId
        {
            get { return incidentId; }
            set { incidentId = value; }
        }
        private Guid incidentId;

        [Parameter(Position = 1, ParameterSetName = "AllIncidents")]
        public SwitchParameter All
        {
            get { return _all; }
            set { _all = value; }
        }
        private SwitchParameter _all = true;

        protected override void ProcessRecord()
        {
            Uri apiUri = null;
            switch (ParameterSetName)
            {
                case "SingleIncident":
                    apiUri = CreateApiUri($"incidents/{incidentId.ToString()}");
                    break;

                case "AllIncidents":
                    apiUri = CreateApiUri("incidents");
                    break;
            }

            WriteVerbose($"Starting api call to {apiUri}.");
            string apiJson = SendApiCall(apiUri, null, HttpMethod.Get);

            WriteVerbose(apiJson);

            switch (ParameterSetName)
            {
                case "SingleIncident":
                    Incident apiResult = JsonSerializer.Deserialize<Incident>(apiJson);
                    WriteObject(apiResult);
                    break;

                case "AllIncidents":
                    IncidentList apiResults = JsonSerializer.Deserialize<IncidentList>(apiJson);

                    foreach (Incident item in apiResults.Value)
                    {
                        WriteObject(item);
                    }
                    break;
            }

        }
    }
}