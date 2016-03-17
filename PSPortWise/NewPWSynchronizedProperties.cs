using System.Management.Automation;
using PortWiseWS;

namespace PSPortWise
{
    [Cmdlet(VerbsCommon.New, "PWSynchronizedProperties")]
    public class NewPWSynchronizedProperties : PSCmdlet
    {
        [Parameter()]
        public bool Enabled { get; set; } = true;
        [Parameter()]
        public bool GeneratePin { get; set; } = false;
        [Parameter()]
        public bool GenerateSeed { get; set; } = false;
        [Parameter()]
        public string Pin { get; set; } = null;
        [Parameter()]
        public bool PinNeverExpires { get; set; } = false;
        [Parameter()]
        public int SeedingMethod { get; set; } = 0;
        [Parameter()]
        public bool UserCannotChangePin { get; set; } = false;
        [Parameter()]
        public bool UserMustChangePin { get; set; } = false;

        protected override void ProcessRecord()
        {
            WriteObject(new SynchronizedProperties()
            {
                enabled = Enabled,
                generatePin = GeneratePin,
                generateSeed = GenerateSeed,
                pin = Pin,
                pinNeverExpires = PinNeverExpires,
                seedingMethod = SeedingMethod,
                userCannotChangePin = UserCannotChangePin,
                userMustChangePin = UserMustChangePin
            });
        }
    }
}
