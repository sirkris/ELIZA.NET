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
        public string Response = null;

        [JsonProperty("script")]
        public string Script = null;

        public GenericResponse(string response, string script)
        {
            this.Response = response;
            this.Script = script;
        }

        public GenericResponse() { }

        public override string ToString()
        {
            return Response;
        }
    }
}
