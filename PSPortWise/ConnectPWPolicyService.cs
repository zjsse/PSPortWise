using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management.Automation;
using PortWiseWS;

namespace PSPortWise
{
    [Cmdlet(VerbsCommunications.Connect, "PWPolicyService")]
    public class ConnectPWPolicyService : PSCmdlet
    {
        [Parameter(Mandatory = true, Position = 0)]
        public string Url { get; set; }

        [Parameter(Mandatory = true, Position = 1)]
        public int PortWisePasswordId { get; set; }

        [Parameter(Mandatory = true, Position = 2)]
        public PSCredential Credential { get; set; }

        protected override void BeginProcessing()
        {
            base.BeginProcessing();
            AuthenticateService authenticationService = new AuthenticateService(Url);
            Subject subject = new Subject()
            {
                credentials = new MapItem[2] {
                    new MapItem() {
                        key = "username",
                        value = UserSessionState.Encoding.GetBytes(Credential.UserName)
                    },
                    new MapItem() {
                        key = "password",
                        value = UserSessionState.Encoding.GetBytes(Credential.GetNetworkCredential().Password)
                    }
                }
            };
            UserSessionState.AuthenticatedSubject = authenticationService.authenticate(subject, PortWisePasswordId);
            UserSessionState.PolicyServiceUrl = Url;
            UserSessionState.UserAccountService = new UserAccountService(Url);
        }
    }
}
