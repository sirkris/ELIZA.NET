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
    public class Session
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
        /// <returns>Reassembly with inverted pairs.</returns>
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

        /// <summary>
        /// Replace parts with their inverse counterparts as defined in the script.  For example, "I" becomes "you" and "was" becomes "were".
        /// </summary>
        /// <param name="reassembly">ELIZA's draft response.</param>
        /// <returns>ELIZA's partially processed response.</returns>
        private string InvertPairs(string reassembly)
        {
            List<string> parts = new List<string>();

            // I know how this looks, but it's actually just O(m * n) because we're still doing the same number of iterations we would if going through the original pairs list.  --Kris
            foreach (KeyValuePair<int, List<Pair>> pairs in SanitizePairs().Reverse())
            {
                foreach (Pair pair in pairs.Value)
                {
                    for (int i = 0; i < LastParts.Count; i++)
                    {
                        string part = LastParts[i].Value;
                        part = ReplacePart(pair.GetWord(), pair.GetInverse(), part);

                        if (pair.GetBidirectional()
                            && part.Equals(LastParts[i].Value))
                        {
                            part = ReplacePart(pair.GetInverse(), pair.GetWord(), part);
                        }

                        parts.Add(part);
                    }
                }
            }

            return ApplyParts(reassembly, parts);
        }

        /// <summary>
        /// Reorder pairs by number of words so that longer phrases will always be matched first.
        /// </summary>
        /// <returns>Pairs reordered into a nested sorted dictionary.  The actual number of pairs remains the same.</returns>
        private SortedDictionary<int, List<Pair>> SanitizePairs()
        {
            SortedDictionary<int, List<Pair>> pairs = new SortedDictionary<int, List<Pair>>();
            foreach (Pair pair in Script.GetPairs())
            {
                int i = pair.GetWord().Split(' ').Count();
                if (!pairs.ContainsKey(i))
                {
                    pairs.Add(i, new List<Pair>());
                }

                pairs[i].Add(pair);
            }

            return pairs;
        }

        /// <summary>
        /// Replace a word with its inverse in a given part.
        /// </summary>
        /// <param name="word">The word to be replaced.</param>
        /// <param name="inverse">The replacement word.</param>
        /// <param name="part">The part to which the replacement is being applied.</param>
        /// <returns>The modified part.</returns>
        private string ReplacePart(string word, string inverse, string part)
        {
            return Regex.Replace(part, ' ' + word + ' ', ' ' + inverse + ' ', RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// Apply the user input parts to the reassembly.
        /// </summary>
        /// <param name="reassembly">ELIZA's partially processed response.</param>
        /// <param name="parts">The modified user input parts to be applied.</param>
        /// <returns>ELIZA's fully processed response.</returns>
        private string ApplyParts(string reassembly, List<string> parts)
        {
            for (int i = 0; i < parts.Count(); i++)
            {
                reassembly = reassembly.Replace('$' + (i + 1).ToString(), parts[i].Trim());
            }

            return reassembly;
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
