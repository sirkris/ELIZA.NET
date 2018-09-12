using ELIZA.NET.Structures;
using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELIZA.NET
{
    public class ScriptHandler
    {
        private const int SOURCE_REGISTRY = 0;
        private const int SOURCE_JSON = 1;
        private const int SOURCE_API = 2;

        private Dictionary<string, RegistryKey> keys = new Dictionary<string, RegistryKey>
        {
            { "software", Registry.CurrentUser.OpenSubKey( "Software", true ) }
        };

        private Script script = null;  // TODO - Use public get/private set with default value after upgrading VS.  --Kris

        public ScriptHandler(bool autoLoad = true, int source = SOURCE_REGISTRY, string sourceParam = "DOCTOR")
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
                    case SOURCE_REGISTRY:
                        LoadFromRegistry(sourceParam);
                        break;
                    case SOURCE_JSON:
                        LoadFromJSON(sourceParam);
                        break;
                    case SOURCE_API:
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

            this.script = new Script(
                JsonConvert.DeserializeObject<List<GenericResponse>>((string) keys[scriptName].GetValue("GenericResponses", new List<GenericResponse>())),
                JsonConvert.DeserializeObject<List<Goodbye>>((string) keys[scriptName].GetValue("Goodbyes", new List<Goodbye>())),
                JsonConvert.DeserializeObject<List<Greeting>>((string) keys[scriptName].GetValue("Greetings", new List<Greeting>())),
                JsonConvert.DeserializeObject<List<Pair>>((string) keys[scriptName].GetValue("Pairs", new List<Pair>())),
                JsonConvert.DeserializeObject<List<Synonym>>((string) keys[scriptName].GetValue("Synonyms", new List<Synonym>())),
                JsonConvert.DeserializeObject<List<Transformation>>((string) keys[scriptName].GetValue("Transformations", new List<Transformation>())),
                JsonConvert.DeserializeObject<List<Keyword>>((string) keys[scriptName].GetValue("Keywords", new List<Keyword>()))
            );
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

            keys[script.scriptName].SetValue("GenericResponses", JsonConvert.SerializeObject(script.GetGenericResponses()));
            keys[script.scriptName].SetValue("Goodbyes", JsonConvert.SerializeObject(script.GetGoodbyes()));
            keys[script.scriptName].SetValue("Greetings", JsonConvert.SerializeObject(script.GetGreetings()));
            keys[script.scriptName].SetValue("Pairs", JsonConvert.SerializeObject(script.GetPairs()));
            keys[script.scriptName].SetValue("Synonyms", JsonConvert.SerializeObject(script.GetSynonyms()));
            keys[script.scriptName].SetValue("Transformations", JsonConvert.SerializeObject(script.GetTransformations()));
            keys[script.scriptName].SetValue("Keywords", JsonConvert.SerializeObject(script.GetKeywords()));

            SaveKeys();
        }

        public void LoadFromJSON(string filename)
        {
            LoadFromJSONData(File.ReadAllText(filename));
        }

        public void LoadFromJSONData(string json)
        {
            this.script = JsonConvert.DeserializeObject<Script>(json);
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
