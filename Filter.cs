namespace PokeDex
{
    class Filter
    {
        //Maybe... Flip Mewtwo and Mew in the pokeList to make it easier to pull the right one?
        //I wrote a bunch of spagetti to make this work in JS when I should have just flipped them

        public static string FilterEntry(string enteredPokemon)
        {
            if (enteredPokemon == "")
            {
                
                //If the user did not put anything into the submission input, generate a MissingNo Error.
            };
            string name = enteredPokemon.ToLower();
            if (name.Contains("farfet"))
            {
                name = "farfetchd";
            }
            else if (name.Contains("jr"))
            {
                name = "mime-jr";
            }
            else if(name.Contains("rime") && name.Contains("mr"))
            {
                name = "mr-rime";
            }
            else if (name.Contains("mime") && name.Contains("mr"))
            {
                name = "mr-mime";
            }
            else if (name.Contains("type") && name.Contains("null"))
            {
                name = "type-null";
            }
            else if (name.Contains("ho") && name.Contains("oh"))
            {
                name = "ho-oh";
            }
            else 
            {
                name = enteredPokemon;
            };
            return name;
        }
    }


}
