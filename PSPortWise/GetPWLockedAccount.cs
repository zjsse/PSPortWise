using System;
using System.Management.Automation;
using PortWiseWS;

namespace PSPortWise
{
    [Cmdlet(VerbsCommon.Get, "PWLockedAccount")]
    public class GetPWLockedAccount : PWConnectedCmdlet
    {
        [Parameter]
        public PortWiseWS.AccountType AccountType { get; set; } = AccountType.PolicyService;

        protected override void ProcessRecord()
        {
            base.ProcessRecord();
            string[] lockedAccounts;
            try
            {
                lockedAccounts = UserSessionState.UserAccountService.getLocked(UserSessionState.AuthenticatedSubject, (int)AccountType);
            }
            catch (Exception e)
            {
                WriteError(new ErrorRecord(e, "GetLockedAccountError", ErrorCategory.NotSpecified, null));
                return;
            }
            foreach (string userName in lockedAccounts)
            {
                try
                {
                    Account account = UserSessionState.UserAccountService.getAccount(UserSessionState.AuthenticatedSubject, userName);
                    WriteObject(new PWAccount(account));
                }
                catch (Exception e)
                {
                    WriteError(new ErrorRecord(e, "GetAccountError", ErrorCategory.NotSpecified, userName));
                }
            }
        }
    }
}
