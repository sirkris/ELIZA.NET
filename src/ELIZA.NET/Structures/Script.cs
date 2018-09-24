using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Text;

namespace ELIZA.NET.Structures
{
    [Serializable]
    public class Script
    {
        [JsonProperty("genericResponses")]
        public List<GenericResponse> GenericResponses = null;

        [JsonProperty("goodbyes")]
        public List<Goodbye> Goodbyes = null;

        [JsonProperty("greetings")]
        public List<Greeting> Greetings = null;

        [JsonProperty("pairs")]
        public List<Pair> Pairs = null;

        [JsonProperty("synonyms")]
        public List<Synonym> Synonyms = null;

        [JsonProperty("transformations")]
        public List<Transformation> Transformations = null;

        [JsonProperty("keywords")]
        public Dictionary<string, Keyword> Keywords
        {
            get;
            private set;
        }

        public string scriptName = null;
        public Random rand = null;

        public Script(List<GenericResponse> genericResponses, List<Goodbye> goodbyes, List<Greeting> greetings,
            List<Pair> pairs, List<Synonym> synonyms, List<Transformation> transformations, Dictionary<string, Keyword> keywords)
        {
            this.GenericResponses = genericResponses;
            this.Goodbyes = goodbyes;
            this.Greetings = greetings;
            this.Pairs = pairs;
            this.Synonyms = synonyms;
            this.Transformations = transformations;
            this.Keywords = keywords;

            // TODO - Modify structure so that script name is top-level in JSON (and remove redundant entries).  --Kris
            this.scriptName = this.GenericResponses[0].Script;

            this.rand = new Random();
        }

        public Script(List<GenericResponse> genericResponses, List<Goodbye> goodbyes, List<Greeting> greetings,
            List<Pair> pairs, List<Synonym> synonyms, List<Transformation> transformations, List<Keyword> keywords)
        {
            this.GenericResponses = genericResponses;
            this.Goodbyes = goodbyes;
            this.Greetings = greetings;
            this.Pairs = pairs;
            this.Synonyms = synonyms;
            this.Transformations = transformations;
            this.Keywords = IndexKeywords(keywords);

            // TODO - Modify structure so that script name is top-level in JSON (and remove redundant entries).  --Kris
            this.scriptName = this.GenericResponses[0].Script;

            this.rand = new Random();
        }

        public Script() { this.Keywords = null; }

        private Dictionary<string, Keyword> IndexKeywords(List<Keyword> keywords)
        {
            Dictionary<string, Keyword> res = new Dictionary<string, Keyword>();
            foreach (Keyword keyword in keywords)
            {
                res.Add(keyword.Word, keyword);
            }

            return res;
        }

        private int GetRand(int maxValue)
        {
            return GetRand(0, maxValue);
        }

        private int GetRand(int minValue, int maxValue)
        {
            if (rand == null)
            {
                rand = new Random();
            }

            return rand.Next(minValue, maxValue);
        }

        public GenericResponse GetRandomGenericResponse()
        {
            return GenericResponses[GetRand(GenericResponses.Count)];
        }

        public Goodbye GetRandomGoodbye()
        {
            return Goodbyes[GetRand(Goodbyes.Count)];
        }

        public Greeting GetRandomGreeting()
        {
            return Greetings[GetRand(Greetings.Count)];
        }

        public void SetKeywords(Dictionary<string, Keyword> keywords)
        {
            this.Keywords = keywords;
        }

        public void SetKeywords(List<Keyword> keywords)
        {
            this.Keywords = IndexKeywords(keywords);
        }
    }
}
