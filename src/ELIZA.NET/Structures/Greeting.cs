using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;

namespace ELIZA.NET.Structures
{
    [Serializable]
    public class Greeting
    {
        [JsonProperty("greeting")]
        public string GreetingText = null;

        [JsonProperty("script")]
        public string Script = null;

        public Greeting(string greeting, string script)
        {
            this.GreetingText = greeting;
            this.Script = script;
        }

        public Greeting() { }

        public override string ToString()
        {
            return GreetingText;
        }
    }
}
