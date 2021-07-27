using Newtonsoft.Json;
using System.Net;
using System.IO;

namespace PokeDex
{
    public class APICall
    {
        private static string initalUrl = "https://pokeapi.co/api/v2/pokemon?limit=1283";
        private static WebClient webClient = new WebClient(); 

        public static string MakeWebRequest(string url) //make private after things are moved around
        {
            byte[] data = webClient.DownloadData(url);
            using (var stream = new MemoryStream(data))
            using (var reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }

            //Instead of crashing the program, I need to handle an Exception
            //Make an exception class and make specific exceptions for things like this
            //Can maybe do so by checking if the list is null/empty and, if so, throw and exception. 
            //Can maybe ask "Would you like to try startup again?" to get the list
        }

        public static PokeList GetFullList()
        {
            return DeserializePokemon(MakeWebRequest(initalUrl));
        }
        public static PokemonEntry GetEntry(string entry)
        {
            return DeserializeEntryJson(MakeWebRequest("https://pokeapi.co/api/v2/pokemon/" + entry));
        }
        public static PokemonSpecies GetEntrySpecies(string speciesURL)
        {
            return DeserializeSpeciesJson(MakeWebRequest(speciesURL));
        }
        public static PokemonEvolution GetEntryEvolutionChain(string evolutionURL)
        {
            return DeserializeEvolutionJson(MakeWebRequest(evolutionURL));
        }

        private static PokeList DeserializePokemon(string json)
        {
            return JsonConvert.DeserializeObject<PokeList>(json);
        }
        private static PokemonEntry DeserializeEntryJson(string json)
        { 
            return JsonConvert.DeserializeObject<PokemonEntry>(json); 
        }
        private static PokemonSpecies DeserializeSpeciesJson(string json) 
        {
            return JsonConvert.DeserializeObject<PokemonSpecies>(json);
        }
        public static SpecialFormsDecriptions DeserializeSpecialJson(string json) //make private after things are moved around
        {
            return JsonConvert.DeserializeObject<SpecialFormsDecriptions>(json);
        }
        public static PokemonEvolution DeserializeEvolutionJson(string json) //make private after things are moved around
        {
            return JsonConvert.DeserializeObject<PokemonEvolution>(json);
        }
    }
}
