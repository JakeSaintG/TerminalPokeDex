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
            Dictionary<List<string>, string> filter2 = new Dictionary<List<string>, string>()
            {
                {new List<string> { "jr", "" }, "mime-jr"},
                {new List<string> { "farfet", "-galar" }, "farfetchd-galar"},
                {new List<string> { "farfet", "" }, "farfetchd"},
                {new List<string> { "pumpkaboo", "" }, "pumpkaboo-average"},
                {new List<string> { "gourgeist", "" }, "gourgeist-average"},
                {new List<string> { "rime", "mr" }, "mr-rime"},
                {new List<string> { "porygon", "2" }, "porygon2"},
                {new List<string> { "porygon", "z" }, "porygon-z"},
                {new List<string> { "type", "null" }, "type-null"},
                {new List<string> { "ho", "oh" }, "ho-oh"},
                {new List<string> { "mime", "-galar" }, "mr-mime-galar"},
                {new List<string> { "mime", "mr" }, "mr-mime"}
            };
            int counter = 0;
            foreach (var key in filter2.Keys)
            {          
                if (enteredPokemon.Contains(key[0]) && enteredPokemon.Contains(key[1]))
                {
                 
                    enteredPokemon = filter2.ElementAt(counter).Value;
                }
                else
                {
                    counter++;
                }
            }
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

            Dictionary<int, string> formattedNames = new Dictionary<int, string>()
            {
                {29, $"{alterName}♀"},
                {32, $"{alterName}♂"},
                {474, $"{alterName}-Z"},
                {250, $"{alterName}-oh"},
                {439, $"{alterName} Jr."},
                {122, $"{alterName}. Mime"},
                {10165, "Galarian Mr. Mime"},
                {866, $"{alterName}. Rime"},
                {83, "Farfetch'd"},
                {10163, "Galarian Farfetch'd"},
                {784, "Kommo-o"},
                {783, "Hakamo-o"},
                {782, "Jangmo-o"},
                {772, "Type: Null"},
                {10178, $"{alterName} Low Key"},
                {10220, $"{alterName} Low Key"},
                {849, $"{alterName} Amped"},
                {10210, $"{alterName} Amped"},
            };
            if (formattedNames.Keys.Contains(pokemonEntry.Id))
            {
                alterName = formattedNames[pokemonEntry.Id];
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
