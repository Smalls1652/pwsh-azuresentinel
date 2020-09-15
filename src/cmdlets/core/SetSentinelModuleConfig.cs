using System.IO;
using System.Management.Automation;
using System.Text.Json;

namespace AzSentinel.PowerShell
{
    using AzSentinel.Models.PowerShell;

    [Cmdlet(VerbsCommon.Set, "SentinelModuleConfig")]
    public class SetSentinelModuleConfig : AzSentinelCmdlet
    {
        [Parameter(Mandatory = true, Position = 0)]
        public string PublicClientId
        {
            get { return publicClientId; }
            set { publicClientId = value; }
        }
        private static string publicClientId;

        [Parameter(Mandatory = true, Position = 1)]
        public string TenantId
        {
            get { return tenantId; }
            set { tenantId = value; }
        }
        private static string tenantId;

        [Parameter(Mandatory = true, Position = 2)]
        public string ResourceGroupName
        {
            get { return resourceGroupName; }
            set { resourceGroupName = value; }
        }
        private static string resourceGroupName;

        [Parameter(Mandatory = true, Position = 3)]
        public string SubscriptionId
        {
            get { return subscriptionId; }
            set { subscriptionId = value; }
        }
        private static string subscriptionId;

        [Parameter(Mandatory = true, Position = 4)]
        public string WorkspaceName
        {
            get { return workspaceName; }
            set { workspaceName = value; }
        }
        private static string workspaceName;

        protected override void ProcessRecord()
        {
            ModuleConfig configObj = new ModuleConfig(
                PublicClientId = publicClientId,
                TenantId = tenantId,
                ResourceGroupName = resourceGroupName,
                SubscriptionId = subscriptionId,
                WorkspaceName = workspaceName
            );

            string configContents = JsonSerializer.Serialize<ModuleConfig>(configObj);

            string userProfilePath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.UserProfile);

            DirectoryInfo userProfile = new DirectoryInfo(userProfilePath);

            DirectoryInfo configFolder = null;
            try
            {
                configFolder = userProfile.CreateSubdirectory(".pwsh-azsentinel");
            }
            catch (IOException)
            {
                WriteVerbose("Settings folder already exists.");
            }

            string configFile = Path.Combine(configFolder.FullName, "config.json");

            try
            {
                File.Create(configFile).Close();
            }
            catch (IOException)
            {
                WriteVerbose("Config file already exists.");
            }

            StreamWriter configWriter = new StreamWriter(configFile);

            configWriter.Write(configContents);

            configWriter.Close();

            WriteObject(configObj);
        }
    }
}