using System;

namespace PokeDex
{
    class Settings
    { 
        public static bool EmperialMeasureSetting = false;
        public static bool DefaultConsole = false;

        public static void AlterSettings()
        { 
            Console.WriteLine("==================================================================");
            Console.WriteLine("To exit settings, enter \"return\"");
            Console.WriteLine("  If you would like to...\r\n   >Change units of measurement, enter \"units\".\r\n   >Change Console Colors, enter \"colors\".");
            while (true)
            {
                Console.WriteLine("  Current settings:");
                if (EmperialMeasureSetting == true)
                {
                    Console.WriteLine("   >Units of measurement: Metric");
                }
                else 
                {
                    Console.WriteLine("   >Units of measurement: Emperial");
                }
                if (DefaultConsole == false)
                {
                    Console.WriteLine("   >Current console: Colorful");
                }
                else
                {
                    Console.WriteLine("   >Current console: Default");
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
                    }
                    else 
                    {
                        Console.BackgroundColor = ConsoleColor.DarkRed;
                        Console.ForegroundColor = ConsoleColor.Cyan;
                    }

                }
                else if (settingsInput == "return")
                {
                    Console.WriteLine("Leaving settings.");
                    Console.WriteLine("==================================================================");
                    break;
                }
                else
                {
                    Console.WriteLine("Please enter \"units\" or \"colors\" to alter settings. Otherwise, enter \"return\" to exit settings.");
                }
            }
        }
    }
}
