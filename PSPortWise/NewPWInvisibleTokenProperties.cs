using System.Management.Automation;
using PortWiseWS;

namespace PSPortWise
{
    [Cmdlet(VerbsCommon.New, "PWInvisibleTokenProperties")]
    public class NewPWInvisibleTokenProperties : PSCmdlet
    {
        [Parameter]
        public bool Enabled { get; set; } = true;
        [Parameter]
        public bool ActivateBrowserNextLogon;
        [Parameter]
        public bool GeneratePassword { get; set; } = false;
        [Parameter]
        public string Password { get; set; } = null;
        [Parameter]
        public bool PasswordNeverExpires { get; set; } = false;
        [Parameter]
        public bool UseDirectoryPassword { get; set; } = false;
        [Parameter]
        public bool UserCannotChangePassword { get; set; } = false;
        [Parameter]
        public bool UserMustChangePassword { get; set; } = false;

        protected override void ProcessRecord()
        {
            WriteObject(new InvisibleTokenProperties
            {
                enabled = Enabled,
                activateBrowserNextLogon = ActivateBrowserNextLogon,
                generatePwd = GeneratePassword,
                password = Password,
                pwdNeverExpires = PasswordNeverExpires,
                useDirectoryPwd = UseDirectoryPassword,
                userCannotChangePwd = UserCannotChangePassword,
                userMustChangePwd = UserMustChangePassword,
                invisibleTokens = null
            });
        }
    }
}
