using System;

namespace PokeDex
{
    class Program
    {
        static void Main(string[] args)
        {
            /*==============================================================================================================
            **Project requirement** Create a dictionary or list, populate it with several values, retrieve at least one value, and use it in your program
            **Project requirement** Connect to an external/3rd party API and read data into your app
            ***Project requirement** Read data from an external file, such as text, JSON, CSV, etc and use that data in your application
            ==============================================================================================================*/
            //Call pokeAPI and store ALL Pokemon names in an array from https://pokeapi.co/api/v2/pokemon ()

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


            //Catch misspelling
                //By checking userInput against pokemonNames array for a match
                    //morePractice in Week3 folder is good place to start on search/compare method
                //send info back to main loop if it's good
                //If bad, print "I think there may be a spelling error. Please try again."

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
