using Newtonsoft.Json;
using RunTests.Structures;
using System;
using System.Collections.Generic;
using System.IO;

namespace RunTests
{
    class Workflow
    {
        private ELIZAWrapper ElizaWrapper;
        private TestScript TestScript;

        public Workflow(string testScript, string scriptPath)
        {
            InitializeWorkflow(testScript, scriptPath);
        }

        private void InitializeWorkflow(string testScript, string scriptPath)
        {
            LoadTestScript(testScript);

            ElizaWrapper = new ELIZAWrapper(scriptPath);
        }

        public void LoadTestScript(string testScript)
        {
            TestScript = JsonConvert.DeserializeObject<Structures.TestScript>(File.ReadAllText(testScript));
        }

        public string Start()
        {
            string res = "";

            DateTime start = DateTime.Now;

            int tests = 0;
            int passed = 0;
            int total = 0;
            List<TestFailure> errors = new List<TestFailure>();

            foreach (TestCase testCase in TestScript.Test)
            {
                int retry = (testCase.Expected.Count * 100);
                List<string> replies = new List<string>();
                do
                {
                    string reply = ElizaWrapper.Query(testCase.Input);
                    if (testCase.Expected.Contains(reply))
                    {
                        if (!replies.Contains(reply))
                        {
                            replies.Add(reply);
                            tests++;
                        }
                    }
                    else
                    {
                        errors.Add(new TestFailure("Unexpected Result", new List<string> { reply }, testCase));
                        break;
                    }

                    retry--;
                } while (replies.Count < testCase.Expected.Count
                    && retry > 0);

                if (replies.Count == testCase.Expected.Count)
                {
                    passed++;
                }
                else if (retry == 0)
                {
                    errors.Add(new TestFailure("Incomplete Result", replies, testCase));
                }

                total++;
            }

            double runTime = ((DateTime.Now.ToUniversalTime() - new DateTime(1970, 1, 1)).TotalMilliseconds - (start.ToUniversalTime() - new DateTime(1970, 1, 1)).TotalMilliseconds) / 1000;

            res += Environment.NewLine + "Executed " + tests.ToString() + " tests on " + TestScript.Test.Count.ToString() + " ELIZA inputs in "
                + runTime.ToString() + " seconds successfully." + Environment.NewLine;

            if (errors.Count > 0)
            {
                res += Environment.NewLine + "TEST FAILURES:" + Environment.NewLine;
                foreach (TestFailure testFailure in errors)
                {
                    res += "--------------------------------------" + Environment.NewLine;
                    res += "Type: " + testFailure.Type + Environment.NewLine;
                    res += "Input: " + testFailure.TestCase.Input + Environment.NewLine;

                    if (testFailure.Results.Count == 1)
                    {
                        res += "Result: " + testFailure.Results[0] + Environment.NewLine;
                    }
                    else
                    {
                        res += "Results: " + Environment.NewLine + "[" + Environment.NewLine;
                        foreach (string result in testFailure.Results)
                        {
                            res += "\t" + result + Environment.NewLine;
                        }
                        res += "]" + Environment.NewLine;
                    }

                    res += "Expected: " + Environment.NewLine + "[" + Environment.NewLine;
                    foreach (string expected in testFailure.TestCase.Expected)
                    {
                        res += "\t" + expected + Environment.NewLine;
                    }
                    res += "]" + Environment.NewLine;
                }
            }

            res += Environment.NewLine + "Test Cases Passed: " + passed.ToString() + " / " + total.ToString() + " (" + (((double)passed / (double)total) * 100).ToString() + @"%)";

            return res;
        }
    }
}
