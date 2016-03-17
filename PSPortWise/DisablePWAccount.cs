using System;
using System.Management.Automation;
using PortWiseWS;

namespace PSPortWise
{
    [Cmdlet(VerbsLifecycle.Disable, "PWAccount", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.Medium)]
    public class DisablePWAccount : PWUAccountCmdlet
    {
        protected override void ProcessRecord()
        {
            base.ProcessRecord();
            if (ShouldProcess(Identity.ToString()))
            {
                try
                {
                    UserSessionState.UserAccountService.enable(UserSessionState.AuthenticatedSubject, Identity.UserName, false);
                }
                catch (Exception e)
                {
                    WriteError(new ErrorRecord(e, "DisableAccountError", ErrorCategory.NotSpecified, Identity));
                }
            }
        }
    }
}
