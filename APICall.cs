using System;
using Newtonsoft.Json;
using System.Net;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using System.Linq;

namespace PokeDex
{
    public class APICall
    {     
        public static string GetPokemonInfo(string url)
        {
            var webClient = new WebClient();
            try
            {
                byte[] data = webClient.DownloadData(url);
                using (var stream = new MemoryStream(data))
                using (var reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
            catch (WebException)
            {
                string fail = "failed";
                Console.WriteLine("Huh...It looks like I have encountered an error.\r\n" +
                "Perhaps an unreliable internet connection?\r\n" +
                "This application depends on a connection to PokeAPI.\r\n");
                return fail;
            }   
        }
        public static PokeList DeserializePokemon(string json)
        {
            return JsonConvert.DeserializeObject<PokeList>(json);
        }
        public static PokemonEntry DeSerializeEntryJson(string json) 
        { 
            return JsonConvert.DeserializeObject<PokemonEntry>(json); 
        }

        public static PokemonSpecies DeSerializeSpeciesJson(string json)
        {
            return JsonConvert.DeserializeObject<PokemonSpecies>(json);
        }

        public static SpecialFormsDecriptions DeSerializeSpecialJson(string json)
        {
            return JsonConvert.DeserializeObject<SpecialFormsDecriptions>(json);
        }
    }
}
