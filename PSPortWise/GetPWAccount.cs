using System;
using System.Management.Automation;
using PortWiseWS;

namespace PSPortWise
{
    [Cmdlet(VerbsCommon.Get, "PWAccount")]
    public class GetPWAccount : PWUAccountCmdlet
    {
        protected override void ProcessRecord()
        {
            base.ProcessRecord();
            try
            {
                Account account = UserSessionState.UserAccountService.getAccount(UserSessionState.AuthenticatedSubject, Identity.UserName);
                WriteObject(new PWAccount(account));
            }
            catch (Exception e)
            {
                WriteError(new ErrorRecord(e, "GetAccountError", ErrorCategory.NotSpecified, Identity));
            }
        }
    }
}
