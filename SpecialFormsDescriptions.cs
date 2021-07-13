using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokeDex
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
