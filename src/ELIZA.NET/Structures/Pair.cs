using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Text;

namespace ELIZA.NET.Structures
{
    [Serializable]
    public class Pair
    {
        [JsonProperty("word")]
        public string Word = null;

        [JsonProperty("script")]
        public string Script = null;

        [JsonProperty("inverse")]
        public string Inverse = null;

        [JsonProperty("bidirectional")]
        public bool Bidirectional = false;

        public Pair(string word, string script, string inverse, bool bidirectional = false)
        {
            this.Word = word;
            this.Script = script;
            this.Inverse = inverse;
            this.Bidirectional = bidirectional;
        }

        public Pair() { }
    }
}
