using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;

namespace ELIZA.NET.Structures
{
    [Serializable]
    public class Pair
    {
        [JsonProperty("word")]
        private string Word = null;

        [JsonProperty("script")]
        private string Script = null;

        [JsonProperty("inverse")]
        private string Inverse = null;

        [JsonProperty("bidirectional")]
        private bool Bidirectional = false;

        public Pair(string word, string script, string inverse, bool bidirectional = false)
        {
            this.Word = word;
            this.Script = script;
            this.Inverse = inverse;
            this.Bidirectional = bidirectional;
        }

        public Pair() { }

        public string GetWord()
        {
            return Word;
        }

        public void SetWord(string word)
        {
            this.Word = word;
        }

        public string GetScript()
        {
            return Script;
        }

        public void SetScript(string script)
        {
            this.Script = script;
        }

        public string GetInverse()
        {
            return Inverse;
        }

        public void SetInverse(string inverse)
        {
            this.Inverse = inverse;
        }

        public bool GetBidirectional()
        {
            return Bidirectional;
        }

        public void SetBidirectional(bool bidirectional)
        {
            this.Bidirectional = bidirectional;
        }
    }
}
