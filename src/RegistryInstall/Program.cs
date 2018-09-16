using ELIZA.NET;
using ELIZA.NET.Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistryInstall
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: RegistryInstall.exe <script path> <script name>");
                Console.WriteLine();
                Console.WriteLine("Script path must point to a JSON file with the correct format.");
                Console.WriteLine("Script name is the string under which this script will be indexed.");

                return;
            }
            else
            {
                int source = (args[0].Length > 8
                    && (args[0].Substring(0, 7).ToLower().Equals("http://") || args[0].Substring(0, 8).ToLower().Equals("https://")) ? Constants.SOURCE_API : Constants.SOURCE_JSON);

                ScriptHandler scriptHandler = new ScriptHandler(true, source, args[0]);

                Console.WriteLine("Script load complete.  Saving to registry....");

                scriptHandler.GetScript().scriptName = args[1];

                scriptHandler.SaveToRegistry();

                Console.WriteLine("Done!");
            }
        }
    }
}
