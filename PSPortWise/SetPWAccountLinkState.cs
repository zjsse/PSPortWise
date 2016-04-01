using System;
using System.Management.Automation;

namespace PSPortWise
{
    [Cmdlet(VerbsCommon.Set, "PWAccountLinkState", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.Medium)]
    public class SetPWAccountLinkStatus : PWUAccountCmdlet
    {
        [Parameter(Mandatory = true, ParameterSetName = "Link")]
        public SwitchParameter Link { get; set; }
        [Parameter(Mandatory = true, ParameterSetName = "Unlink")]
        public SwitchParameter Unlink { get; set; }
        [Parameter(ParameterSetName = "Link")]
        public SwitchParameter Force { get; set; }

        protected override void ProcessRecord()
        {
            base.ProcessRecord();
            if (ShouldProcess(Identity.ToString()))
            {
                if (Link)
                {
                    try
                    {
                        UserSessionState.UserAccountService.link(UserSessionState.AuthenticatedSubject, Identity.UserName, Force);
                    }
                    catch (Exception e)
                    {
                        WriteError(new ErrorRecord(e, "LinkAccountError", ErrorCategory.NotSpecified, Identity));
                    }
                }
                else
                {
                    try
                    {
                        UserSessionState.UserAccountService.unlink(UserSessionState.AuthenticatedSubject, Identity.UserName);
                    }
                    catch (Exception e)
                    {
                        WriteError(new ErrorRecord(e, "UnlinkAccountError", ErrorCategory.NotSpecified, Identity));
                    }
                }
            }
        }
    }
}
