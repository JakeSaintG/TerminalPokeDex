using System;
using System.Linq;
using System.Collections.Generic;

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

                quit = Settings.CheckForQuit(entry);
                if (quit == false){
                    Console.WriteLine("Goodbye! Thank you for utilizing my services!");
                    break;
                }

                //opens the settings menu.
                if (entry == "settings")
                {
                    Settings.AlterSettings();
                    continue;
                }
                
                //Sends the user's input for filtering to make it compatible with PokeAPI
                entry = Formatting.FilterNameEntry(entry);

                if (entry != "")
                {
                    //Displays possible matches from the list of Pokemon gathered at startup.
                    entry = Print.DisplayOptions(entry, pokeList);
                }

                //If anything matches the list of Pokemon gathered at start-up, this will print it to the console.
                //If nothing matches, it will prompt the user to try again.
                
                if (pokeList.Results.Any(p => p.Name == entry) && !entry.Contains("cap") && !entry.Contains("totem"))
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    var pokemonMainEntry = APICall.GetEntry(entry);
                    var pokemonSpeciesEntry = APICall.GetEntrySpecies(pokemonMainEntry.Species.Url);
                    var pokemonEvolutionEntry = APICall.GetEntryEvolutionChain(pokemonSpeciesEntry.EvolutionChain.Url);
                    Console.WriteLine(Print.PrintPokemonPokedexEntry(pokemonMainEntry, pokemonSpeciesEntry, pokemonEvolutionEntry, entry));
                    Settings.CheckColors(ConsoleColor.Cyan);
                }
                else 
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("Hmm..I would appear that I don't know about this Pokemon.\r\nPerhaps check your spelling and try again.\r\n");
                    Settings.CheckColors(ConsoleColor.Cyan);
                }         
            }
        }
    }
}
