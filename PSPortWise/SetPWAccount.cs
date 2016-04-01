using System;
using System.Management.Automation;
using PortWiseWS;

namespace PSPortWise
{
    [Cmdlet(VerbsCommon.Set, "PWAccount", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.Medium)]
    public class SetPWAccount : PWUAccountCmdlet
    {
        [Parameter(ValueFromPipelineByPropertyName = true)]
        public string DisplayName { get; set; }

        [Parameter(ValueFromPipelineByPropertyName = true)]
        public string EmailAddress { get; set; }

        [Parameter(ValueFromPipelineByPropertyName = true)]
        public bool Enabled { get; set; }

        [Parameter(ValueFromPipelineByPropertyName = true)]
        public GlobalAccess GlobalAccess { get; set; }

        [Parameter(ValueFromPipelineByPropertyName = true)]
        public string SmsNumber { get; set; }

        [Parameter(ValueFromPipelineByPropertyName = true)]
        public string UserName { get; set; }

        [Parameter(ValueFromPipelineByPropertyName = true)]
        public DateTime ValidFrom { get; set; }

        [Parameter(ValueFromPipelineByPropertyName = true)]
        public DateTime ValidTo { get; set; }

        [Parameter(ValueFromPipelineByPropertyName = true)]
        public MethodAccess MethodAccess { get; set; }

        [Parameter(ValueFromPipelineByPropertyName = true)]
        public MapItem[] CustomAttributes { get; set; }

        [Parameter(ValueFromPipelineByPropertyName = true)]
        public MapItem[] NotificationMappings { get; set; }


        protected override void ProcessRecord()
        {
            base.ProcessRecord();
            PWAccount pwAccount;
            if (Identity.IsDisconnected)
            {
                try
                {
                    Account account = UserSessionState.UserAccountService.getAccount(UserSessionState.AuthenticatedSubject, Identity.UserName);
                    pwAccount = new PWAccount(account);
                }
                catch (Exception e)
                {
                    WriteError(new ErrorRecord(e, "GetAccountError", ErrorCategory.NotSpecified, Identity));
                    return;
                }
            }
            else
            {
                pwAccount = Identity;
            }
            if (MyInvocation.BoundParameters.ContainsKey("DisplayName"))
            {
                pwAccount.DisplayName = DisplayName;
            }
            if (MyInvocation.BoundParameters.ContainsKey("EmailAddress"))
            {
                pwAccount.EmailAddress = EmailAddress;
            }
            if (MyInvocation.BoundParameters.ContainsKey("Enabled"))
            {
                pwAccount.Enabled = Enabled;
            }
            if (MyInvocation.BoundParameters.ContainsKey("GlobalAccess"))
            {
                pwAccount.GlobalAccess = GlobalAccess;
            }
            if (MyInvocation.BoundParameters.ContainsKey("SmsNumber"))
            {
                pwAccount.SmsNumber = SmsNumber;
            }
            if (MyInvocation.BoundParameters.ContainsKey("UserName"))
            {
                pwAccount.UserName = UserName;
            }
            if (MyInvocation.BoundParameters.ContainsKey("ValidFrom"))
            {
                pwAccount.ValidFrom = ValidFrom;
            }
            if (MyInvocation.BoundParameters.ContainsKey("ValidTo"))
            {
                pwAccount.ValidTo = ValidTo;
            }
            if (MyInvocation.BoundParameters.ContainsKey("MethodAccess"))
            {
                pwAccount.MethodAccess = MethodAccess;
            }
            if (MyInvocation.BoundParameters.ContainsKey("CustomAttributes"))
            {
                pwAccount.CustomAttributes = CustomAttributes;
            }
            if (MyInvocation.BoundParameters.ContainsKey("NotificationMappings"))
            {
                pwAccount.NotificationMappings = NotificationMappings;
            }
            if (ShouldProcess(Identity.ToString()))
            {
                try
                {
                    UserSessionState.UserAccountService.update(UserSessionState.AuthenticatedSubject, pwAccount.Account);
                }
                catch (Exception e)
                {
                    WriteError(new ErrorRecord(e, "UpdateAccountError", ErrorCategory.NotSpecified, Identity));
                }
            }
        }
    }
}
