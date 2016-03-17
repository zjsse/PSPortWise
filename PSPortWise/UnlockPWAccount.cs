using System;
using System.Management.Automation;
using PortWiseWS;

namespace PSPortWise
{
    [Cmdlet(VerbsCommon.Unlock, "PWAccount", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.Medium)]
    public class UnlockPWAccount : PWUAccountCmdlet
    {
        [Parameter()]
        public PortWiseWS.AccountType AccountType { get; set; } = AccountType.PolicyService;

        protected override void ProcessRecord()
        {
            base.ProcessRecord();
            if (ShouldProcess(Identity.ToString()))
            {
                try
                {
                    UserSessionState.UserAccountService.unlock(UserSessionState.AuthenticatedSubject, Identity.UserName, (int)AccountType);
                }
                catch (Exception e)
                {
                    WriteError(new ErrorRecord(e, "UnlockAccountError", ErrorCategory.NotSpecified, Identity));
                }
            }
        }
    }
}
