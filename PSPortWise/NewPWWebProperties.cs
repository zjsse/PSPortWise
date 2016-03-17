using System.Management.Automation;
using PortWiseWS;

namespace PSPortWise
{
    [Cmdlet(VerbsCommon.New, "PWWebProperties")]
    public class NewPWWebProperties : PSCmdlet
    {
        [Parameter()]
        public bool Enabled { get; set; } = true;
        [Parameter()]
        public bool GeneratePassword { get; set; } = false;
        [Parameter()]
        public string Password { get; set; } = null;
        [Parameter()]
        public bool PasswordNeverExpires { get; set; } = false;
        [Parameter()]
        public bool UserCannotChangePassword { get; set; } = false;
        [Parameter()]
        public bool UserMustChangePassword { get; set; } = false;

        protected override void ProcessRecord()
        {
            WriteObject(new WebProperties()
            {
                enabled = Enabled,
                generatePwd = GeneratePassword,
                password = Password,
                pwdNeverExpires = PasswordNeverExpires,
                userCannotChangePwd = UserCannotChangePassword,
                userMustChangePwd = UserMustChangePassword
            });
        }
    }
}
