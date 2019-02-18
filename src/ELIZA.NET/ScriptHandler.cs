using ELIZA.NET.Structures;
using Newtonsoft.Json;

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
            Script = null;

            if (autoLoad)
            {
                LoadFromJSONData(scriptJSON);
            }
        }

        public ScriptHandler() { Script = null; }

        public void LoadFromJSONData(string json)
        {
            Script = JsonConvert.DeserializeObject<Script>(json);
        }
    }
}
