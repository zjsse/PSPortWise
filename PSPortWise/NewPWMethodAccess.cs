using System.Management.Automation;
using PortWiseWS;

namespace PSPortWise
{
    [Cmdlet(VerbsCommon.New, "PWMethodAccess")]
    public class NewPWMethodAccess : PSCmdlet
    {
        [Parameter()]
        public int MaxRetries = 10;
        [Parameter()]
        public string RadiusFramedIP;
        [Parameter()]
        public string RadiusGroup;
        [Parameter()]
        public ChallengeProperties ChallengeProperties;
        [Parameter()]
        public InvisibleTokenProperties InvisibleTokenProperties;
        [Parameter()]
        public MobileTextProperties MobileTextProperties;
        [Parameter()]
        public PasswordProperties PasswordProperties;
        [Parameter()]
        public SynchronizedProperties SynchronizedProperties;
        [Parameter()]
        public WebProperties WebProperties;

        protected override void ProcessRecord()
        {
            WriteObject(new MethodAccess()
            {
                maxRetries = MaxRetries,
                radiusFramedIP = RadiusFramedIP,
                radiusGroup = RadiusGroup,
                challengeProps = ChallengeProperties,
                invisibleTokenProps = InvisibleTokenProperties,
                mobileTextProps = MobileTextProperties,
                passwordProps = PasswordProperties,
                synchronizedProps = SynchronizedProperties,
                webProps = WebProperties
            });
        }
    }
}
