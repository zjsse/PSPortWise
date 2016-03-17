using System;
using System.Management.Automation;
using PortWiseWS;

namespace PSPortWise
{
    public class PWConnectedCmdlet : PSCmdlet
    {
        protected override void BeginProcessing()
        {
            base.BeginProcessing();
            if (!UserSessionState.IsAuthenticated)
            {
                ThrowTerminatingError(
                    new ErrorRecord(
                        new Exception("Not connected to PortWise Web Service (XPI). Use Connect-PWPPolicyService to connect."),
                        "AuthenticationError",
                        ErrorCategory.AuthenticationError,
                        null
                    )
                );
            }
        }
    }

    public class PWUAccountCmdlet : PWConnectedCmdlet
    {
        [ValidateNotNullOrEmpty]
        [Parameter(Mandatory = true, ValueFromPipeline = true, Position = 0)]
        public PWAccount Identity { get; set; }
    }

}
