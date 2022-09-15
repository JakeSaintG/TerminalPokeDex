using Newtonsoft.Json;

namespace PokeDex.Models
{
    public class PokemonEvolution
    {
        [JsonProperty(PropertyName = "baby_trigger_item")]
        public object BabyTriggerItem { get; set; }
        [JsonProperty(PropertyName = "chain")]
        public Chain Chain { get; set; }
        [JsonProperty(PropertyName = "id")]
        public int ID { get; set; }
    }

    public class Chain
    {
        [JsonProperty(PropertyName = "evolution_details")]
        public object[] EvolutionDetails { get; set; }
        [JsonProperty(PropertyName = "evolves_to")]
        public Evolves_To[] EvolvesTo { get; set; }
        [JsonProperty(PropertyName = "is_baby")]
        public bool IsBaby { get; set; }
        [JsonProperty(PropertyName = "species")]
        public Species SpeciesEvo { get; set; }
    }

    public class EvolutionSpecies
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }
    }

    public class Evolves_To
    {
        [JsonProperty(PropertyName = "evolves_to")]
        public Evolves_To1[] EvolvesTo1 { get; set; }
        [JsonProperty(PropertyName = "is_baby")]
        public bool IsBaby { get; set; }
        [JsonProperty(PropertyName = "species")]
        public Species1 Species { get; set; }
    }

    public class Species1
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }
    }

    public class Evolves_To1
    {
        [JsonProperty(PropertyName = "evolves_to")]
        public object[] EvolvesTo { get; set; }
        [JsonProperty(PropertyName = "is_baby")]
        public bool IsBaby { get; set; }
        [JsonProperty(PropertyName = "species")]
        public Species2 Species { get; set; }
    }

    public class Species2
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }
    }

}
