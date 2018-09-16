using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;

namespace ELIZA.NET.Structures
{
    [Serializable]
    public class Script
    {
        // Note - Setting default values on properties inline requires C# 6+, which I don't have at the moment, so we'll just do our getters/setters the old-fashioned way for now.  --Kris
        [JsonProperty("genericResponses")]
        private List<GenericResponse> GenericResponses = null;

        [JsonProperty("goodbyes")]
        private List<Goodbye> Goodbyes = null;

        [JsonProperty("greetings")]
        private List<Greeting> Greetings = null;

        [JsonProperty("pairs")]
        private List<Pair> Pairs = null;

        [JsonProperty("synonyms")]
        private List<Synonym> Synonyms = null;

        [JsonProperty("transformations")]
        private List<Transformation> Transformations = null;

        [JsonProperty("keywords")]
        private Dictionary<string, Keyword> Keywords = null;

        public string scriptName = null;
        private Random rand = null;

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
            this.scriptName = this.GenericResponses[0].GetScript();

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
            this.scriptName = this.GenericResponses[0].GetScript();

            this.rand = new Random();
        }

        public Script() { }

        private Dictionary<string, Keyword> IndexKeywords(List<Keyword> keywords)
        {
            Dictionary<string, Keyword> res = new Dictionary<string, Keyword>();
            foreach (Keyword keyword in keywords)
            {
                res.Add(keyword.GetWord(), keyword);
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

        public List<GenericResponse> GetGenericResponses()
        {
            return GenericResponses;
        }

        public GenericResponse GetRandomGenericResponse()
        {
            return GenericResponses[GetRand(GenericResponses.Count)];
        }

        public void SetGenericResponses(List<GenericResponse> genericResponses)
        {
            this.GenericResponses = genericResponses;
        }

        public List<Goodbye> GetGoodbyes()
        {
            return Goodbyes;
        }

        public Goodbye GetRandomGoodbye()
        {
            return Goodbyes[GetRand(Goodbyes.Count)];
        }

        public void SetGoodbyes(List<Goodbye> goodbyes)
        {
            this.Goodbyes = goodbyes;
        }

        public List<Greeting> GetGreetings()
        {
            return Greetings;
        }

        public Greeting GetRandomGreeting()
        {
            return Greetings[GetRand(Greetings.Count)];
        }

        public void SetGreetings(List<Greeting> greetings)
        {
            this.Greetings = greetings;
        }

        public List<Pair> GetPairs()
        {
            return Pairs;
        }

        public void SetPairs(List<Pair> pairs)
        {
            this.Pairs = pairs;
        }

        public List<Synonym> GetSynonyms()
        {
            return Synonyms;
        }

        public void SetSynonyms(List<Synonym> synonyms)
        {
            this.Synonyms = synonyms;
        }

        public List<Transformation> GetTransformations()
        {
            return Transformations;
        }

        public void SetTransformations(List<Transformation> transformations)
        {
            this.Transformations = transformations;
        }

        public Dictionary<string, Keyword> GetKeywords()
        {
            return Keywords;
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
