using System;
using System.Collections.Generic;
using System.Linq;

namespace PokeDex
{
    class Settings
    { 
        public static bool EmperialMeasureSetting = false;
        public static bool DefaultConsole = false;

        //Main settings loop.
        public static void AlterSettings()
        { 
            Console.WriteLine("Entering settings...\r\n\r\n==================================================================================");
            Console.WriteLine("To exit settings, enter \"return\"");
            Console.WriteLine("\r\nIf you would like to...\r\n   >Change units of measurement, enter \"units\".\r\n   >Change Console Colors, enter \"colors\".");
            Console.WriteLine("==================================================================================");
            while (true)
            {
                WriteFullLine("\r\nCurrent settings:");
               //Show the current units of measurement setting.
                if (EmperialMeasureSetting == true)
                {
                    WriteFullLine("   >Units of measurement: Emperial");
                }
                else 
                {
                    WriteFullLine("   >Units of measurement: Metric");
                }

                //Shows the current console color settings.
                if (DefaultConsole == false)
                {
                    WriteFullLine("   >Current console: Colorful");
                }
                else
                {
                    WriteFullLine("   >Current console: Default");
                }

                //Handles user input in the settings menu.
                string settingsInput = Console.ReadLine().ToLower();
                if (settingsInput == "units")
                {
                    EmperialMeasureSetting = !EmperialMeasureSetting;
                }
                else if (settingsInput == "colors")
                {
                    DefaultConsole = !DefaultConsole;

                    if (DefaultConsole == true)
                    {
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.ForegroundColor = ConsoleColor.White;
                        WriteFullLine("");
                    }
                    else 
                    {
                        Console.BackgroundColor = ConsoleColor.DarkRed;
                        Console.ForegroundColor = ConsoleColor.Cyan;
                    }

                }
                else if (CheckForQuit(settingsInput) == false)
                {
                    WriteFullLine("Leaving settings.");
                    WriteFullLine("\r\n==================================================================================\r\n");
                    break;
                }
                else
                {
                    WriteFullLine("Please enter \"units\" or \"colors\" to alter settings. Otherwise, enter \"return\" to exit settings.");
                }
            }
        }

        //Attempts to handle coloring errors that sometimes occur when switching console colors.
        //It fills the remaining space on the line.
        public static void WriteFullLine(string value)
        {
            Console.WriteLine(value.PadRight(Console.WindowWidth));
        }

        public static void CheckColors(ConsoleColor color)
        { 
            if (DefaultConsole == false)
            {
                Console.ForegroundColor = color;
            }
        }

        //Sets the terminal colors based on what is passed in.
        public static void SetColors()
        {
            if (Console.BackgroundColor == ConsoleColor.Black)
            {
                Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Clear();
            }
        }

        public static bool CheckForQuit(string entry)
        {
            var quitCommands = new List<string> { "stop", "exit", "quit", "q", "return" };
            if (quitCommands.Any(str => str.Contains(entry)) && entry != "")
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
