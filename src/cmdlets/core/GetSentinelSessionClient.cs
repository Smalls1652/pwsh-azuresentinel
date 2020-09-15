using System;
using System.Management.Automation;

namespace AzSentinel.PowerShell
{
    using AzSentinel.Clients.Session;

    [Cmdlet(VerbsCommon.Get, "SentinelSessionClient")]
    public class GetSentinelSessionClient : AzSentinelCmdlet
    {
        protected override void ProcessRecord()
        {
            SessionClient sessionClient = GetSessionClient();

            WriteObject(sessionClient);
        }
    }
}