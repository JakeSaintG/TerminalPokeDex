﻿using System.Collections.Generic;
using System.Linq;
using static System.Globalization.CultureInfo;

namespace PokeDex
{
    class Print
    {
        private static string PrintPokemonName(PokemonEntry pokemonMainEntry) 
        {
            string pokemonName = pokemonMainEntry.Name;
            var info = CurrentCulture.TextInfo;
            return pokemonName = info.ToTitleCase(Formatting.FormatNameOuput(pokemonMainEntry));
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
            if (pokemonSpeciesEntry.habitat != null)
            {
                pokemonHabitat = $"Habitat: {pokemonSpeciesEntry.habitat.name}";
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
            pokemonSpeciesEntry.flavor_text_entries.RemoveAll(f => f.language.name != "en");
            var description = pokemonSpeciesEntry.flavor_text_entries[0].flavor_text;
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
            return pokemonForms;
        }
        private static string PrintPokemonEvolutions(PokemonEvolution pokemonEvolutionEntry)
        {
            var info = CurrentCulture.TextInfo; //Need to figure out how to make this a field.....
            string evolvesTo = "";
            var evolutions = pokemonEvolutionEntry.chain.evolves_to;
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
                            int count = 0;
                            foreach (var i in item.evolves_to)
                            {
                                count++;
                                if (count == item.evolves_to.Length)
                                {
                                    complexEvolution += $"{info.ToTitleCase(i.species.name)} ";
                                }
                                else
                                {
                                    complexEvolution += $"{info.ToTitleCase(i.species.name)}, ";
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
            string pokemonName = PrintPokemonName(pokemonMainEntry);
            string pokemonType = PrintPokemonType(pokemonMainEntry);
            string pokemonHabitat = PrintPokemonHabitat(pokemonSpeciesEntry);
            string abilities = PrintPokemonAbilities(pokemonMainEntry);
            string pokemonMeasure = PrintPokemonMeasurements(pokemonMainEntry);
            string description = PrintPokemonDescription(pokemonSpeciesEntry, entry);
            string pokemonForms = PrintPokemonForms(pokemonSpeciesEntry);
            string evolvesTo = PrintPokemonEvolutions(pokemonEvolutionEntry);

            string completedEntry = $"\r\n{pokemonName}================================================No. {pokemonSpeciesEntry.id}==={pokemonSpeciesEntry.generation.name}\r\n" +
                $"||Types: {pokemonType};        Color: {pokemonSpeciesEntry.Color.name};        {pokemonHabitat}\r\n" +
                $"||\r\n" +
                $"||{abilities}\r\n" +
                $"||\r\n" +
                $"||{pokemonMeasure}\r\n" +
                $"||\r\n" +
                $"||Description: {description}\r\n" +
                $"{pokemonForms}" +
                $"{evolvesTo}" +
                $"==================================================================================\r\n";
            return completedEntry;
        }
    }
}
