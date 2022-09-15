using Newtonsoft.Json;
using System.Net;
using System.IO;
using System;
using PokeDex.Models;

namespace PokeDex
{
    public class APICall
    {
        private static string initalUrl = "https://pokeapi.co/api/v2/pokemon?limit=1283";
        private static WebClient webClient = new WebClient();

        //WebRequest attempts to download the requested information.
        //If it fails, it starts a loop that allows the user to retry or quit.
        //If successful, it returns a byte[].
        private static byte[] WebRequest(string url)
        {
            var data = new byte[] { };           
            bool tryAgain = true;
            while (tryAgain)
            {
                try
                {
                    data = webClient.DownloadData(url);
                    tryAgain = false;
                }
                catch (Exception)
                {
                    Console.WriteLine("Hmm...It would appear that I do not have a connection to PokeAPI.");
                    Console.WriteLine("Enter \"quit\" to exit the program. Enter anything to try again.");
                    string errorEntry = Console.ReadLine();             
                    bool quit = Settings.CheckForQuit(errorEntry);
                    if (quit == false) {
                        Console.WriteLine("Goodbye! Thank you for utilizing my services!");
                        Environment.Exit(1);
                    }
                }
            }
            return data;
        }

        //Assigns the returned information to byte[] and builds it out into a string.
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
        private static PokemonEvolution DeserializeEvolutionJson(string json)
        {
            return JsonConvert.DeserializeObject<PokemonEvolution>(json);
        }
        public static SpecialFormsDecriptions DeserializeSpecialJson(string json)
        {
            return JsonConvert.DeserializeObject<SpecialFormsDecriptions>(json);
        }

    }
}
