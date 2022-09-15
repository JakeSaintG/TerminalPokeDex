using Newtonsoft.Json;
using System.Collections.Generic;

namespace PokeDex.Models
{
    public class SpecialFormsDecriptions
    {
        [JsonProperty(PropertyName = "special-forms")]
        public List<SpecialForms> Specialforms { get; set; }
    }

    public class SpecialForms
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
        [JsonProperty(PropertyName = "fixed-description")]
        public string FixedDescription { get; set; }
    }

}
