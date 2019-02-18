using System;
using Newtonsoft.Json;

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
            Word = word;
            Script = script;
            Inverse = inverse;
            Bidirectional = bidirectional;
        }

        public Pair() { }
    }
}
