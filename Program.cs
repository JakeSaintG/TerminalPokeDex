﻿using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace PokeDex
{

    class Program
    {
        static void Main(string[] args)
        {
            if (Console.BackgroundColor == ConsoleColor.Black)
            {
                Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Clear();
            }

            string initalUrl = "https://pokeapi.co/api/v2/pokemon?limit=1283";
            var rawPokeInfo = APICall.GetPokemonInfo(initalUrl);
            var pokeList = APICall.DeserializePokemon(rawPokeInfo);      

            Console.WriteLine("Hello, my name is Dexter.");
            Console.WriteLine("I'm here to give you more information regarding the wonderful world of Pokemon!");
            Console.WriteLine("For settings, enter \"settings\".");

            Console.WriteLine("If you are done, enter \"quit\" to stop searching for Pokemon and close me down.");

            while(true)
            { 
                Console.Write("What Pokemon do you want more information on? ");
                if (Settings.DefaultConsole == false)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                }
                string entry = Console.ReadLine().ToLower();
                if (Settings.DefaultConsole == false)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                }
                

                if (entry == "quit" || entry == "exit" || entry == "stop")
                {
                    Console.WriteLine("Goodbye! Thanks for playing!");
                    break;
                }
                if (entry == "settings")
                {
                    Settings.AlterSettings();
                }

                //var filteredEntry = entry.FilterName()
                //MAYBE! Change it to a Dictionary to have the key/items methods
                //if (pokeList.Contains(entry))
                //{
                //    Console.Write("We have info " + entry + "! We are still in development. Give us a little bit to format your result for " + entry + "and check back later!");
                //    //var sendEntry = entry.MakeApiCall()
                //}

                //if (pokeList.)

                foreach (var item in pokeList) 
                {
                    if (entry == item.Name)
                    {
                        Console.ForegroundColor = ConsoleColor.White;

                        var getPokemonEntryUrl = "https://pokeapi.co/api/v2/pokemon/" + entry;
                        var sendEntry = APICall.GetPokemonInfo(getPokemonEntryUrl);
                        var foo = APICall.DeSerializeEntryJson(sendEntry);

                        /*========================Move to Print Method after testing========================*/
                        string pokemonType;
                        if (foo.Types.Length == 1)
                        {
                            pokemonType = foo.Types[0].Type.Name;
                            pokemonType = char.ToUpper(pokemonType[0]) + pokemonType.Substring(1);
                        }
                        else
                        {
                            pokemonType = foo.Types[0].Type.Name;
                            pokemonType = char.ToUpper(pokemonType[0]) + pokemonType.Substring(1);
                            string pokemonSecondType = foo.Types[1].Type.Name;
                            pokemonSecondType = char.ToUpper(pokemonSecondType[0]) + pokemonSecondType.Substring(1);
                            pokemonType = $"{pokemonType}/{pokemonSecondType}";
                        }

                        double pokemonHeight = foo.Height / 10;
                        double pokemonWeight = foo.Weight / 10;
                        string pokemonMeasure;

                        if (Settings.EmperialMeasureSetting == true)
                        {
                            pokemonHeight = pokemonHeight * 39.370;
                            pokemonWeight = pokemonWeight * 35.274;
                            int pounds = (int)pokemonWeight / 16;
                            int ouncesLeft = (int)pokemonWeight % 16;
                            int feet = (int)pokemonHeight / 12;
                            int inchesLeft = (int)pokemonHeight % 12;
                            pokemonMeasure = $"Height: {feet}ft {inchesLeft}in | Weight: {pounds}lb {ouncesLeft}oz";
                        }
                        else
                        {
                            pokemonMeasure = $"Height: {pokemonHeight}M | Weight: {pokemonWeight}Kg";
                        }
                         

                        string pokemonName = foo.Name;
                        pokemonName = char.ToUpper(pokemonName[0]) + pokemonName.Substring(1);
                        Console.WriteLine($"\r\nName: {pokemonName} | {pokemonMeasure} | No. #{foo.Id} | {pokemonType} |\r\n" );
                        /*========================Move to Print Method after testing========================*/

                        if (Settings.DefaultConsole == false)
                        {
                            Console.ForegroundColor = ConsoleColor.Cyan;
                        }
 
                    }
                    //else: Need to figure out how to handle the error of a misspelling 
                }
            }

            
            /*==============================================================================================================
            **Project requirement** Create a dictionary or list, populate it with several values, retrieve at least one value, and use it in your program
            **Project requirement** Connect to an external/3rd party API and read data into your app
            ***Project requirement** Read data from an external file, such as text, JSON, CSV, etc and use that data in your application
            ==============================================================================================================*/
            //Call pokeAPI and store ALL Pokemon names in an array OR !!!List<string>!!! from https://pokeapi.co/api/v2/pokemon ()

            //Say "Hello, my name is Dexter".
            //"If you are done, enter "exit" to stop searching for Pokemon and close me down."
            //"What Pokemon do you want more information on?"

            /*==============================================================================================================
            **Project requirement** Implement a “master loop” console application where the user can repeatedly enter commands / perform actions, including choosing to exit the program
            ==============================================================================================================*/
            //Start loop. 
                //Readline for input
                /*==============================================================================================================
                **MAYBE ? Project requirement; need to learn more**Use a LINQ query to retrieve information from a data structure(such as a list or array) or file
                ==============================================================================================================*/
                //Send input for testing (catch misspelling, ToLower(), Closest match with regex?maybe?, etc)
                //receive back from testing/catching and send Pokemon.name to PokeAPI
                    //https://pokeapi.co/api/v2/pokemon-species/1/ (for some details, evo-chain link, and varieties/mega)
                    //https://pokeapi.co/api/v2/pokemon/mewtwo (for rest of details)
                    //https://pokeapi.co/api/v2/evolution-chain/1/ (retrieved from pokemon-species call for evo-chain)
                    //!!!!!!!!!!May run into issue with this. HttpClient seems to be intended to be instantiated once per application, rather than per-use
                //Receive info back from PokeAPI and send to a method for formatting.
                //Maybe: store a file of searched pokemon using system.io (CreateFile)




            //Formatting method.
                //Will need a name-formatting method for input
                    //Will have to handle user input of pokemon with special characters in name "Ho-oh" "Farfetch'd" "Mr. Mime"
                //Will need a name-formatting method for output
                    //Will have to fill remaining space after name with "===" to keep uniform; "feraligatr" is max char length(10) in game so "Ditto===="
                    //Will have spit out pokemon names properly formatted ("hooh"=>"Ho-oh") ("farfetchd"=>"Farfetch'd") ("mrmime"=>"Mr. Mime")

                //Do some ASCII "art" to make it look nice
                //ex: 
                    /*==========================================
                      See "DreamFormat.txt" for formatting goal
                    ==========================================*/

                //Formatting notes
                    //Will have to change between singular and plural; (Type or Types)
                    //Will have to pull description from https://pokeapi.co/api/v2/pokedex/1/ (national pokedex)
                    //Will have to pull evolutions from included evolution-chain link in API https://pokeapi.co/api/v2/evolution-chain/1/
                        //If evolution chain contains one value, don't display it...hopefully, I'll figure out how to not call evo-chain if not needed. PokeAPI has an "evolution-chain" for every pokemon (probably to future proof it) so this may not be possible from the 
                    //Will have to wrap description if it exceeds character length of Heading "Name=======etc"

        }
    }
}
