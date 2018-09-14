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
        private string GreetingText = null;

        [JsonProperty("script")]
        private string Script = null;

        public Greeting(string greeting, string script)
        {
            this.GreetingText = greeting;
            this.Script = script;
        }

        public Greeting() { }

        public string GetGreeting()
        {
            return GreetingText;
        }

        public void SetGreeting(string greeting)
        {
            this.GreetingText = greeting;
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
            return GreetingText;
        }
    }
}
