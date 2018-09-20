using ELIZA.NET.Structures;
using Microsoft.Win32;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ELIZA.NET
{
    public class ScriptHandler
    {
        public Script script
        {
            get;
            private set;
        }

        public ScriptHandler(string scriptJSON, bool autoLoad = true)
        {
            this.script = null;

            if (autoLoad)
            {
                LoadFromJSONData(scriptJSON);
            }
        }

        public ScriptHandler() { this.script = null; }

        public void LoadFromJSON(string filename)
        {
            LoadFromJSONData(File.ReadAllText(filename));
        }

        public void LoadFromJSONData(string json)
        {
            this.script = JsonConvert.DeserializeObject<Script>(SanitizeJSON(json));
        }

        // JSON.NET does not seem to do well with embedded regex (things like word boundaries get parsed as JSON escape sequences).  Messy but effective workaround right here (TODO - Cleanup).
        // I realize we could get around this by deserializing to a JObject then converting, but this replacement workaround makes me cry a little less inside so I'm gonna stick with it for now.  --Kris
        private string SanitizeJSON(string json)
        {
            return Regex.Replace(json, @"(\\)(?=[a-z])", "_____").Replace("\"reassembly\":\"", "\"reassembly\":").Replace("]\"", "]").Replace("\\", "");
        }
    }
}
