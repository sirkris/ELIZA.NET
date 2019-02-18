namespace ELIZA.NET
{
    /// <summary>
    /// The main class for ELIZA.NET.
    /// </summary>
    public class ELIZALib
    {
        /// <summary>
        /// Class responsible for deserializing an ELIZA script.
        /// </summary>
        public ScriptHandler ScriptHandler
        {
            get;
            private set;
        }

        /// <summary>
        /// An instance of this class represents a session with ELIZA.
        /// </summary>
        public Session Session
        {
            get;
            private set;
        }

        /// <summary>
        /// Create a new instance of ELIZA.
        /// </summary>
        /// <param name="scriptJSON">A JSON string containing the ELIZA script.</param>
        /// <param name="autoLoad">If true, the ELIZA script will be automatically loaded in the constructor and the session will be initialized.</param>
        public ELIZALib(string scriptJSON, bool autoLoad = true)
        {
            ScriptHandler = new ScriptHandler(scriptJSON, autoLoad);
            Session = new Session(ScriptHandler.Script);
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
            ScriptHandler = scriptHandler;
        }

        /// <summary>
        /// Manually set the session.
        /// </summary>
        /// <param name="session">A valid Session object.</param>
        public void SetSession(Session session)
        {
            Session = session;
        }
    }
}
