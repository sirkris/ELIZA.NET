using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELIZA.NET.Structures
{
    public class Goodbye
    {
        private string message = null;
        private string script = null;

        public Goodbye(string message, string script)
        {
            this.message = message;
            this.script = script;
        }

        public Goodbye() { }

        public string GetMessage()
        {
            return message;
        }

        public void SetMessage(string message)
        {
            this.message = message;
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
