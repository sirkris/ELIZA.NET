using ELIZA.NET.Structures;
using Microsoft.Win32;
using Newtonsoft.Json;
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
        private Dictionary<string, RegistryKey> keys = new Dictionary<string, RegistryKey>
        {
            { "software", Registry.CurrentUser.OpenSubKey( "Software", true ) }
        };

        private Script script = null;  // TODO - Use public get/private set with default value after upgrading VS.  --Kris

        public ScriptHandler(bool autoLoad = Constants.DEFAULT_AUTOLOAD, int source = Constants.DEFAULT_SOURCE, string sourceParam = Constants.DEFAULT_SCRIPT)
        {
            keys.Add("app", keys["software"].CreateSubKey("ELIZA.NET"));
            keys.Add("scripts", keys["app"].CreateSubKey("Scripts"));

            SaveKeys();

            if (autoLoad)
            {
                switch (source)
                {
                    default:
                        throw new ArgumentException("Unrecognized source passed to constructor.");
                    case Constants.SOURCE_REGISTRY:
                        LoadFromRegistry(sourceParam);
                        break;
                    case Constants.SOURCE_JSON:
                        LoadFromJSON(sourceParam);
                        break;
                    case Constants.SOURCE_API:
                        LoadFromAPI(sourceParam);
                        break;
                }
            }
        }

        private void SaveKeys()
        {
            // Flush individual script keys before flushing their parents.  --Kris
            foreach (KeyValuePair<string, RegistryKey> pair in keys)
            {
                if (!pair.Key.Equals("software")
                    && !pair.Key.Equals("app")
                    && !pair.Key.Equals("scripts"))
                {
                    pair.Value.Flush();
                }
            }

            keys["scripts"].Flush();
            keys["app"].Flush();
            keys["software"].Flush();
        }

        public void LoadFromRegistry(string scriptName)
        {
            if (scriptName.ToLower().Equals("software")
                || scriptName.ToLower().Equals("app")
                || scriptName.ToLower().Equals("scripts"))
            {
                throw new ArgumentException("Script name cannot be 'software', 'app', or 'scripts' because those keys are reserved.");
            }

            if (keys.ContainsKey(scriptName))
            {
                keys.Remove(scriptName);
            }

            // Reminder:  This handler is only designed to handle one script at a time.  If you want to juggle multiple scripts, you should use multiple instances of this class.  --Kris
            keys.Add(scriptName, keys["scripts"].CreateSubKey(scriptName));

            string json = "{"
                + "\"GenericResponses\":" + (string) keys[scriptName].GetValue("GenericResponses", new List<GenericResponse>()) + ","
                + "\"Goodbyes\":" + (string) keys[scriptName].GetValue("Goodbyes", new List<Goodbye>()) + ","
                + "\"Greetings\":" + (string) keys[scriptName].GetValue("Greetings", new List<Greeting>()) + ","
                + "\"Pairs\":" + (string) keys[scriptName].GetValue("Pairs", new List<Pair>()) + ","
                + "\"Synonyms\":" + (string) keys[scriptName].GetValue("Synonyms", new List<Synonym>()) + ","
                + "\"Transformations\":" + (string) keys[scriptName].GetValue("Transformations", new List<Transformation>()) + ","
                + "\"Keywords\":" + (string) keys[scriptName].GetValue("Keywords", new List<Keyword>())
                + "}";

            LoadFromJSONData(json);
        }

        public void SaveToRegistry()
        {
            if (script.scriptName.ToLower().Equals("software")
                || script.scriptName.ToLower().Equals("app")
                || script.scriptName.ToLower().Equals("scripts"))
            {
                throw new ArgumentException("Script name cannot be 'software', 'app', or 'scripts' because those keys are reserved.");
            }

            if (!keys.ContainsKey(script.scriptName))
            {
                keys.Add(script.scriptName, keys["scripts"].CreateSubKey(script.scriptName));
            }

            keys[script.scriptName].SetValue("GenericResponses", JsonConvert.SerializeObject(script.GenericResponses));
            keys[script.scriptName].SetValue("Goodbyes", JsonConvert.SerializeObject(script.Goodbyes));
            keys[script.scriptName].SetValue("Greetings", JsonConvert.SerializeObject(script.Greetings));
            keys[script.scriptName].SetValue("Pairs", JsonConvert.SerializeObject(script.Pairs));
            keys[script.scriptName].SetValue("Synonyms", JsonConvert.SerializeObject(script.Synonyms));
            keys[script.scriptName].SetValue("Transformations", JsonConvert.SerializeObject(script.Transformations));
            keys[script.scriptName].SetValue("Keywords", JsonConvert.SerializeObject(script.Keywords));

            SaveKeys();
        }

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

        public void LoadFromAPI(string URI)
        {
            string json = null;
            using (System.Net.WebClient client = new System.Net.WebClient())
            {
                json = client.DownloadString(URI);
            }

            if (json == null)
            {
                throw new System.Net.WebException("API retrieval failed or returned a null result.");
            }

            LoadFromJSONData(json);
        }

        public Script GetScript()
        {
            return script;
        }
    }
}
