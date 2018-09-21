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
            if (args.Length != 2)
            {
                Console.WriteLine("Usage: RunTests.exe <test script path> <script path>");
                Console.WriteLine();
                Console.WriteLine(@"Example: RunTests.exe ..\..\..\..\scripts\DOCTOR\tests\doctorTest.json ..\..\..\..\scripts\DOCTOR\DOCTOR.json");
                Console.WriteLine();
                Console.WriteLine("Test script path must point to a JSON file with the correct format.");
                Console.WriteLine("Script path is the path to the script JSON.");

                return;
            }
            else
            {
                workflow = new Workflow(args[0], args[1]);
            }

            Console.WriteLine(workflow.Start());
        }
    }
}
