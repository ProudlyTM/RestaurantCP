using System;
using System.Collections.Generic;
using System.Linq;

namespace RestaurantCP
{
    class Table
    {
        // ** Variables ** //
        static int
        takenTablesDayTotal,
        salesSaladsDayTotal, salesSoupsDayTotal,
        salesMainDishesDayTotal, salesDessertsDayTotal, salesDrinksDayTotal;
        static double
        salesDayTotal, sumSalesDayTotal,
        sumSalesSaladsDayTotal, sumSalesSoupsDayTotal,
        sumSalesMainDishesDayTotal, sumSalesDessertsDayTotal, sumSalesDrinksDayTotal;

        static string choiceName, choiceProduct;
        
        static double choiceGrammageOrMilliliters, choicePrice;

        public static List<Product> salads = new List<Product>();
        public static List<Product> soups = new List<Product>();
        public static List<Product> mainDishes = new List<Product>();
        public static List<Product> desserts = new List<Product>();
        public static List<Product> drinks = new List<Product>();

        public static List<string>[] tables = new List<string>[30];
        // ** ---------- ** //

        public const string PressAnyKeyToContinue = "\nPress any key to continue...";

        public static void PrintMenu()
        {
            Console.Clear();
            Console.WriteLine("Please enter a command - a digit for 1 or 2 or the relevant keyword or (letter) for others:\n");
            Console.WriteLine("1 - Add a product to the menu");
            Console.WriteLine("2 - Table order");
            Console.WriteLine("    Sales / (s)");
            Console.WriteLine("    Exit / (e)");
            Console.Write("\nCommand: ");
        }

        public static void AddProductToMenu()
        {
            Console.InputEncoding = System.Text.Encoding.Unicode;

            choiceProduct = choiceName = "";
            choiceGrammageOrMilliliters = choicePrice = 0;

            Console.Clear();
            Console.Write("Please enter all the requirements for the command.\n");
            Console.Write("Available options for \"Category\": salad, soup, main dish, dessert, drink\n");
            Console.WriteLine("\nKeep in mind that \"Category\" and \"Name\" accept only letters and \"Grams/milliliters\" and \"Price\" accept only digits >= 0!\n");

            // ** Adding a product to the menu ** //
            try
            {
                Console.Write("Category: "); choiceProduct = Console.ReadLine().ToLower();
                if (choiceProduct.Any(char.IsDigit)) { throw new Exception(); }
                if (!(choiceProduct == "salad" || choiceProduct == "soup" || choiceProduct == "main dish" || choiceProduct == "dessert" || choiceProduct == "drink"))
                {
                    throw new Exception();
                }
                Console.Write("Name: "); choiceName = Console.ReadLine().ToLower();
                if (choiceName.Any(char.IsDigit)) { throw new Exception(); }
                Console.Write("Grams/milliliters: "); choiceGrammageOrMilliliters = double.Parse(Console.ReadLine());
                if (choiceGrammageOrMilliliters < 0) { throw new Exception(); }
                Console.Write("Price: "); choicePrice = double.Parse(Console.ReadLine());
                if (choicePrice < 0) { throw new Exception(); }
            }
            catch
            {
                Console.Clear();
                Console.WriteLine("Incorrectly entered data!");
                if (choiceGrammageOrMilliliters < 0 || choicePrice < 0)
                {
                    Console.Write("Keep in mind that \"Grams/milliliters\" and \"Price\" accept only digits >= 0!\n");
                }
                Console.Write("Keep in mind that \"Category\" and \"Name\" accept only letters and \"Grams/milliliters\" and \"Price\" accept only digits >= 0!\n");
                Console.Write(PressAnyKeyToContinue);
                Console.ReadKey();
            }

            if (choiceProduct == "salad") { salads.Add(new Salad(choiceName, choicePrice, choiceGrammageOrMilliliters)); }
            if (choiceProduct == "soup") { soups.Add(new Soup(choiceName, choicePrice, choiceGrammageOrMilliliters)); }
            if (choiceProduct == "main dish") { mainDishes.Add(new MainDish(choiceName, choicePrice, choiceGrammageOrMilliliters)); }
            if (choiceProduct == "dessert") { desserts.Add(new Dessert(choiceName, choicePrice, choiceGrammageOrMilliliters)); }
            if (choiceProduct == "drink") { drinks.Add(new Drink(choiceName, choicePrice, choiceGrammageOrMilliliters)); }
            // ** ---------- ** //
        }

        public static void AddProductToTable()
        {
            Console.InputEncoding = System.Text.Encoding.Unicode;

            int tableNum = 0;

            Console.Clear();
            // ** Adding a product to a table ** //
            try
            {
                Console.Write("Table number: "); tableNum = int.Parse(Console.ReadLine());
                if (tableNum < 1 || tableNum > 30) { throw new Exception(); }
                Console.WriteLine("Enter \"e\" to stop adding products to the table.\n");

                while (true)
                {
                    Console.Write("Product name: ");
                    choiceName = Console.ReadLine().ToLower();
                    if (choiceName == "e") { break; }
                    if (choiceName.Any(char.IsDigit)) { throw new Exception(); }
                    if (tables[tableNum] is null)
                    {
                        tables[tableNum] = new List<string>();
                        takenTablesDayTotal++;
                    }
                    tables[tableNum].Add(choiceName);
                }
            }
            catch
            {
                Console.Clear();
                Console.WriteLine("Incorrectly entered data!");
                if (tableNum < 1 || tableNum > 30)
                {
                    Console.Write("Please keep in mind that \"table number\" accepts only digits from 1-30!\n");
                }
                else if (choiceName.Any(char.IsDigit))
                {
                    Console.Write("Keep in mind that \"Product name\" accepts only letters!\n");
                }
                Console.Write(PressAnyKeyToContinue);
                Console.ReadKey();
            }
            // ** ---------- ** //
        }

        public static void CalculateStatistics()
        {
            double[] sumSalesSaladsDayTotalArr = { 0, 0 };
            double[] sumSalesSoupsDayTotalArr = { 0, 0 };
            double[] sumSalesMainDishesDayTotalArr = { 0, 0 };
            double[] sumSalesDessertsDayTotalArr = { 0, 0 };
            double[] sumSalesDrinksDayTotalArr = { 0, 0 };

            // ** Calculating the total amount of sales for the day by category and the total amount of sales for the day ** //
            salesSaladsDayTotal = tables.Where(x => x != null)
                .SelectMany(s => s)
                .Count(x => x == "shopska salad" || x == "ovcharska salad");
            salesSoupsDayTotal = tables.Where(x => x != null)
                .SelectMany(s => s)
                .Count(x => x == "chicken soup" || x == "tarator");
            salesMainDishesDayTotal = tables.Where(x => x != null)
                .SelectMany(s => s)
                .Count(x => x == "wine kebap" || x == "moussaka");
            salesDessertsDayTotal = tables.Where(x => x != null)
                .SelectMany(s => s)
                .Count(x => x == "pancake" || x == "biscuit cake");
            salesDrinksDayTotal = tables.Where(x => x != null)
                .SelectMany(s => s)
                .Count(x => x == "coffee" || x == "tea");

            salesDayTotal = salesSaladsDayTotal + salesSoupsDayTotal + salesMainDishesDayTotal + salesDessertsDayTotal + salesDrinksDayTotal;
            // ** ---------- ** //

            // ** Calculating the profits for the day by category and the total profits for the day ** //
            foreach (var table in tables)
            {
                foreach (var salad in salads)
                {
                    if (salad.Name.Contains("shopska salad"))
                    {
                        if (tables.Where(x => x != null).SelectMany(s => s).Contains("shopska salad"))
                        {
                            sumSalesSaladsDayTotalArr[0] = salad.Price * tables.Where(x => x != null).SelectMany(s => s).Count(x => x == "shopska salad");
                        }
                    }
                    if (salad.Name.Contains("ovcharska salad"))
                    {
                        if (tables.Where(x => x != null).SelectMany(s => s).Contains("ovcharska salad"))
                        {
                            sumSalesSaladsDayTotalArr[1] = salad.Price * tables.Where(x => x != null).SelectMany(s => s).Count(x => x == "ovcharska salad");
                        }
                    }
                }

                foreach (var soup in soups)
                {
                    if (soup.Name.Contains("chicken soup"))
                    {
                        if (tables.Where(x => x != null).SelectMany(s => s).Contains("chicken soup"))
                        {
                            sumSalesSoupsDayTotalArr[0] = soup.Price * tables.Where(x => x != null).SelectMany(s => s).Count(x => x == "chicken soup");
                        }
                    }
                    if (soup.Name.Contains("tarator"))
                    {
                        if (tables.Where(x => x != null).SelectMany(s => s).Contains("tarator"))
                        {
                            sumSalesSoupsDayTotalArr[1] = soup.Price * tables.Where(x => x != null).SelectMany(s => s).Count(x => x == "tarator");
                        }
                    }
                }

                foreach (var dish in mainDishes)
                {
                    if (dish.Name.Contains("wine kebab"))
                    {
                        if (tables.Where(x => x != null).SelectMany(s => s).Contains("wine kebab"))
                        {
                            sumSalesMainDishesDayTotalArr[0] = dish.Price * tables.Where(x => x != null).SelectMany(s => s).Count(x => x == "wine kebab");
                        }
                    }
                    if (dish.Name.Contains("moussaka"))
                    {
                        if (tables.Where(x => x != null).SelectMany(s => s).Contains("moussaka"))
                        {
                            sumSalesMainDishesDayTotalArr[1] = dish.Price * tables.Where(x => x != null).SelectMany(s => s).Count(x => x == "moussaka");
                        }
                    }
                }

                foreach (var dessert in desserts)
                {
                    if (dessert.Name.Contains("pancake"))
                    {
                        if (tables.Where(x => x != null).SelectMany(s => s).Contains("pancake"))
                        {
                            sumSalesDessertsDayTotalArr[0] = dessert.Price * tables.Where(x => x != null).SelectMany(s => s).Count(x => x == "pancake");
                        }
                    }
                    if (dessert.Name.Contains("biscuit cake"))
                    {
                        if (tables.Where(x => x != null).SelectMany(s => s).Contains("biscuit cake"))
                        {
                            sumSalesDessertsDayTotalArr[1] = dessert.Price * tables.Where(x => x != null).SelectMany(s => s).Count(x => x == "biscuit cake");
                        }
                    }
                }

                foreach (var drink in drinks)
                {
                    if (drink.Name.Contains("coffee"))
                    {
                        if (tables.Where(x => x != null).SelectMany(s => s).Contains("coffee"))
                        {
                            sumSalesDrinksDayTotalArr[0] = drink.Price * tables.Where(x => x != null).SelectMany(s => s).Count(x => x == "coffee");
                        }
                    }
                    if (drink.Name.Contains("tea"))
                    {
                        if (tables.Where(x => x != null).SelectMany(s => s).Contains("tea"))
                        {
                            sumSalesDrinksDayTotalArr[1] = drink.Price * tables.Where(x => x != null).SelectMany(s => s).Count(x => x == "tea");
                        }
                    }
                }
            }

            sumSalesSaladsDayTotal = sumSalesSaladsDayTotalArr[0] + sumSalesSaladsDayTotalArr[1];
            sumSalesSoupsDayTotal = sumSalesSoupsDayTotalArr[0] + sumSalesSoupsDayTotalArr[1];
            sumSalesMainDishesDayTotal = sumSalesMainDishesDayTotalArr[0] + sumSalesMainDishesDayTotalArr[1];
            sumSalesDessertsDayTotal = sumSalesDessertsDayTotalArr[0] + sumSalesDessertsDayTotalArr[1];
            sumSalesDrinksDayTotal = sumSalesDrinksDayTotalArr[0] + sumSalesDrinksDayTotalArr[1];

            sumSalesDayTotal = sumSalesSaladsDayTotal + sumSalesSoupsDayTotal + sumSalesMainDishesDayTotal + sumSalesDessertsDayTotal + sumSalesDrinksDayTotal;
            // ** ---------- ** //
        }

        public static void Statistics()
        {
            CalculateStatistics();
            Console.Clear();
            Console.WriteLine($"Total taken tables for the day: {takenTablesDayTotal}");
            Console.WriteLine($"Total sales for the day: {salesDayTotal} - {String.Format("{0:0.00}", sumSalesDayTotal)}");
            Console.WriteLine($"\nBy category:");
            Console.WriteLine($"    - Salad: {salesSaladsDayTotal} - {String.Format("{0:0.00}", sumSalesSaladsDayTotal)}");
            Console.WriteLine($"    - Soup: {salesSoupsDayTotal} - {String.Format("{0:0.00}", sumSalesSoupsDayTotal)}");
            Console.WriteLine($"    - Main dish: {salesMainDishesDayTotal} - {String.Format("{0:0.00}", sumSalesMainDishesDayTotal)}");
            Console.WriteLine($"    - Dessert: {salesDessertsDayTotal} - {String.Format("{0:0.00}", sumSalesDessertsDayTotal)}");
            Console.WriteLine($"    - Drink: {salesDrinksDayTotal} - {String.Format("{0:0.00}", sumSalesDrinksDayTotal)}");
        }
    }
}