using System;
using System.Management.Automation;
using PortWiseWS;

namespace PSPortWise
{
    [Cmdlet(VerbsCommon.Reset, "PWAccount", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.Medium)]
    public class ResetPWAccount : PWUAccountCmdlet
    {
        [Parameter]
        public PortWiseWS.AccountType AccountType { get; set; } = AccountType.PolicyService;

        protected override void ProcessRecord()
        {
            base.ProcessRecord();
            if (ShouldProcess(Identity.ToString()))
            {
                try
                {
                    UserSessionState.UserAccountService.reset(UserSessionState.AuthenticatedSubject, Identity.UserName, (int)AccountType);
                }
                catch (Exception e)
                {
                    WriteError(new ErrorRecord(e, "ResetAccountError", ErrorCategory.NotSpecified, Identity));
                }
            }
        }
    }
}
