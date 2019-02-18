using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace RunTests.Structures
{
    [Serializable]
    class TestScript
    {
        [JsonProperty("script")]
        public string Script
        {
            get;
            private set;
        }

        [JsonProperty("test")]
        public List<TestCase> Test
        {
            get;
            private set;
        }

        public TestScript(string script, List<TestCase> test)
        {
            SetScript(script);
            SetTest(test);
        }

        public TestScript() { }

        public void SetScript(string script)
        {
            Script = script;
        }

        public void SetTest(List<TestCase> test)
        {
            Test = test;
        }
    }
}
