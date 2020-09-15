using System;
using System.Collections.Generic;
using System.IO;
using System.Management.Automation;
using System.Threading;
using System.Threading.Tasks;
using System.Text.Json;
using Microsoft.Identity.Client;

namespace AzSentinel.PowerShell
{
    using AzSentinel.Clients.MSGraph;
    using AzSentinel.Clients.Session;
    using AzSentinel.Models.PowerShell;

    [Cmdlet(VerbsCommunications.Connect, "Sentinel")]
    public class ConnectSentinel : AzSentinelCmdlet
    {
        protected override void ProcessRecord()
        {

            ModuleConfig moduleConfig = GetModuleConfig();

            IPublicClientApplication app = null;

            try
            {
                app = GetSessionClient().App;
                WriteVerbose("PublicClient already in memory.");
            }
            catch
            {
                WriteVerbose("Building PublicClient app object.");
                app = PublicClientApplicationBuilder.Create(moduleConfig.PublicClientId)
                    .WithAuthority($"https://login.microsoftonline.com/{moduleConfig.TenantId}")
                    .WithDefaultRedirectUri()
                    .Build();
            }

            PublicAuthenticationHelper TokenFlow = new PublicAuthenticationHelper(app);

            string[] scopes = new string[] {
                "https://management.azure.com/user_impersonation"
                };

            AuthenticationResult result = null;
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

            try
            {
                result = TokenFlow.StartAcquire(scopes).GetAwaiter().GetResult();
            }
            catch (System.Exception e)
            {
                throw e;
            }
            cancellationTokenSource = null;

            SessionClient sessionClient = new SessionClient(new Uri("https://management.azure.com/"), result, app);

            SessionState.PSVariable.Set(new PSVariable("AzSentinelSessionClient", sessionClient, ScopedItemOptions.Private));
            SetModuleConfigSessionVariable(moduleConfig);
            WriteObject("You are now connected to Azure Sentinel.");
        }

    }
}