using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELIZA.NET
{
    public static class Constants
    {
        // Defaults you can change to your liking.  --Kris
        public const int DEFAULT_SOURCE = SOURCE_REGISTRY;
        public const bool DEFAULT_AUTOLOAD = true;
        public const string DEFAULT_SCRIPT = "DOCTOR";

        // No need to modify anything below here.  --Kris
        public const int SOURCE_REGISTRY = 0;
        public const int SOURCE_JSON = 1;
        public const int SOURCE_API = 2;
    }
}
