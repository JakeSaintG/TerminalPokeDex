using Newtonsoft.Json;
using System;
using System.Collections;

namespace PokeDex.Models
{

    public class PokeList
    {
        [JsonProperty(PropertyName = "results")]
        public Result[] Results { get; set; }
    }

    public class Result
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
        //[JsonProperty(PropertyName = "url")]
        //public string Url { get; set; }
    }

    //Maybe store the list in a dictionary so I can assign the returned data to each pokemon
    //This will allow the pokedex entry to be stored when the user enters so, if they type in the same pokemon again, it can be retrieved without making another API call
}
