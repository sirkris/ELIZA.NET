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
        public Script Script
        {
            get;
            private set;
        }

        public ScriptHandler(string scriptJSON, bool autoLoad = true)
        {
            this.Script = null;

            if (autoLoad)
            {
                LoadFromJSONData(scriptJSON);
            }
        }

        public ScriptHandler() { this.Script = null; }

        public void LoadFromJSONData(string json)
        {
            this.Script = JsonConvert.DeserializeObject<Script>(json);
        }
    }
}
