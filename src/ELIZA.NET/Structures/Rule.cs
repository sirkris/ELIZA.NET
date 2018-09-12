using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;

namespace ELIZA.NET.Structures
{
    public class Rule
    {
        private string decomposition = null;
        private List<string> reassembly = null;
        private bool memorize = false;  // TODO - Implement this feature.  --Kris

        public Rule(string decomposition, string reassembly, bool memorize = false)
        {
            this.decomposition = decomposition;
            this.reassembly = JsonConvert.DeserializeObject<List<string>>(reassembly);
            this.memorize = memorize;
        }

        public Rule(string decomposition, List<string> reassembly, bool memorize = false)
        {
            this.decomposition = decomposition;
            this.reassembly = reassembly;
            this.memorize = memorize;
        }

        public Rule() { }

        public string GetDecomposition()
        {
            return decomposition;
        }

        public void SetDecomposition(string decomposition)
        {
            this.decomposition = decomposition;
        }

        public List<string> GetReassembly()
        {
            return reassembly;
        }

        public void SetReassembly(string reassembly)
        {
            this.reassembly = JsonConvert.DeserializeObject<List<string>>(reassembly);
        }

        public void SetReassembly(List<string> reassembly)
        {
            this.reassembly = reassembly;
        }

        public bool GetMemorize()
        {
            return memorize;
        }

        public void SetMemorize(bool memorize)
        {
            this.memorize = memorize;
        }
    }
}
