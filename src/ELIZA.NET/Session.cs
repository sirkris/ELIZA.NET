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
        private Random rand = null;

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
        private SortedDictionary<int, List<string>> LastKeyStack = null;
        private Rule LastRule = null;
        private MatchCollection LastParts = null;

        public Session(Script script)
        {
            SetScript(script);
            ResetHistory();

            this.rand = new Random();
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
            return InvertPairs(GetReassembly());
        }

        /// <summary>
        /// Gets the reassembly string to use in formulating ELIZA's response.
        /// </summary>
        /// <returns>A randomly-chosen reassembly string from the last rule.</returns>
        private string GetReassembly()
        {
            string reassembly = LastRule.GetReassembly()[rand.Next(LastRule.GetReassembly().Count())];

            // If reassembly string is of the form "GOTO <keyword>" (keyword MUST have a matching decomposition rule!), that keyword's reassembly will be substituted.  --Kris
            if (reassembly.Substring(0, 5).Equals("GOTO "))
            {
                if (CheckRule(reassembly.Substring(5)))
                {
                    GetReassembly();
                }
                else
                {
                    throw new InvalidOperationException("GOTO statement in reassembly rule leads to unmatched decomposition rule!  This needs to be fixed in the script.");
                }
            }

            return reassembly;
        }

        private string InvertPairs(string reassembly)
        {

        }

        private List<string> SanitizePairs()
        {

        }

        private string ReplacePart(string word, string inverse, string part)
        {
            return Regex.Replace(part, ' ' + word + ' ', ' ' + inverse + ' ', RegexOptions.IgnoreCase);
        }

        private string ApplyParts(string reassembly)
        {

        }

        /// <summary>
        /// Applies transformations, removes non-alphanumeric characters, and updates session history.
        /// </summary>
        /// <param name="s">The trimmed input string.</param>
        private void ProcessInput(string s)
        {
            // Add to session history.  --Kris
            History.Add(s);

            // Apply transformations.  --Kris
            foreach (Transformation transformation in Script.GetTransformations())
            {
                foreach (string alias in transformation.GetAliases())
                {
                    s = Regex.Replace(s, alias, transformation.GetWord(), RegexOptions.IgnoreCase);
                }
            }

            // Remove all punctuation/symbols.  --Kris
            this.LastInput = Regex.Replace(s, @"[^\da-z ]", "", RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// Builds a rank-indexed stack of any script keywords found in the user input.
        /// </summary>
        private void BuildKeyStack()
        {
            this.LastKeyStack = new SortedDictionary<int, List<string>>();
            foreach (string word in LastInput.Split(' '))
            {
                if (Script.GetKeywords().ContainsKey(word.ToLower())
                    && !LastKeyStack.ContainsKey(Script.GetKeywords()[word.ToLower()].GetRank()))
                {
                    LastKeyStack.Add(Script.GetKeywords()[word.ToLower()].GetRank(), new List<string>());
                }
            }
        }

        /// <summary>
        /// Scan the keystack for a matching rule, starting with the highest rank.
        /// </summary>
        /// <returns>Whether or not the rule exists.</returns>
        private bool GetRule()
        {
            foreach (KeyValuePair<int, List<string>> pair in LastKeyStack.Reverse())
            {
                foreach (string keyword in pair.Value)
                {
                    if (CheckRule(keyword))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Check to see if any of keyword's decomposition rules applies to the last input.  If so, deconstruct into matched parts.
        /// </summary>
        /// <param name="keyword">A keyword defined in the bound ELIZA script.</param>
        /// <returns>Whether or not any of the keyword's decomposition rules matched the last input.</returns>
        private bool CheckRule(string keyword)
        {
            foreach (Rule rule in Script.GetKeywords()[keyword].GetRules())
            {
                // If decomposition rule contains '@' alias, match against all corresponding entries in the synonyms table.  --Kris
                foreach (Synonym synonym in Script.GetSynonyms())
                {
                    rule.SetDecomposition(rule.GetDecomposition().Replace('@' + synonym.GetWord(), synonym.GetWord() + '|' + String.Join(@"|", synonym.GetAliases().ToArray())));
                }

                // Match the decomposition rule against the last input and capture matched parts.  --Kris
                MatchCollection matches = Regex.Matches(LastInput, rule.GetDecomposition(), RegexOptions.IgnoreCase);
                if (matches.Count > 0)
                {
                    this.LastRule = rule;
                    return true;
                }
            }

            return false;
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
