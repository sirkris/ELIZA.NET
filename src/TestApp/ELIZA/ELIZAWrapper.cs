using ELIZA.NET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELIZA
{
    public class ELIZAWrapper
    {
        public ELIZALib eliza = null;

        /// <summary>
        /// An example ELIZA wrapper class.
        /// </summary>
        /// <param name="source">One of "registry", "json", or "api".</param>
        /// <param name="sourceParam">If source is registry, sourceParam is the script name.  Otherwise, it's the path to the script JSON.</param>
        public ELIZAWrapper(string source = "registry", string sourceParam = "DOCTOR")
        {
            switch (source.Trim().ToLower())
            {
                default:
                    throw new ArgumentException("Source must be registry, json, or api.");
                case "registry":
                    eliza = new ELIZALib(ELIZA.NET.Constants.DEFAULT_AUTOLOAD, ELIZA.NET.Constants.SOURCE_REGISTRY, sourceParam);
                    break;
                case "json":
                    eliza = new ELIZALib(ELIZA.NET.Constants.DEFAULT_AUTOLOAD, ELIZA.NET.Constants.SOURCE_JSON, sourceParam);
                    break;
                case "api":
                    eliza = new ELIZALib(ELIZA.NET.Constants.DEFAULT_AUTOLOAD, ELIZA.NET.Constants.SOURCE_API, sourceParam);
                    break;
            }
        }

        public string start()
        {
            return eliza.Session.GetGreeting();
        }

        public string stop()
        {
            return eliza.Session.GetGoodbye();
        }

        public string query(string q)
        {
            return eliza.GetResponse(q);
        }
    }
}
