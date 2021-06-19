using Newtonsoft.Json;
using System.Net;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using System.Linq;

namespace PokeDex
{
    class APICall
    {
        public static string GetPokemonInfo(string url)
        {
            var webClient = new WebClient();
            byte[] data = webClient.DownloadData(url);

            using (var stream = new MemoryStream(data))
            using (var reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }

        public static List<Result> DeserializePokemon(string json)
        {
            JObject jObject = JObject.Parse(json);
            IList<JToken> results = jObject["results"].Children().ToList();
            List<Result> pokeList = new List<Result>(1283);
            foreach (JToken result in results)
            {
                Result pokemon = result.ToObject<Result>();
                pokeList.Add(pokemon);
            }
            return pokeList;
        }

        //public static List<PokemonEntry> DeSerializeEntryJson(string json)
        //{
        //    JObject jObject = JObject.Parse(json);
        //    IList<JToken> results = jObject.Children().ToList();
        //    List<PokemonEntry> pokemonEntry = new List<PokemonEntry>(20);

        //    //I think my issue is that I need further deserialization of more complex objects.
        //    //each ability needs to be added to the Ability class before each one can be added to the Abilities[] class?
        //    foreach (JToken result in results)
        //    {
        //        PokemonEntry pokemon = result.ToObject<PokemonEntry>();
        //        pokemonEntry.Add(pokemon);
        //    }
        //    return pokemonEntry;
        //}

        public static PokemonEntry DeSerializeEntryJson(string json) 
        { 
            return JsonConvert.DeserializeObject<PokemonEntry>(json); 
        }
    }
}
