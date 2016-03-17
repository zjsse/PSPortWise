using System;
using System.Management.Automation;
using PortWiseWS;

namespace PSPortWise
{
    [Cmdlet(VerbsLifecycle.Enable, "PWAccount", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.Medium)]
    public class EnablePWAccount : PWUAccountCmdlet
    {
        protected override void ProcessRecord()
        {
            base.ProcessRecord();
            if (ShouldProcess(Identity.ToString()))
            {
                try
                {
                    UserSessionState.UserAccountService.enable(UserSessionState.AuthenticatedSubject, Identity.UserName, true);
                }
                catch (Exception e)
                {
                    WriteError(new ErrorRecord(e, "EnableAccountError", ErrorCategory.NotSpecified, Identity));
                }
            }
        }
    }
}
