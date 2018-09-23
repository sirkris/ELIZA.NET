using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Text;

namespace ELIZA.NET.Structures
{
    [Serializable]
    public class Keyword
    {
        [JsonProperty("word")]
        public string Word = null;

        [JsonProperty("script")]
        public string Script = null;

        [JsonProperty("rank")]
        public int Rank = 0;

        [JsonProperty("rules")]
        public List<Rule> Rules = null;

        public Keyword(string word, string script, int rank, List<Rule> rules)
        {
            this.Word = word;
            this.Script = script;
            this.Rank = rank;
            this.Rules = rules;
        }

        public Keyword() { }
    }
}
