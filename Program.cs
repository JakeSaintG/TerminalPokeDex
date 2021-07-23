using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using static System.Globalization.CultureInfo;

namespace PokeDex
{
    class Program
    {
        static void Main(string[] args)
        {
            Settings.SetColors();
            var pokeList = APICall.GetFullList();

            Console.WriteLine("Hello, my name is Dexter.");
            Console.WriteLine("I'm here to give you more information regarding the wonderful world of Pokemon!");
            Console.WriteLine("For settings, enter \"settings\".");
            Console.WriteLine("If you are done, enter \"quit\" to stop searching for Pokemon and close me down.");

            bool quit = true;
            while(quit)
            { 
                Console.Write("What Pokemon do you want more information on? ");
                Settings.CheckColors(ConsoleColor.Yellow);
                string entry = Formatting.GetUserInput();
                Settings.CheckColors(ConsoleColor.Cyan);

                quit = Formatting.CheckForQuit(entry);
                if (quit == false){break;}

                if (entry == "settings")
                {
                    Settings.AlterSettings();
                    continue;
                }
                entry = Formatting.FilterNameEntry(entry);

                //Displays possible matches that the user may be looking for
                entry = Formatting.DisplayOptions(entry, pokeList);

                if (pokeList.Results.Any(p => p.Name == entry))
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    var pokemonMainEntry = APICall.GetEntry(entry);
                    var pokemonSpeciesEntry = APICall.GetEntrySpecies(pokemonMainEntry.Species.Url);
                    var pokemonEvolutionEntry = APICall.GetEntryEvolutionChain(pokemonSpeciesEntry.EvolutionChain.Url);

                    /*========================Move to Print Method after testing========================*/   
                    string pokemonName = pokemonMainEntry.Name;
                    pokemonName = Formatting.FormatNameOuput(pokemonMainEntry);
              
                    var info = CurrentCulture.TextInfo;
                    pokemonName = info.ToTitleCase(pokemonName);
                    string pokemonType;

                    pokemonType = info.ToTitleCase(pokemonMainEntry.Types[0].Type.Name);
                    if (pokemonMainEntry.Types.Length != 1)
                    {
                        string pokemonSecondType = info.ToTitleCase(pokemonMainEntry.Types[1].Type.Name);
                        pokemonType = $"{pokemonType}/{pokemonSecondType}";
                    }

                    var pokemonHabitat = "";
                    if (pokemonSpeciesEntry.habitat != null)
                    {
                        pokemonHabitat = $"Habitat: {pokemonSpeciesEntry.habitat.name}";
                    }
                    //Some Pokemon don't have habitats. This just removes habitat from the output if none exists.
                    //Handles: System.NullReferenceException

                    string abilities = "";
                    if (pokemonMainEntry.Abilities.Length > 1)
                    {
                        abilities = $"Abilities: ";
                    }
                    else
                    {
                        abilities = $"Ability: ";
                    }

                    foreach (var ability in pokemonMainEntry.Abilities)
                    {
                        if (ability.Slot == 1)
                        {
                            abilities += Formatting.FormatProperNouns(ability.Ability.Name);
                        }
                        else
                        {
                            abilities += ", " + Formatting.FormatProperNouns(ability.Ability.Name);
                        }
                    }

                    double pokemonHeight = pokemonMainEntry.Height / 10;
                    double pokemonWeight = pokemonMainEntry.Weight / 10;
                    string pokemonMeasure;
                    if (Settings.EmperialMeasureSetting == true)
                    {
                        pokemonHeight = pokemonHeight * 39.370;
                        pokemonWeight = pokemonWeight * 35.274;
                        int pounds = (int)pokemonWeight / 16;
                        int ouncesLeft = (int)pokemonWeight % 16;
                        int feet = (int)pokemonHeight / 12;
                        int inchesLeft = (int)pokemonHeight % 12;
                        pokemonMeasure = $"Height: {feet}ft {inchesLeft}in Weight: {pounds}lb {ouncesLeft}oz";
                    }
                    else
                    {
                        pokemonMeasure = $"Height: {pokemonHeight}M Weight: {pokemonWeight}Kg";
                    }

                    pokemonSpeciesEntry.flavor_text_entries.RemoveAll(f => f.language.name != "en");
                    var description = pokemonSpeciesEntry.flavor_text_entries[0].flavor_text;

                    var specialForms = new List<string> { "-galar", "-alola", "-gmax", "-mega", "rotom", "-black", "-white" };
                    var formsList = new List<SpecialForms>();

                    //In order for this to work, I probably need to deserialize the forms JSON on startup.
                    //if (formsList.Any(s => s.Name.Contains(entry)))
                    //{
                    //    description = Filter.GetActualDescription(entry);
                    //}
                    foreach (var item in specialForms)
                    {
                        if (entry.Contains(item))
                        {
                            description = Formatting.GetActualDescription(entry);
                        }
                    }
                    description = description.Replace("\n", " ").Replace("\f", " "); //Make method for removing weird symbols

                    string pokemonForms = "";
                    int formsCounter = 0;
                    if (pokemonSpeciesEntry.varieties.Length > 1)
                    {
                        pokemonForms = "||\r\n" + "||Forms: ";
                        foreach (var item in pokemonSpeciesEntry.varieties)
                        {
                            formsCounter++;
                            if (formsCounter == pokemonSpeciesEntry.varieties.Length)
                            {
                                pokemonForms += $"{Formatting.FormatProperNouns(item.pokemon.name)}";
                            }
                            else
                            {
                                pokemonForms += $"{Formatting.FormatProperNouns(item.pokemon.name)}, ";
                            }
                        }
                        pokemonForms += "\r\n"; 
                    }
                          
                    var evolutions = pokemonEvolutionEntry.chain.evolves_to;
                    string evolvesTo = "";

                    if (evolutions.Length != 0)
                    {
                        //Handles evolution branching if base pokemon has two possible evolutions that then evolve.
                        //It checks for how many evolutions the base has AND if any of those also evolve.
                        //(ex: Wurmple to Cascoon OR Silcoon. Then Cascoon to Dustox AND Silcoon to Beautifly.)
                        if (evolutions.Length > 1 && evolutions.Any(e => e.evolves_to.Length > 0))
                        {
                            evolvesTo = $"||\r\n";
                            foreach (var item in evolutions)
                            {
                                evolvesTo += $"||Evolution chain: {info.ToTitleCase(pokemonEvolutionEntry.chain.species.Name)} ===> {info.ToTitleCase(item.species.name)} ";
                                foreach (var i in item.evolves_to)
                                {
                                    evolvesTo += $"===> {info.ToTitleCase(i.species.name)}\r\n";
                                }
                            }
                            evolvesTo += "\r\n";
                        }
                        else
                        //Handles evolution branching if there are no more evolutions after the branch
                        //(ex: Slowpoke to Slowbro OR Slowking; Poliwag to Poliwhirl to Poliwrath OR Politoad.).
                        //Allows pokemon like Eevee that do not evolve again to be formatted easily.
                        {
                            evolvesTo = $"||\r\n||Evolution chain: {info.ToTitleCase(pokemonEvolutionEntry.chain.species.Name)} ===> ";
                            foreach (var item in evolutions)
                            {
                                evolvesTo += $"{info.ToTitleCase(item.species.name)} ";
                                if (item.evolves_to.Length != 0)
                                {
                                    string complexEvolution = "===> ";
                                    foreach (var i in item.evolves_to)
                                    {
                                        complexEvolution += $"{info.ToTitleCase(i.species.name)} ";
                                    }
                                    evolvesTo += complexEvolution;
                                }
                            }
                            evolvesTo += "\r\n";
                        }
                        
                    }
                    Console.WriteLine($"\r\n{pokemonName}================================================No. {pokemonSpeciesEntry.id}==={pokemonSpeciesEntry.generation.name}\r\n" +
                        $"||Types: {pokemonType};        Color: {pokemonSpeciesEntry.Color.name};        {pokemonHabitat}\r\n" +
                        $"||\r\n" +
                        $"||{abilities}\r\n" +
                        $"||\r\n" +
                        $"||{pokemonMeasure}\r\n" +
                        $"||\r\n" +
                        $"||Description: {description}\r\n" +
                        $"{pokemonForms}" + 
                        $"{evolvesTo}" + 
                        $"==================================================================================\r\n");

                    /*========================Move to Print Method after testing========================*/

                    Settings.CheckColors(ConsoleColor.Cyan);
                }
                else 
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("Hmm..I would appear that I don't know about this Pokemon.\r\nPerhaps check your spelling and try again.\r\n");
                    Settings.CheckColors(ConsoleColor.Cyan);
                }

                      
            }


            //Send input for testing (catch misspelling, ToLower(), Closest match with regex?maybe?, etc)

            //Formatting method.
                //Will need a name-formatting method for output
                    //Will have to fill remaining space after name with "===" to keep uniform; "feraligatr" is max char length(10) in game so "Ditto===="
                    //Will have spit out pokemon names properly formatted ("hooh"=>"Ho-oh") ("farfetchd"=>"Farfetch'd") ("mrmime"=>"Mr. Mime")

                //Formatting notes
                    //Will have to change between singular and plural; (Type or Types)
                    //Will have to wrap description if it exceeds character length of Heading "Name=======etc"

        }
    }
}
