using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;

namespace ELIZA.NET.Structures
{
    [Serializable]
    public class GenericResponse
    {
        [JsonProperty("response")]
        private string Response = null;

        [JsonProperty("script")]
        private string Script = null;

        public GenericResponse(string response, string script)
        {
            this.Response = response;
            this.Script = script;
        }

        public GenericResponse() { }

        public string GetResponse()
        {
            return Response;
        }

        public void SetResponse(string response)
        {
            this.Response = response;
        }

        public string GetScript()
        {
            return Script;
        }

        public void SetScript(string script)
        {
            this.Script = script;
        }

        public string ToString()
        {
            return Response;
        }
    }
}
