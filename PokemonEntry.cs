namespace PokeDex
{

    public class PokemonEntry
    {
        public Ability[] abilities { get; set; }
        public Form[] forms { get; set; }
        public int height { get; set; }
        public int id { get; set; }
        public string name { get; set; }
        public Species species { get; set; } //Use this to make the species call?
        public Type[] types { get; set; }
        public int weight { get; set; }
    }

    public class Species
    {
        public string name { get; set; }
        public string url { get; set; }
    }

    public class Ability
    {
        public Ability1 ability { get; set; }
        public bool is_hidden { get; set; }
        public int slot { get; set; }
    }

    public class Ability1
    {
        public string name { get; set; }
        public string url { get; set; }
    }

    public class Form
    {
        public string name { get; set; }
        public string url { get; set; }
    }

    public class Type
    {
        public int slot { get; set; }
        public Type1 type { get; set; }
    }

    public class Type1
    {
        public string name { get; set; }
        public string url { get; set; }
    }


}