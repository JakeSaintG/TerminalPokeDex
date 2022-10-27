using Newtonsoft.Json;

namespace PokeDex.Models
{
    public class PokemonEntry
    {
        [JsonProperty(PropertyName = "abilities")]
        public Abilities[] Abilities { get; set; }

        [JsonProperty(PropertyName = "forms")]
        public Form[] Forms { get; set; }

        [JsonProperty(PropertyName = "height")]
        public double Height { get; set; }

        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "species")]
        public Species Species { get; set; }

        [JsonProperty(PropertyName = "types")]
        public Types[] Types { get; set; }

        [JsonProperty(PropertyName = "weight")]
        public double Weight { get; set; }
    }

    public class Species
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }
    }

    public class Abilities
    {
        [JsonProperty(PropertyName = "ability")]
        public Ability Ability { get; set; }

        [JsonProperty(PropertyName = "is_hidden")]
        public bool Is_hidden { get; set; }

        [JsonProperty(PropertyName = "slot")]
        public int Slot { get; set; }
    }

    public class Ability
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }
    }

    public class Form
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }
    }

    public class Types
    {
        [JsonProperty(PropertyName = "slot")]
        public int Slot { get; set; }

        [JsonProperty(PropertyName = "type")]
        public Type Type { get; set; }
    }

    public class Type
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }
    }
}