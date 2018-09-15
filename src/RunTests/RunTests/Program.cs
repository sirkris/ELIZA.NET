using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunTests
{
    class Program
    {
        static void Main(string[] args)
        {
            Workflow workflow;
            if (args.Length == 0)
            {
                Console.WriteLine("Usage: RunTests.exe <test script path> <script source> <script param>");
                Console.WriteLine();
                Console.WriteLine(@"Example: RunTests.exe ..\..\..\..\..\scripts\DOCTOR\tests\doctorTest.json json ..\..\..\..\..\scripts\DOCTOR\DOCTOR.json");
                Console.WriteLine();
                Console.WriteLine("Test script path must point to a JSON file with the correct format.");
                Console.WriteLine("Script source can be one of:  registry (default), json, or api.");
                Console.WriteLine("If script source is registry, script param arg is ignored.");
                Console.WriteLine("Otherwise, script param is the local or URL path to the script JSON.");

                return;
            }
            else if (args.Length == 1)
            {
                workflow = new Workflow(args[0]);
            }
            else
            {
                workflow = new Workflow(args[1], args[0], (args.Count() > 2 ? args[2] : null));
            }

            Console.WriteLine(workflow.start());
        }
    }
}
