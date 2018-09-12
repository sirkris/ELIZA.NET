using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELIZA.NET.Structures
{
    [Serializable]
    public class Greeting
    {
        private string Greeting = null;
        private string Script = null;

        public Greeting(string greeting, string script)
        {
            this.Greeting = greeting;
            this.Script = script;
        }

        public Greeting() { }

        public string GetGreeting()
        {
            return Greeting;
        }

        public void SetGreeting(string greeting)
        {
            this.Greeting = greeting;
        }

        public string GetScript()
        {
            return Script;
        }

        public void SetScript(string script)
        {
            this.Script = script;
        }
    }
}
