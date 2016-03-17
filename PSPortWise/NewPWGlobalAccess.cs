using System.Management.Automation;
using PortWiseWS;

namespace PSPortWise
{
    [Cmdlet(VerbsCommon.New, "PWGlobalAccess")]
    public class NewPWGlobalAccess : PSCmdlet
    {
        [Parameter()]
        public int MaxRetries { get; set; } = 10;

        protected override void ProcessRecord()
        {
            WriteObject(new GlobalAccess()
            {
                maxRetries = MaxRetries
            });
        }
    }
}
