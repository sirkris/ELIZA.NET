using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELIZA.NET
{
    public class ELIZA
    {
        public ScriptHandler ScriptHandler
        {
            get;
            private set;
        }

        public Session Session
        {
            get;
            private set;
        }

        public ELIZA(bool autoLoad = Constants.DEFAULT_AUTOLOAD, int source = Constants.DEFAULT_SOURCE, string sourceParam = Constants.DEFAULT_SCRIPT)
        {
            this.ScriptHandler = new ScriptHandler(autoLoad, source, sourceParam);
            this.Session = new Session(ScriptHandler.GetScript());
        }

        public void SetScriptHandler(ScriptHandler scriptHandler)
        {
            this.ScriptHandler = scriptHandler;
        }

        public void SetSession(Session session)
        {
            this.Session = session;
        }
    }
}
