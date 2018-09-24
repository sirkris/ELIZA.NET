using System;

namespace ELIZA
{
    class Program
    {
        static void Main(string[] args)
        {
            ELIZAWrapper elizaWrapper;

            if (args.Length == 1)
            {
                elizaWrapper = new ELIZAWrapper(args[0]);
            }
            else
            {
                Console.WriteLine("Usage: ELIZA.exe <script JSON file>");

                return;
            }

            Console.WriteLine(elizaWrapper.Start());

            string userInput;
            do
            {
                userInput = Console.ReadLine();
                Console.WriteLine(elizaWrapper.Query(userInput));
            } while (userInput.Trim().IndexOf("bye", StringComparison.OrdinalIgnoreCase) < 0
                && userInput.Trim().IndexOf("farewell", StringComparison.OrdinalIgnoreCase) < 0
                && !userInput.Trim().Equals("exit", StringComparison.OrdinalIgnoreCase)
                && !userInput.Trim().Equals("quit", StringComparison.OrdinalIgnoreCase));
        }
    }
}
