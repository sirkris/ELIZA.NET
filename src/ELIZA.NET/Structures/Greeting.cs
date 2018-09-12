using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELIZA.NET.Structures
{
    public class Greeting
    {
        private string greeting = null;
        private string script = null;

        public Greeting(string greeting, string script)
        {
            this.greeting = greeting;
            this.script = script;
        }

        public Greeting() { }

        public string GetGreeting()
        {
            return greeting;
        }

        public void SetGreeting(string greeting)
        {
            this.greeting = greeting;
        }

        public string GetScript()
        {
            return script;
        }

        public void SetScript(string script)
        {
            this.script = script;
        }
    }
}
