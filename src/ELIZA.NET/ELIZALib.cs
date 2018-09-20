using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELIZA.NET
{
    public class ELIZALib
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

        public ELIZALib(string scriptJSON, bool autoLoad = true)
        {
            this.ScriptHandler = new ScriptHandler(scriptJSON, autoLoad);
            this.Session = new Session(ScriptHandler.Script);
        }

        /// <summary>
        /// Say something to ELIZA and get her response.
        /// </summary>
        /// <returns>A string representing ELIZA's response to your query.</returns>
        public string GetResponse(string s)
        {
            return Session.GetResponse(s);
        }

        /// <summary>
        /// Manually set the script handler.
        /// </summary>
        /// <param name="scriptHandler">A valid ScriptHandler object.</param>
        public void SetScriptHandler(ScriptHandler scriptHandler)
        {
            this.ScriptHandler = scriptHandler;
        }

        /// <summary>
        /// Manually set the session.
        /// </summary>
        /// <param name="session">A valid Session object.</param>
        public void SetSession(Session session)
        {
            this.Session = session;
        }
    }
}
