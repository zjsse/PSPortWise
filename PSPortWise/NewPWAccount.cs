using System;
using System.Management.Automation;
using PortWiseWS;

namespace PSPortWise
{
    [Cmdlet(VerbsCommon.New, "PWAccount", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.Medium)]
    public class NewPWAccount : PWConnectedCmdlet
    {
        [ValidateNotNullOrEmpty]
        [Parameter(Mandatory = true, ValueFromPipelineByPropertyName = true)]
        public string UserName { get; set; }

        [ValidateNotNullOrEmpty]
        [Parameter(Mandatory = true, ValueFromPipelineByPropertyName = true)]
        public string DisplayName { get; set; }

        [Parameter(ValueFromPipelineByPropertyName = true)]
        public string EmailAddress { get; set; }

        [Parameter(ValueFromPipelineByPropertyName = true)]
        public SwitchParameter Linked { get; set; } = false;

        [Parameter(ValueFromPipelineByPropertyName = true)]
        public bool Enabled { get; set; }

        [Parameter(ValueFromPipelineByPropertyName = true)]
        public GlobalAccess GlobalAccess { get; set; }

        [Parameter(ValueFromPipelineByPropertyName = true)]
        public string SmsNumber { get; set; }

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
            var pwAccount = new PWAccount();
            pwAccount.UserName = UserName;
            pwAccount.DisplayName = DisplayName;
            // Set defaults
            pwAccount.NotificationMappings = new MapItem[0];
            pwAccount.GlobalAccess = new GlobalAccess();
            pwAccount.GlobalAccess.maxRetries = 10;
            pwAccount.Enabled = true;
            pwAccount.ValidFrom = DateTime.Today;
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
            if (ShouldProcess(UserName))
            {
                try
                {
                    UserSessionState.UserAccountService.add(UserSessionState.AuthenticatedSubject, pwAccount.Account, Linked);
                }
                catch (Exception e)
                {
                    WriteError(new ErrorRecord(e, "NewAccountError", ErrorCategory.NotSpecified, pwAccount));
                }
            }
        }
    }
}
