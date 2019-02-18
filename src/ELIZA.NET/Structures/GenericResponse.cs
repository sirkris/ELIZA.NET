using System;
using Newtonsoft.Json;

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
            Response = response;
            Script = script;
        }

        public GenericResponse() { }

        public override string ToString()
        {
            return Response;
        }
    }
}
