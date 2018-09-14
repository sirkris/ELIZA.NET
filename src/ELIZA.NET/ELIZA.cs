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
