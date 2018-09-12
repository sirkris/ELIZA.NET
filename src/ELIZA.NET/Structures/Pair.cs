using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELIZA.NET.Structures
{
    public class Pair
    {
        private string word = null;
        private string script = null;
        private string inverse = null;
        private bool bidirectional = false;

        public Pair(string word, string script, string inverse, bool bidirectional = false)
        {
            this.word = word;
            this.script = script;
            this.inverse = inverse;
            this.bidirectional = bidirectional;
        }

        public Pair() { }

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

        public string GetInverse()
        {
            return inverse;
        }

        public void SetInverse(string inverse)
        {
            this.inverse = inverse;
        }

        public bool GetBidirectional()
        {
            return bidirectional;
        }

        public void SetBidirectional(bool bidirectional)
        {
            this.bidirectional = bidirectional;
        }
    }
}
