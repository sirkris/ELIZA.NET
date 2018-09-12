using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELIZA.NET.Structures
{
    public class Keyword
    {
        private string word = null;
        private string script = null;
        private int rank = 0;
        private List<Rule> rules = null;

        public Keyword(string word, string script, int rank, List<Rule> rules)
        {
            this.word = word;
            this.script = script;
            this.rank = rank;
            this.rules = rules;
        }

        public Keyword() { }

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

        public int GetRank()
        {
            return rank;
        }

        public void SetRank(int rank)
        {
            this.rank = rank;
        }

        public List<Rule> GetRules()
        {
            return rules;
        }

        public void SetRules(List<Rule> rules)
        {
            this.rules = rules;
        }
    }
}
