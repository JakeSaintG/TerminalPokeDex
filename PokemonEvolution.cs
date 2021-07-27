namespace PokeDex
{
    public class PokemonEvolution
    {
        public object baby_trigger_item { get; set; }
        public Chain chain { get; set; }
        public int id { get; set; }
    }

    public class Chain
    {
        public object[] evolution_details { get; set; }
        public Evolves_To[] evolves_to { get; set; }
        public bool is_baby { get; set; }
        public Species species { get; set; }
    }

    public class EvolutionSpecies
    {
        public string name { get; set; }
        public string url { get; set; }
    }

    public class Evolves_To
    {
        public Evolves_To1[] evolves_to { get; set; }
        public bool is_baby { get; set; }
        public Species1 species { get; set; }
    }

    public class Species1
    {
        public string name { get; set; }
        public string url { get; set; }
    }

    public class Evolves_To1
    {
        public object[] evolves_to { get; set; }
        public bool is_baby { get; set; }
        public Species2 species { get; set; }
    }

    public class Species2
    {
        public string name { get; set; }
        public string url { get; set; }
    }

}
