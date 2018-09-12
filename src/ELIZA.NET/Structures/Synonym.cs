using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELIZA.NET.Structures
{
    public class Synonym
    {
        private string word = null;
        private string script = null;
        private List<string> aliases = null;

        public Synonym(string word, string script, string aliases)
        {
            this.word = word;
            this.script = script;
            this.aliases = aliases.Split(',').ToList();
        }

        public Synonym(string word, string script, List<string> aliases)
        {
            this.word = word;
            this.script = script;
            this.aliases = aliases;
        }

        public Synonym() { }

        public string GetWord()
        {
            return word;
        }

        public void SetWord(string word)
        {
            this.word = word;
        }

        public string GetScript()
        {
            return script;
        }

        public void SetScript(string script)
        {
            this.script = script;
        }

        public List<string> GetAliases()
        {
            return aliases;
        }

        public void SetAliases(string aliases)
        {
            this.aliases = aliases.Split(',').ToList();
        }

        public void SetAliases(List<string> aliases)
        {
            this.aliases = aliases;
        }
    }
}
