using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELIZA.NET.Structures
{
    [Serializable]
    public class Transformation
    {
        private string Word = null;
        private string Script = null;
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

        public List<string> GetAliases()
        {
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
