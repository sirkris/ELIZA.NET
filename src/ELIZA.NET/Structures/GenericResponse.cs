using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELIZA.NET.Structures
{
    public class GenericResponse
    {
        private string response = null;
        private string script = null;

        public GenericResponse(string response, string script)
        {
            this.response = response;
            this.script = script;
        }

        public GenericResponse() { }

        public string GetResponse()
        {
            return response;
        }

        public void SetResponse(string response)
        {
            this.response = response;
        }

        public string GetScript()
        {
            return script;
        }

        public void SetScript(string script)
        {
            this.script = script;
        }
    }
}
