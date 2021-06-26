// App for managing a newly opened restaurant

using System;

namespace RestaurantCP
{
    class Program
    {
        const string PressAnyKeyToContinue = Table.PressAnyKeyToContinue;

        static void Main()
        {
            // ** Localization settings ** //
            System.Globalization.CultureInfo customCulture = (System.Globalization.CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
            customCulture.NumberFormat.NumberDecimalSeparator = ".";
            System.Threading.Thread.CurrentThread.CurrentCulture = customCulture;
            // ** ---------- ** //

            while (true)
            {
                Table.PrintMenu();
                string command = Console.ReadLine().ToLower();

                if (command == "1")
                {
                    Table.AddProductToMenu();
                }
                else if (command == "2")
                {
                    Table.AddProductToTable();
                }
                else if (command == "sales" || command == "s")
                {
                    Table.Statistics();
                    Console.Write(PressAnyKeyToContinue);
                    Console.ReadKey();
                }
                else if (command == "exit" || command == "e")
                {
                    Table.Statistics();
                    break;
                }
            }
        }
    }
}