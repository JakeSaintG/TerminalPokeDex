using System;
using System.Collections.Generic;
using System.Linq;

namespace PokeDex
{
    class Settings
    { 
        public static bool EmperialMeasureSetting = false;
        public static bool DefaultConsole = false;

        public static void AlterSettings()
        { 
            Console.WriteLine("Entering settings...\r\n\r\n==================================================================================");
            Console.WriteLine("To exit settings, enter \"return\"");
            Console.WriteLine("\r\nIf you would like to...\r\n   >Change units of measurement, enter \"units\".\r\n   >Change Console Colors, enter \"colors\".");
            Console.WriteLine("==================================================================================");
            while (true)
            {
                WriteFullLine("\r\nCurrent settings:");
                if (EmperialMeasureSetting == true)
                {
                    WriteFullLine("   >Units of measurement: Emperial");
                }
                else 
                {
                    WriteFullLine("   >Units of measurement: Metric");
                }
                if (DefaultConsole == false)
                {
                    WriteFullLine("   >Current console: Colorful");
                }
                else
                {
                    WriteFullLine("   >Current console: Default");
                }

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
            if (quitCommands.Any(str => str.Contains(entry)))
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
