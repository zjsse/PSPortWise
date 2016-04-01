using System.Management.Automation;
using PortWiseWS;

namespace PSPortWise
{
    [Cmdlet(VerbsCommon.New, "PWPasswordProperties")]
    public class NewPWPasswordProperties : PSCmdlet
    {
        [Parameter]
        public bool Enabled { get; set; } = true;
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
            WriteObject(new PasswordProperties
            {
                enabled = Enabled,
                generatePwd = GeneratePassword,
                password = Password,
                pwdNeverExpires = PasswordNeverExpires,
                useDirectoryPwd = UseDirectoryPassword,
                userCannotChangePwd = UserCannotChangePassword,
                userMustChangePwd = UserMustChangePassword
            });
        }
    }
}
