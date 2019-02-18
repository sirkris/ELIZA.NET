using System.Collections.Generic;

namespace RunTests.Structures
{
    class TestFailure
    {
        public string Type
        {
            get;
            private set;
        }

        public List<string> Results
        {
            get;
            private set;
        }

        public TestCase TestCase
        {
            get;
            private set;
        }

        public TestFailure(string type, List<string> results, TestCase testCase)
        {
            SetType(type);
            SetResults(results);
            SetTestCase(testCase);
        }

        public TestFailure() { }

        public void SetType(string type)
        {
            Type = type;
        }

        public void SetResults(List<string> results)
        {
            Results = results;
        }

        public void SetTestCase(TestCase testCase)
        {
            TestCase = testCase;
        }
    }
}
