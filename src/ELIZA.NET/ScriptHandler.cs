using ELIZA.NET.Structures;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

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
