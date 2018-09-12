using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;

namespace ELIZA.NET.Structures
{
    [Serializable]
    public class Rule
    {
        private string Decomposition = null;
        private List<string> Reassembly = null;
        private bool Memorize = false;  // TODO - Implement this feature.  --Kris

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

        public Rule() { }

        public string GetDecomposition()
        {
            return Decomposition;
        }

        public void SetDecomposition(string decomposition)
        {
            this.Decomposition = decomposition;
        }

        public List<string> GetReassembly()
        {
            return Reassembly;
        }

        public void SetReassembly(string reassembly)
        {
            this.Reassembly = JsonConvert.DeserializeObject<List<string>>(reassembly);
        }

        public void SetReassembly(List<string> reassembly)
        {
            this.Reassembly = reassembly;
        }

        public bool GetMemorize()
        {
            return Memorize;
        }

        public void SetMemorize(bool memorize)
        {
            this.Memorize = memorize;
        }
    }
}
