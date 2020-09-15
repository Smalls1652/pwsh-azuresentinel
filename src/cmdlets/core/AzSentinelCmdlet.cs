using System;
using System.IO;
using System.Management.Automation;
using System.Net.Http;
using System.Text.Json;

namespace AzSentinel.PowerShell
{
    using AzSentinel.Clients.Session;
    using AzSentinel.Models.PowerShell;
    public abstract class AzSentinelCmdlet : PSCmdlet
    {
        protected ModuleConfig GetModuleConfig()
        {
            string userProfilePath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.UserProfile);

            DirectoryInfo userProfile = new DirectoryInfo(userProfilePath);

            string configFilePath = Path.Combine(userProfile.FullName, ".pwsh-azsentinel", "config.json");

            ModuleConfig moduleConfig = null;

            if (!(File.Exists(configFilePath)))
            {
                throw new Exception("No config file found. Run 'Set-SentinelModuleConfig' on first run.");
            }
            else
            {
                StreamReader configReader = new StreamReader(configFilePath);

                moduleConfig = JsonSerializer.Deserialize<ModuleConfig>(configReader.ReadToEnd());

                configReader.Close();
            }

            return moduleConfig;
        }

        protected void SetModuleConfigSessionVariable(ModuleConfig moduleConfig)
        {
            SessionState.PSVariable.Set(new PSVariable("AzSentinelModuleConfig", moduleConfig, ScopedItemOptions.Private));
        }

        protected ModuleConfig GetModuleConfigSessionVariable()
        {
            ModuleConfig moduleConfig = (ModuleConfig)SessionState.PSVariable.GetValue("AzSentinelModuleConfig");

            return moduleConfig;
        }

        protected Uri CreateApiUri(string apiEndpoint)
        {
            ModuleConfig moduleConfig = GetModuleConfigSessionVariable();

            Uri apiUri = new Uri($"https://management.azure.com/subscriptions/{moduleConfig.SubscriptionId}/resourceGroups/{moduleConfig.ResourceGroupName}/providers/Microsoft.OperationalInsights/workspaces/{moduleConfig.WorkspaceName}/providers/Microsoft.SecurityInsights/{apiEndpoint}?api-version=2020-01-01");

            return apiUri;
        }

        protected SessionClient GetSessionClient()
        {
            SessionClient sessionClient = (SessionClient)SessionState.PSVariable.GetValue("AzSentinelSessionClient");

            if (null == sessionClient)
            {
                throw new Exception("Azure Sentinel session client not found.");
            }

            return sessionClient;
        }

        protected string SendApiCall(Uri uri, string body, HttpMethod httpMethod)
        {
            SessionClient sessionClient = GetSessionClient();

            string apiResponse = sessionClient.SendApiCall(uri.ToString(), body, httpMethod);

            return apiResponse;
        }
    }
}