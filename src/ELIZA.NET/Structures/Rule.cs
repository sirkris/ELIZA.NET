using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Text;

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
            this.Decomposition = decomposition;
            this.Reassembly = JsonConvert.DeserializeObject<List<string>>(reassembly);
            this.Memorize = memorize;
        }

        public Rule(string decomposition, List<string> reassembly, bool memorize = false)
        {
            this.Decomposition = decomposition;
            this.Reassembly = reassembly;
            this.Memorize = memorize;
        }

        public Rule() { this.Reassembly = null; }

        public void SetReassembly(string reassembly)
        {
            this.Reassembly = JsonConvert.DeserializeObject<List<string>>(reassembly);
        }

        public void SetReassembly(List<string> reassembly)
        {
            this.Reassembly = reassembly;
        }
    }
}
