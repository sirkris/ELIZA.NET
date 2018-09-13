using ELIZA.NET.Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ELIZA.NET
{
    /// <summary>
    /// Your session with the great ELIZA.
    /// </summary>
    private class Session
    {
        /// <summary>
        /// The ELIZA script that will be bound to this session.
        /// </summary>
        public Script Script
        {
            get;
            private set;
        }

        /// <summary>
        /// User input history.
        /// </summary>
        public List<string> History
        {
            get;
            private set;
        }

        // Variables used for parsing the input and formulating a response.  --Kris
        private string LastInput = null;
        private Dictionary<int, string> LastKeyStack = null;
        private Rule LastRule = null;
        private MatchCollection LastParts = null;

        public Session(Script script)
        {
            SetScript(script);
            ResetHistory();
        }

        /// <summary>
        /// Say something to ELIZA and get her response.
        /// </summary>
        /// <returns>A string representing ELIZA's response to your query.</returns>
        public string GetResponse(string s)
        {
            if (s == null)
            {
                return null;
            }

            s = s.Trim();

            if (s.Equals(""))
            {
                return (History.Count > 0 ? GetGenericResponse() : GetGreeting());
            }
            else if (s.ToLower().Contains("bye")
                || s.ToLower().Contains("farewell")
                || s.ToLower().Equals("exit")
                || s.ToLower().Equals("quit"))
            {
                return GetGoodbye();
            }

            ProcessInput(s);
            BuildKeyStack();

            if (!GetRule())
            {
                return GetGenericResponse();
            }

            return ProcessRule().Replace(" i ", " I ");
        }

        /// <summary>
        /// Invert pairs and reassemble.
        /// </summary>
        /// <returns></returns>
        private string ProcessRule()
        {

        }

        /// <summary>
        /// Applies transformations, removes non-alphanumeric characters, and updates session history.
        /// </summary>
        /// <param name="s">The trimmed input string.</param>
        private void ProcessInput(string s)
        {
            // Apply transformations.  --Kris


            // Remove all punctuation/symbols.  --Kris


            // Add to session history.  --Kris

        }

        /// <summary>
        /// Builds a rank-indexed stack of any script keywords found in the user input.
        /// </summary>
        private void BuildKeyStack()
        {

        }

        /// <summary>
        /// Scan the keystack for a matching rule, starting with the highest rank.
        /// </summary>
        /// <returns></returns>
        private bool GetRule()
        {

        }

        /// <summary>
        /// Gets a random generic response message from the script.
        /// </summary>
        /// <returns>The Response string from a randomly chosen GenericResponse object.</returns>
        public string GetGenericResponse()
        {
            return Script.GetRandomGenericResponse().ToString();
        }

        /// <summary>
        /// Gets a random goodbye message from the script.
        /// </summary>
        /// <returns>The Message string from a randomly chosen Goodbye object.</returns>
        public string GetGoodbye()
        {
            return Script.GetRandomGoodbye().ToString();
        }

        /// <summary>
        /// Gets a random greeting message from the script.
        /// </summary>
        /// <returns>The Greeting string from a randomly chosen Greeting object.</returns>
        public string GetGreeting()
        {
            return Script.GetRandomGreeting().ToString();
        }

        /// <summary>
        /// Manually specifies the script to be bound to this session.
        /// </summary>
        /// <param name="script">A valid Script object.</param>
        public void SetScript(Script script)
        {
            this.Script = script;
        }

        /// <summary>
        /// Manually clears the user input history.
        /// </summary>
        public void ResetHistory()
        {
            this.History = new List<string>();
        }
    }
}
