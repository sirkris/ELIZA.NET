using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELIZA
{
    class Program
    {
        static void Main(string[] args)
        {
            ELIZAWrapper elizaWrapper;
            if (args.Length == 0)
            {
                elizaWrapper = new ELIZAWrapper();
            }
            else if (args.Length == 1)
            {
                if (args[0].Trim().ToLower().Equals("help"))
                {
                    Console.WriteLine("Usage: ELIZA.exe <script source> <script param>");
                    Console.WriteLine();
                    Console.WriteLine("Script source can be one of:  registry (default), json, or api.");
                    Console.WriteLine("If script source is registry, script param is the name of the script you want to use (default is DOCTOR).");
                    Console.WriteLine("Otherwise, script param is the local or URL path to the script JSON.");

                    return;
                }
                else
                {
                    elizaWrapper = new ELIZAWrapper(args[0]);
                }
            }
            else
            {
                elizaWrapper = new ELIZAWrapper(args[0], args[1]);
            }

            Console.WriteLine(elizaWrapper.start());

            string userInput;
            do
            {
                userInput = Console.ReadLine();
                Console.WriteLine(elizaWrapper.query(userInput));
            } while (!userInput.Trim().ToLower().Equals("goodbye")
                && !userInput.Trim().ToLower().Equals("bye")
                && !userInput.Trim().ToLower().Equals("farewell")
                && !userInput.Trim().ToLower().Equals("exit")
                && !userInput.Trim().ToLower().Equals("quit"));

            Console.WriteLine(elizaWrapper.stop());
        }
    }
}
