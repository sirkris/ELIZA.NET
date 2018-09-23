using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq;
using System.Text;

namespace ELIZA.NET.Structures
{
    [Serializable]
    public class Transformation
    {
        [JsonProperty("word")]
        public string Word = null;

        [JsonProperty("script")]
        public string Script = null;

        [JsonProperty("aliases")]
        private string AliasesStr = null;

        private List<string> Aliases = null;

        public Transformation(string word, string script, string aliases)
        {
            this.Word = word;
            this.Script = script;
            this.Aliases = aliases.Split(',').ToList();
        }

        public Transformation(string word, string script, List<string> aliases)
        {
            this.Word = word;
            this.Script = script;
            this.Aliases = aliases;
        }

        public Transformation() { }

        public List<string> GetAliases()
        {
            if (AliasesStr != null)
            {
                SetAliases(AliasesStr);
                this.AliasesStr = null;
            }

            return Aliases;
        }

        public void SetAliases(string aliases)
        {
            this.Aliases = aliases.Split(',').ToList();
        }

        public void SetAliases(List<string> aliases)
        {
            this.Aliases = aliases;
        }
    }
}
