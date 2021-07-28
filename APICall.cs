using Newtonsoft.Json;
using System.Net;
using System.IO;
using System;

namespace PokeDex
{
    public class APICall
    {
        private static string initalUrl = "https://pokeapi.co/api/v2/pokemon?limit=1283";
        private static WebClient webClient = new WebClient();

        private static byte[] WebRequest(string url)
        {
            var data = new byte[] { };           
            bool test = true;
            while (test)
            {
                try
                {
                    data = webClient.DownloadData(url);
                    test = false;
                }
                catch (Exception)
                {
                    Console.WriteLine("Hmm...It would appear that I do not have a connection to PokeAPI.");
                    Console.WriteLine("Enter \"quit\" to exit the program. Enter anything to try again.");
                    string errorEntry = Console.ReadLine();             
                    bool quit = Formatting.CheckForQuit(errorEntry);
                    if (quit == false) { Environment.Exit(1);}
                }
            }
            return data;
        }

        private static string ReturnWebRequest(string url)
        {
            byte[] data = WebRequest(url);
            using (var stream = new MemoryStream(data))
            using (var reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }

        public static PokeList GetFullList()
        {
            return DeserializePokemon(ReturnWebRequest(initalUrl));
        }
        public static PokemonEntry GetEntry(string entry)
        {
            return DeserializeEntryJson(ReturnWebRequest("https://pokeapi.co/api/v2/pokemon/" + entry));
        }
        public static PokemonSpecies GetEntrySpecies(string speciesURL)
        {
            return DeserializeSpeciesJson(ReturnWebRequest(speciesURL));
        }
        public static PokemonEvolution GetEntryEvolutionChain(string evolutionURL)
        {
            return DeserializeEvolutionJson(ReturnWebRequest(evolutionURL));
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
