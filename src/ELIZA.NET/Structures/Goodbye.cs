using System;
using Newtonsoft.Json;

namespace ELIZA.NET.Structures
{
    [Serializable]
    public class Goodbye
    {
        [JsonProperty("message")]
        public string Message = null;

        [JsonProperty("script")]
        public string Script = null;

        public Goodbye(string message, string script)
        {
            Message = message;
            Script = script;
        }

        public Goodbye() { }

        public override string ToString()
        {
            return Message;
        }
    }
}
