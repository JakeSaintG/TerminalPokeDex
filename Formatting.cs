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

        public static string FormatProperNouns(string name)
        {
            var info = CurrentCulture.TextInfo;
            name = info.ToTitleCase(name.Replace("-", " "));
            return name;
        }
        
        //Handles certain pokemon with names that are most commonly mis-input.
        public static string FilterNameEntry(string enteredPokemon)
        {
            if (enteredPokemon.Contains("farfet") && enteredPokemon.Contains("-galar"))
            {
                enteredPokemon = "farfetchd-galar";
            }
            else if (enteredPokemon.Contains("farfet"))
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
            else if (enteredPokemon.Contains("mime") && enteredPokemon.Contains("-galar"))
            {
                enteredPokemon = "mr-mime-galar";
            }
            else if (enteredPokemon.Contains("mime") && enteredPokemon.Contains("mr"))
            {
                enteredPokemon = "mr-mime";
            }
            else if (enteredPokemon.Contains("type") && enteredPokemon.Contains("null"))
            {
                enteredPokemon = "type-null";
            }
            else if (enteredPokemon.Contains("pumpkaboo") || enteredPokemon.Contains("gourgeist"))
            {
                enteredPokemon += "-average";
            }
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
            var specialName = new List<string> { "-galar", "-alola", "-gmax", "-mega" };
            if (specialName.Any(a => a.Contains(a)))
            {
                if (alterName.Contains("-alola"))
                {
                    alterName = $"Alolan {alterName}";
                }
                if (alterName.Contains("-galar"))
                {
                    alterName = $"Galarian {alterName}";
                }
                if (alterName.Contains("-gmax"))
                {
                    alterName = $"Gigantamax {alterName}";
                }
                if (alterName.Contains("-mega"))
                {
                    alterName = $"Mega {alterName}";                
                }
            } 
            if (alterName.Contains("-"))
            {
                if (alterName.Contains("-x"))
                {
                    alterName = $"{alterName.Substring(0, alterName.IndexOf("-"))} X";
                }
                else if (alterName.Contains("-y"))
                {
                    alterName = $"{alterName.Substring(0, alterName.IndexOf("-"))} Y";
                }
                else
                {
                    alterName = alterName.Substring(0, alterName.IndexOf("-"));
                }              
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
                alterName = $"Galarian Mr. Mime";
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
                alterName = "Galarian Farfetch'd";
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
            if (pokemonEntry.Id == 10178 || pokemonEntry.Id == 10220)
            {
                alterName += " Low Key";
            }
            if (pokemonEntry.Id == 849 || pokemonEntry.Id == 10210)
            {
                alterName += " Amped";
            }
            return alterName;
        }
        
        //PokeAPI's descriptions are not organized in way that allows for differentiation between a description for a base form Pokemon and a special form pokemon.
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
        }

        //Wraps the text for the description so that it can fit in the "box" of the printed entry.
        public static string WrapText(string textToWrap)
        {
            string wrappedText = "";
            int counter = 0;
            foreach (var character in textToWrap)
            {
                counter++;
                if (counter >= 65)
                {
                    if (character == ' ')
                    {
                        string replaceChar = character.ToString();
                        wrappedText += replaceChar.Replace(" ", "\r\n||");
                        counter = 0;
                    }
                    else
                    {
                        wrappedText += character;
                    }
                }
                else
                {
                    wrappedText += character;
                }
            }
            return wrappedText;
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
