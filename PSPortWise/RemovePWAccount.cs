using System;
using System.Management.Automation;

namespace PSPortWise
{
    [Cmdlet(VerbsCommon.Remove, "PWAccount", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
    public class RemovePWAccount : PWUAccountCmdlet
    {
        protected override void ProcessRecord()
        {
            base.ProcessRecord();
            if (ShouldProcess(Identity.ToString()))
            {
                try
                {
                    UserSessionState.UserAccountService.remove(UserSessionState.AuthenticatedSubject, Identity.UserName);
                }
                catch (Exception e)
                {
                    WriteError(new ErrorRecord(e, "DisableAccountError", ErrorCategory.NotSpecified, Identity));
                }
            }
        }
    }
}
