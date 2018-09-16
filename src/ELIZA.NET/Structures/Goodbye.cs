using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;

namespace ELIZA.NET.Structures
{
    [Serializable]
    public class Goodbye
    {
        [JsonProperty("message")]
        private string Message = null;

        [JsonProperty("script")]
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

        public string ToString()
        {
            return Message;
        }
    }
}
