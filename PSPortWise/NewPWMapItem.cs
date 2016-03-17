using System;
using System.Collections;
using System.Collections.Generic;
using System.Management.Automation;
using PortWiseWS;

namespace PSPortWise
{
    [Cmdlet(VerbsCommon.New, "PWMapItem")]
    public class NewPWMapItem : PSCmdlet
    {
        [Parameter(ValueFromPipeline = true)]
        public MapItem[] MapItem { get; set; }

        [ValidateNotNull]
        [Parameter(Mandatory = true, Position = 0, ParameterSetName = "KeyAndValue")]
        public string[] Keys { get; set; }
        [Parameter(Mandatory = true, Position = 1, ParameterSetName = "KeyAndValue")]
        public string[] Values { get; set; }

        [ValidateNotNull]
        [Parameter(Mandatory = true, Position = 0, ParameterSetName = "HashTable")]
        public Hashtable Hashtable { get; set; }

        // Collects MapItem objects during processing. This is the only way I could figure
        // out to get a MapItem array as output and not a generic object array.
        private List<MapItem> result;

        protected override void BeginProcessing()
        {
            result = new List<MapItem>();
            if (Keys != null && Keys.Length > 0)
            {
                if (Keys.Length != Values.Length)
                {
                    WriteError(
                        new ErrorRecord(
                            new Exception("The number of Keys and Values must be equal."),
                            "NewMapItemError",
                            ErrorCategory.NotSpecified,
                            null
                         )
                    );
                }
                else
                {
                    for (int i = 0; i < Keys.Length; i++)
                    {
                        result.Add(new MapItem()
                        {
                            key = Keys[i],
                            value = UserSessionState.Encoding.GetBytes(Values[i])
                        });
                    }
                }
            }
            if (Hashtable != null)
            {
                foreach (DictionaryEntry entry in Hashtable)
                {
                    result.Add(new MapItem()
                    {
                        key = entry.Key.ToString(),
                        value = UserSessionState.Encoding.GetBytes(entry.Value.ToString())
                    });
                }
            }
        }

        protected override void ProcessRecord()
        {
            if (MapItem != null)
            {
                result.AddRange(MapItem);
            }
        }

        protected override void EndProcessing()
        {
            WriteObject(result.ToArray());
        }
    }
}
