using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunTests.Structures
{
    [Serializable]
    class TestCase
    {
        [JsonProperty("input")]
        public string Input
        {
            get;
            private set;
        }

        [JsonProperty("expected")]
        public List<string> Expected
        {
            get;
            private set;
        }

        public TestCase(string input, List<string> expected)
        {
            SetInput(input);
            SetExpected(expected);
        }

        public void SetInput(string input)
        {
            this.Input = input;
        }

        public void SetExpected(List<string> expected)
        {
            this.Expected = expected;
        }
    }
}
