using Newtonsoft.Json;
using System;
using System.Collections;

namespace PokeDex
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
}
