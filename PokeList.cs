using Newtonsoft.Json;
using System;

namespace PokeDex
{

    public class PokeList //: IComparable<PokeList>                 May want to sort this? And use BinarySearch() to increase find times?
    {
        [JsonProperty(PropertyName = "results")]
        public Result[] Results { get; set; }
    }

    public class Result
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }
    }
}
