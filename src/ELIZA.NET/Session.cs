using ELIZA.NET.Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELIZA.NET
{
    private class Session
    {
        public Script Script
        {
            get;
            private set;
        }

        public Session(Script script)
        {
            this.Script = script;
        }

        public void SetScript(Script script)
        {
            this.Script = script;
        }


    }
}
