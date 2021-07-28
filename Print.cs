using System.Collections.Generic;
using System.Linq;
using static System.Globalization.CultureInfo;

namespace PokeDex
{
    class Print
    {
        private static string PrintPokemonName(PokemonEntry pokemonMainEntry, PokemonSpecies pokemonSpeciesEntry) 
        {
            string pokemonName = pokemonMainEntry.Name;
            var info = CurrentCulture.TextInfo;
            pokemonName = info.ToTitleCase(Formatting.FormatNameOuput(pokemonMainEntry));

            //This fills the space between the end of the Pokemon's name and the beginning of it's Dex Number.
            //Makes sure that the bottom and top are of even length.
            int totalLength = pokemonName.Length + pokemonSpeciesEntry.ID.ToString().Length;
            for (int i = totalLength; i <= 61; i++)
            {
                pokemonName += "=";
            }

            return pokemonName;
        }

        public static string WrapDescriptionText(string textToWrap)
        {
            string wrappedText = "";
            return wrappedText;
        }

        private static string PrintPokemonGeneration(PokemonSpecies pokemonSpeciesEntry)
        {
            //There's a better way to do this but I would like to avoid another call to PokeAPI for something so small:
            //1. Make a call to https://pokeapi.co/api/v2/generation
            //2. Iterate through the array.
            //3. If Generation.Name === i, take the index number, add 1, put it in the string "Generation: ${indexNumber}"

            var gen = pokemonSpeciesEntry.Generation.Name;
            if (gen.Contains("-viii"))
            {
                gen = "Generation: 8";
            }
            else if (gen.Contains("-vii"))
            {
                gen = "Generation: 7";
            }
            else if (gen.Contains("-vi"))
            {
                gen = "Generation: 6";
            }
            else if (gen.Contains("-v"))
            {
                gen = "Generation: 5";
            }
            else if (gen.Contains("-iv"))
            {
                gen = "Generation: 4";
            }
            else if (gen.Contains("-iii"))
            {
                gen = "Generation: 3";
            }
            else if (gen.Contains("-ii"))
            {
                gen = "Generation: 2";
            }
            else if (gen.Contains("-i"))
            {
                gen = "Generation: 1";
            }
            return gen;
        }
        private static string PrintPokemonType(PokemonEntry pokemonMainEntry)
        {
            var info = CurrentCulture.TextInfo; //Need to figure out how to make this a field.....
            string pokemonType = info.ToTitleCase(pokemonMainEntry.Types[0].Type.Name);
            if (pokemonMainEntry.Types.Length != 1)
            {
                string pokemonSecondType = info.ToTitleCase(pokemonMainEntry.Types[1].Type.Name);
                pokemonType = $"{pokemonType}/{pokemonSecondType}";
            }
            return pokemonType;
        }
        private static string PrintPokemonHabitat(PokemonSpecies pokemonSpeciesEntry)
        {
            var pokemonHabitat = "";
            if (pokemonSpeciesEntry.Habitat != null)
            {
                pokemonHabitat = $"Habitat: {pokemonSpeciesEntry.Habitat.Name}";
            }
            return pokemonHabitat;
            //Some Pokemon don't have habitats. This just removes habitat from the output if none exists.
            //Handles: System.NullReferenceException
        }
        private static string PrintPokemonAbilities(PokemonEntry pokemonMainEntry)
        {
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
            return abilities;
        }
        private static string PrintPokemonMeasurements(PokemonEntry pokemonMainEntry)
        {
            string pokemonMeasure;

            double pokemonHeight = pokemonMainEntry.Height / 10;
            double pokemonWeight = pokemonMainEntry.Weight / 10;
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
            return pokemonMeasure;
        }
        private static string PrintPokemonDescription(PokemonSpecies pokemonSpeciesEntry, string entry) 
        {
            pokemonSpeciesEntry.FlavorTextEntries.RemoveAll(f => f.Language.Name != "en");
            var description = pokemonSpeciesEntry.FlavorTextEntries[0].FlavorText;
            var specialDescriptionForms = new List<string> { "-galar", "-alola", "-gmax", "-mega", "rotom", "-black", "-white" };
            var formsList = new List<SpecialForms>();
            //In order for this to work, I probably need to deserialize the forms JSON on startup.
            //if (formsList.Any(s => s.Name.Contains(entry)))
            //{
            //    description = Filter.GetActualDescription(entry);
            //}
            foreach (var item in specialDescriptionForms)
            {
                if (entry.Contains(item))
                {
                    description = Formatting.GetActualDescription(entry);
                }
            }
            return description = description.Replace("\n", " ").Replace("\f", " "); //Make method for removing weird symbols
        }
        private static string PrintPokemonForms(PokemonSpecies pokemonSpeciesEntry)
        {
            string pokemonForms = "";
            int formsCounter = 0;
            if (pokemonSpeciesEntry.Varieties.Length > 1)
            {
                pokemonForms = "||\r\n" + "||Forms: ";
                foreach (var item in pokemonSpeciesEntry.Varieties)
                {
                    formsCounter++;
                    if (formsCounter == pokemonSpeciesEntry.Varieties.Length)
                    {
                        pokemonForms += $"{Formatting.FormatProperNouns(item.Pokemon.Name)}";
                    }
                    else
                    {
                        pokemonForms += $"{Formatting.FormatProperNouns(item.Pokemon.Name)}, ";
                    }
                }
                pokemonForms += "\r\n";
            }
            return pokemonForms;
        }
        private static string PrintPokemonEvolutions(PokemonEvolution pokemonEvolutionEntry)
        {
            var info = CurrentCulture.TextInfo; //Need to figure out how to make this a field.....
            string evolvesTo = "";
            var evolutions = pokemonEvolutionEntry.Chain.EvolvesTo;
            if (evolutions.Length != 0)
            {
                //Handles evolution branching if base pokemon has two possible evolutions that then evolve.
                //It checks for how many evolutions the base has AND if any of those also evolve.
                //(ex: Wurmple to Cascoon OR Silcoon. Then Cascoon to Dustox AND Silcoon to Beautifly.)
                if (evolutions.Length > 1 && evolutions.Any(e => e.EvolvesTo1.Length > 0))
                {
                    evolvesTo = $"||\r\n";
                    foreach (var item in evolutions)
                    {
                        evolvesTo += $"||Evolution chain: {info.ToTitleCase(pokemonEvolutionEntry.Chain.SpeciesEvo.Name)} ===> {info.ToTitleCase(item.Species.Name)} ";
                        foreach (var i in item.EvolvesTo1)
                        {
                            evolvesTo += $"===> {info.ToTitleCase(i.Species.Name)}\r\n";
                        }
                    }
                    evolvesTo += "\r\n";
                }
                else
                //Handles evolution branching if there are no more evolutions after the branch
                //(ex: Slowpoke to Slowbro OR Slowking; Poliwag to Poliwhirl to Poliwrath OR Politoad.).
                //Allows pokemon like Eevee that do not evolve again to be formatted easily.
                {
                    evolvesTo = $"||\r\n||Evolution chain: {info.ToTitleCase(pokemonEvolutionEntry.Chain.SpeciesEvo.Name)} ===> ";
                    foreach (var item in evolutions)
                    {
                        evolvesTo += $"{info.ToTitleCase(item.Species.Name)} ";
                        if (item.EvolvesTo1.Length != 0)
                        {
                            string complexEvolution = "===> ";
                            int count = 0;
                            foreach (var i in item.EvolvesTo1)
                            {
                                count++;
                                if (count == item.EvolvesTo1.Length)
                                {
                                    complexEvolution += $"{info.ToTitleCase(i.Species.Name)} ";
                                }
                                else
                                {
                                    complexEvolution += $"{info.ToTitleCase(i.Species.Name)}, ";
                                }

                            }
                            evolvesTo += complexEvolution;
                        }
                    }
                    evolvesTo += "\r\n";
                }
            }
            return evolvesTo;
        }
        public static string PrintPokemonPokedexEntry(PokemonEntry pokemonMainEntry,PokemonSpecies pokemonSpeciesEntry, PokemonEvolution pokemonEvolutionEntry, string entry)
        {
            string pokemonName = PrintPokemonName(pokemonMainEntry, pokemonSpeciesEntry);
            string pokemonGen = PrintPokemonGeneration(pokemonSpeciesEntry);
            string pokemonType = PrintPokemonType(pokemonMainEntry);
            string pokemonHabitat = PrintPokemonHabitat(pokemonSpeciesEntry);
            string abilities = PrintPokemonAbilities(pokemonMainEntry);
            string pokemonMeasure = PrintPokemonMeasurements(pokemonMainEntry);
            string description = PrintPokemonDescription(pokemonSpeciesEntry, entry);
            string pokemonForms = PrintPokemonForms(pokemonSpeciesEntry);
            string evolvesTo = PrintPokemonEvolutions(pokemonEvolutionEntry);
            string bottomBar = "==================================================================================";
            string completedEntry = $"\r\n{pokemonName}No. {pokemonSpeciesEntry.ID}==={pokemonGen}\r\n" +
                $"||Types: {pokemonType};        Color: {pokemonSpeciesEntry.Color.Name};        {pokemonHabitat}\r\n" +
                $"||\r\n" +
                $"||{abilities}\r\n" +
                $"||\r\n" +
                $"||{pokemonMeasure}\r\n" +
                $"||\r\n" +
                $"||Description: {description}\r\n" +
                $"{pokemonForms}" +
                $"{evolvesTo}" +
                $"{bottomBar}\r\n";
            return completedEntry;
        }
    }
}
