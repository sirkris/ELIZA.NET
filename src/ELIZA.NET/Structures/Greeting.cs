using System;
using Newtonsoft.Json;

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
            GreetingText = greeting;
            Script = script;
        }

        public Greeting() { }

        public override string ToString()
        {
            return GreetingText;
        }
    }
}
