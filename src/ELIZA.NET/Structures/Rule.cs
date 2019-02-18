using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ELIZA.NET.Structures
{
    [Serializable]
    public class Rule
    {
        [JsonProperty("decomposition")]
        public string Decomposition = null;

        [JsonProperty("reassembly")]
        public List<string> Reassembly
        {
            get;
            private set;
        }

        [JsonProperty("memorize")]
        public bool Memorize = false;  // TODO - Implement this feature.  --Kris

        public Rule(string decomposition, string reassembly, bool memorize = false)
        {
            Decomposition = decomposition;
            Reassembly = JsonConvert.DeserializeObject<List<string>>(reassembly);
            Memorize = memorize;
        }

        public Rule(string decomposition, List<string> reassembly, bool memorize = false)
        {
            Decomposition = decomposition;
            Reassembly = reassembly;
            Memorize = memorize;
        }

        public Rule() { Reassembly = null; }

        public void SetReassembly(string reassembly)
        {
            Reassembly = JsonConvert.DeserializeObject<List<string>>(reassembly);
        }

        public void SetReassembly(List<string> reassembly)
        {
            Reassembly = reassembly;
        }
    }
}
