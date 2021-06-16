using System;

namespace PokeDex
{

    public class PokeList //: IComparable<PokeList>                 May want to sort this? And use BinarySearch() to increase find times?
    {
        public Result[] results { get; set; }
    }

    public class Result
    {
        public string name { get; set; }
        public string url { get; set; }
    }
}
