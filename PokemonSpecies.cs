using Newtonsoft.Json;
using System.Collections.Generic;

public class PokemonSpecies
{
    [JsonProperty(PropertyName = "color")]
    public Color Color { get; set; }
    [JsonProperty(PropertyName = "evolution_chain")]
    public EvolutionChain EvolutionChain { get; set; }
    [JsonProperty(PropertyName = "evolves_from_species")]
    public object EvolvesFromSpecies { get; set; }
    [JsonProperty(PropertyName = "flavor_text_entries")]
    public List<Flavor_Text_Entries> FlavorTextEntries { get; set; }
    [JsonProperty(PropertyName = "form_descriptions")]
    public object[] FormsDescriptions { get; set; }
    [JsonProperty(PropertyName = "forms_switchable")]
    public bool FormsSwitchable { get; set; }
    [JsonProperty(PropertyName = "gender_rate")]
    public int GenderRate { get; set; }
    [JsonProperty(PropertyName = "genera")]
    public Genera[] Genera { get; set; }
    [JsonProperty(PropertyName = "generation")]
    public Generation Generation { get; set; }
    [JsonProperty(PropertyName = "growth_rate")]
    public Growth_Rate GrowthRate { get; set; }
    [JsonProperty(PropertyName = "habitat")]
    public Habitat Habitat { get; set; }
    [JsonProperty(PropertyName = "has_gender_differences")]
    public bool HasGenderDifferences { get; set; }
    [JsonProperty(PropertyName = "hatch_counter")]
    public int HatchCounter { get; set; }
    [JsonProperty(PropertyName = "id")]
    public int ID { get; set; }
    [JsonProperty(PropertyName = "is_baby")]
    public bool IsBaby { get; set; }
    [JsonProperty(PropertyName = "is_legendary")]
    public bool IsLegendary { get; set; }
    [JsonProperty(PropertyName = "is_mythical")]
    public bool IsMythical { get; set; }
    [JsonProperty(PropertyName = "name")]
    public string Name { get; set; }
    [JsonProperty(PropertyName = "names")]
    public Name[] Names { get; set; }
    [JsonProperty(PropertyName = "order")]
    public int Order { get; set; }
    [JsonProperty(PropertyName = "pal_park_encounters")]
    public Pal_Park_Encounters[] PalParkEncounters { get; set; }
    [JsonProperty(PropertyName = "pokedex_numbers")]
    public Pokedex_Numbers[] PokedexNumbers { get; set; }
    [JsonProperty(PropertyName = "shape")]
    public Shape Shape { get; set; }
    [JsonProperty(PropertyName = "varieties")]
    public Variety[] Varieties { get; set; }
}

public class Color
{
    [JsonProperty(PropertyName = "name")]
    public string Name { get; set; }
    [JsonProperty(PropertyName = "url")]
    public string Url { get; set; }
}

public class EvolutionChain
{
    [JsonProperty(PropertyName = "url")]
    public string Url { get; set; }
}

public class Generation
{
    [JsonProperty(PropertyName = "name")]
    public string Name { get; set; }
    [JsonProperty(PropertyName = "url")]
    public string Url { get; set; }
}

public class Growth_Rate
{
    [JsonProperty(PropertyName = "name")]
    public string Name { get; set; }
    [JsonProperty(PropertyName = "url")]
    public string Url { get; set; }
}

public class Habitat
{
    [JsonProperty(PropertyName = "name")]
    public string Name { get; set; }
    [JsonProperty(PropertyName = "url")]
    public string Url { get; set; }
}

public class Shape
{
    [JsonProperty(PropertyName = "name")]
    public string Name { get; set; }
    [JsonProperty(PropertyName = "url")]
    public string Url { get; set; }
}

public class Egg_Groups
{
    [JsonProperty(PropertyName = "name")]
    public string Name { get; set; }
    [JsonProperty(PropertyName = "url")]
    public string Url { get; set; }
}

public class Flavor_Text_Entries
{
    [JsonProperty(PropertyName = "flavor_text")]
    public string FlavorText { get; set; }
    [JsonProperty(PropertyName = "language")]
    public Language Language { get; set; }
    [JsonProperty(PropertyName = "version")]
    public Version Version { get; set; }
}

public class Language
{
    [JsonProperty(PropertyName = "name")]
    public string Name { get; set; }
    [JsonProperty(PropertyName = "url")]
    public string Url { get; set; }
}

public class Version
{
    [JsonProperty(PropertyName = "name")]
    public string Name { get; set; }
    [JsonProperty(PropertyName = "url")]
    public string Url { get; set; }
}

public class Genera
{
    [JsonProperty(PropertyName = "genus")]
    public string Genus { get; set; }
    [JsonProperty(PropertyName = "language")]
    public Language1 Language { get; set; }
}

public class Language1
{
    [JsonProperty(PropertyName = "name")]
    public string Name { get; set; }
    [JsonProperty(PropertyName = "genus")]
    public string Url { get; set; }
}

public class Name
{
    [JsonProperty(PropertyName = "language")]
    public Language2 Language { get; set; }
    [JsonProperty(PropertyName = "name")]
    public string LangName { get; set; }
}

public class Language2
{
    [JsonProperty(PropertyName = "name")]
    public string Name { get; set; }
    [JsonProperty(PropertyName = "url")]
    public string Url { get; set; }
}

public class Pal_Park_Encounters
{
    [JsonProperty(PropertyName = "area")]
    public Area Area { get; set; }
    [JsonProperty(PropertyName = "base_score")]
    public int BaseScore { get; set; }
    [JsonProperty(PropertyName = "rate")]
    public int Rate { get; set; }
}

public class Area
{
    [JsonProperty(PropertyName = "name")]
    public string Name { get; set; }
    [JsonProperty(PropertyName = "url")]
    public string Url { get; set; }
}

public class Pokedex_Numbers
{
    [JsonProperty(PropertyName = "entry_number")] 
    public int EntryNumber { get; set; }
    [JsonProperty(PropertyName = "pokedex")]
    public Pokedex Pokedex { get; set; }
}

public class Pokedex
{
    [JsonProperty(PropertyName = "name")]
    public string Name { get; set; }
    [JsonProperty(PropertyName = "url")]
    public string Url { get; set; }
}

public class Variety
{
    [JsonProperty(PropertyName = "is_default")]
    public bool IsDefault { get; set; }
    [JsonProperty(PropertyName = "pokemon")]
    public Pokemon Pokemon { get; set; }
}

public class Pokemon
{
    [JsonProperty(PropertyName = "name")]
    public string Name { get; set; }
    [JsonProperty(PropertyName = "url")]
    public string Url { get; set; }
}


