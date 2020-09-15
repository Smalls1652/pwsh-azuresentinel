namespace AzSentinel.Models.PowerShell
{
    public class ModuleConfig
    {
        public ModuleConfig() { }

        public ModuleConfig(string publicClientId, string tenantId, string resourceGroupName, string subscriptionId, string workspaceName)
        {
            PublicClientId = publicClientId;
            TenantId = tenantId;
            ResourceGroupName = resourceGroupName;
            SubscriptionId = subscriptionId;
            WorkspaceName = workspaceName;
        }

        public string PublicClientId { get; set; }
        public string TenantId { get; set; }
        public string ResourceGroupName { get; set; }
        public string SubscriptionId { get; set; }
        public string WorkspaceName { get; set; }

    }
}