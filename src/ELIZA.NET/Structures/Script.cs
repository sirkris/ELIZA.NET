using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELIZA.NET.Structures
{
    [Serializable]
    public class Script
    {
        // Note - Setting default values on properties inline requires C# 6+, which I don't have at the moment, so we'll just do our getters/setters the old-fashioned way for now.  --Kris
        private List<GenericResponse> GenericResponses = null;
        private List<Goodbye> Goodbyes = null;
        private List<Greeting> Greetings = null;
        private List<Pair> Pairs = null;
        private List<Synonym> Synonyms = null;
        private List<Transformation> Transformations = null;
        private Dictionary<string, Keyword> Keywords = null;

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

        public List<GenericResponse> GetGenericResponses()
        {
            return GenericResponses;
        }

        public void SetGenericResponses(List<GenericResponse> genericResponses)
        {
            this.GenericResponses = genericResponses;
        }

        public List<Goodbye> GetGoodbyes()
        {
            return Goodbyes;
        }

        public void SetGoodbyes(List<Goodbye> goodbyes)
        {
            this.Goodbyes = goodbyes;
        }

        public List<Greeting> GetGreetings()
        {
            return Greetings;
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
