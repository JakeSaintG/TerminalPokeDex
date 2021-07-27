using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using static System.Globalization.CultureInfo;


namespace PokeDex
{
    class Formatting
    {
        public static string GetUserInput()
        {
            return Console.ReadLine().ToLower();
        }

        public static bool CheckForQuit(string entry)
        {
            var quitCommands = new List<string> { "stop", "exit", "quit", "q" };
            if (quitCommands.Any(str => str.Contains(entry)))
            {
                Console.WriteLine("Goodbye! Thank you for utilizing my services!");
                return false;
            }
            else
            {
                return true;
            }
        }

        public static string DisplayOptions(string entry, PokeList pokeList)
        {
            var skipForms = new List<string> { "-small", "-super", "-average", "-large", "-battle-bond", "-ash" };
            var displayOptions = new List<Result>(pokeList.Results.Where(p => p.Name.Contains(entry)));
            //Goal: If any of the names in displayOptions contain any of the strings in skipForms, then skip displaying options and send the fist item in displayOptions to API

            if (displayOptions.Count > 1)
            {
                Console.WriteLine("\r\nYour entry matches a few options. Which one do you want?\r\n");
                foreach (var item in displayOptions)
                {

                    Console.WriteLine(item.Name);

                }
                Settings.CheckColors(ConsoleColor.Yellow);
                entry = GetUserInput();
                Settings.CheckColors(ConsoleColor.Cyan);
                return FilterNameEntry(entry);
            }
            else 
            {
                return entry;
            }
        }

        public static string FormatProperNouns(string name)
        {
            var info = CurrentCulture.TextInfo;
            name = info.ToTitleCase(name.Replace("-", " "));
            return name;
        }

        public static string FilterNameEntry(string enteredPokemon)
        {
            if (enteredPokemon == "")
            {

                //If the user did not put anything into the submission input, generate a MissingNo Error.
            };
            if (enteredPokemon.Contains("farfet"))
            {
                enteredPokemon = "farfetchd";
            }
            else if (enteredPokemon.Contains("jr"))
            {
                enteredPokemon = "mime-jr";
            }
            else if (enteredPokemon.Contains("rime") && enteredPokemon.Contains("mr"))
            {
                enteredPokemon = "mr-rime";
            }
            else if (enteredPokemon.Contains("porygon") && enteredPokemon.Contains("2"))
            {
                enteredPokemon = "porygon2";
            }
            else if (enteredPokemon.Contains("porygon") && enteredPokemon.Contains("z"))
            {
                enteredPokemon = "porygon-z";
            }
            else if (enteredPokemon.Contains("mime") && enteredPokemon.Contains("mr"))
            {
                enteredPokemon = "mr-mime";
            }
            else if (enteredPokemon.Contains("type") && enteredPokemon.Contains("null"))
            {
                enteredPokemon = "type-null";
            }
            //else if gourgeist or pumpkaboo, just display the first one "pokemon-average"
            else if (enteredPokemon.Contains("ho") && enteredPokemon.Contains("oh"))
            {
                enteredPokemon = "ho-oh";
            }
            else {/*Do nothing*/};
            return enteredPokemon;
        }

        public static string FormatNameOuput(PokemonEntry pokemonEntry)
        {
            var alterName = pokemonEntry.Name;
            if (alterName.Contains("-"))
            {
                alterName = alterName.Substring(0, alterName.IndexOf("-"));
                // Gets a substring from the beginning of the string to the first instance of the character "-". 
                // Removes hyphens and other form info from the end of the pokemon name.
            };
            if (pokemonEntry.Id == 29)
            {
                alterName = $"{alterName}♀";
            }; // Alters the name of nidoran to the correct Dex form of "Nidoran♀".
            if (pokemonEntry.Id == 32)
            {
                alterName = $"{alterName}♂";
            }; // Alters the name of nidoran to the correct Dex form of "Nidoran♂".
            if (pokemonEntry.Id == 474)
            {
                alterName = $"{alterName}-Z";
            }; // Fixes Porygon-Z's name
            if (pokemonEntry.Id == 250)
            {
                alterName = $"{alterName}-oh";
            }; // Attempt to fix Ho-Oh's name
            if (pokemonEntry.Id == 439)
            {
                alterName = $"{alterName} Jr.";
            }; // Fixes Mime Jr.'s name
            if (pokemonEntry.Id == 122)
            {
                alterName = $"{alterName}. Mime";
            }; // Fixes Mr. Mime's name
            if (pokemonEntry.Id == 10165)
            {
                alterName = $"{alterName}. Mime";
            }; // Fixes Galar Mr. Mime's name
            if (pokemonEntry.Id == 866)
            {
                alterName = $"{alterName}. Rime";
            }; // Fixes Mr. Rime's name
            if (pokemonEntry.Id == 83)
            {
                alterName = "Farfetch'd";
            }; // Fixes Farfetch'd's name
            if (pokemonEntry.Id == 10163)
            {
                alterName = "Farfetch'd";
            }; // Fixes Galar Farfetch'd's name
            if (pokemonEntry.Id == 784)
            {
                alterName = "Kommo-o";
            }; // Fixes name
            if (pokemonEntry.Id == 783)
            {
                alterName = "Hakamo-o";
            }; // Fixes name
            if (pokemonEntry.Id == 782)
            {
                alterName = "Jangmo-o";
            }; // Fixes name
            if (pokemonEntry.Id == 772)
            {
                alterName = "Type: Null";
            }; // Fixes name
            return alterName;
        }

        public static string GetActualDescription(string entry)
        {
            string path = Directory.GetCurrentDirectory();
            DirectoryInfo directory = new DirectoryInfo(path);
            var fileName = Path.Combine(directory.FullName, "formExceptions.json");
            var json = ReadFile(fileName);
            var forms = APICall.DeserializeSpecialJson(json);
            forms.Specialforms.RemoveAll(f => f.Name != entry);
            var formName = forms.Specialforms[0].Name;
            var description = forms.Specialforms[0].FixedDescription;
            return description;


            //darmanitan (zen, zen-galar, etc) may be a pain

            //kyurem has different entries for white/black forms

            //Toxtricty may be an issue...

            //alcreme has 100000 entries for each flavor and one for g-max (g-max is listed first...)

            //urshifu may also be a pain

            //farfetched may also be a pain :/
        }

        public static string ReadFile(string fileName)
        {
            using (var reader = new StreamReader(fileName))
            {
                return reader.ReadToEnd();
            }
        }
    }
}
