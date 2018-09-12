using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELIZA.NET.Structures
{
    [Serializable]
    public class Goodbye
    {
        private string Message = null;
        private string Script = null;

        public Goodbye(string message, string script)
        {
            this.Message = message;
            this.Script = script;
        }

        public Goodbye() { }

        public string GetMessage()
        {
            return Message;
        }

        public void SetMessage(string message)
        {
            this.Message = message;
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
