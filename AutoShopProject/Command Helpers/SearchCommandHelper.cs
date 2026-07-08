using AutoShopProject.Filters;
using AutoShopProject.Interfaces;

namespace AutoShopProject.Command_Helpers
{
    internal class SearchCommandHelper
    {        
        public void SearchCarsString(Func<Car, string> keyselector)
        {
            FilteredCars<string> filteredCars = new FilteredCars<string>(keyselector);
            var options = filteredCars._cars.Select(car => car.Key).ToList();
            options.Sort();

            Console.WriteLine();
            int i = 0;
            foreach (var option in options)
                Console.WriteLine($"\t{i++} - {option}");

            int input = -1;
            while (input < 0 || input > options.Count - 1)
            {
                Console.WriteLine("\n[COMMAND] Please enter the number for the option you'd like to choose >>");
                try
                {
                    input = int.Parse(Console.ReadLine().Trim());
                }
                catch { }
            }

            var chosen = options.ElementAt(input);

        }

        public void SearchCarsInt(Func<Car, int> keyselector)
        {
            FilteredCars<int> filteredCars = new FilteredCars<int>(keyselector);
            var options = filteredCars._cars.Select(car => car.Key).ToList();
            options.Sort();

            Console.WriteLine();
            int i = 0;
            foreach (var option in options)
                Console.WriteLine($"\t{i++} - {option}");

            int input = -1;
            while (input < 0 || input > options.Count - 1)
            {
                Console.WriteLine("\n[COMMAND] Please enter the number for the option you'd like to choose >>");
                try
                {
                    input = int.Parse(Console.ReadLine().Trim());
                }
                catch { }
            }

            var chosen = options.ElementAt(input);


        }
        public void SearchCarsByPrice()
        {

        }

        public void SearchEngineString(Func<Engine, string> keyselector)
        {
            FilteredEngines<string> filteredEngines = new FilteredEngines<string>(keyselector);
            var options = filteredEngines._engines.Select(eng => eng.Key).ToList();
            options.Sort();

            Console.WriteLine();
            int i = 0;
            foreach (var option in options)
                Console.WriteLine($"\t{i++} - {option}");

            int input = -1;
            while (input < 0 || input > options.Count - 1)
            {
                Console.WriteLine("\n[COMMAND] Please enter the number for the option you'd like to choose >>");
                try
                {
                    input = int.Parse(Console.ReadLine().Trim());
                }
                catch { }
            }
            var chosen = options.ElementAt(input);


        }

        public void SearchEngineDouble(Func<Engine, double> keyselector)
        {
            FilteredEngines<double> filteredEngines = new FilteredEngines<double>(keyselector);
            var options = filteredEngines._engines.Select(eng => eng.Key).ToList();
            options.Sort();

            Console.WriteLine();
            int i = 0;
            foreach (var option in options)
                Console.WriteLine($"\t{i++} - {option}");

            int input = -1;
            while (input < 0 || input > options.Count - 1)
            {
                Console.WriteLine("\n[COMMAND] Please enter the number for the option you'd like to choose >>");
                try
                {
                    input = int.Parse(Console.ReadLine().Trim());
                }
                catch { }
            }
            var chosen = options.ElementAt(input);


        }

        public void SearchEngineInt(Func<Engine, int> keyselector)
        {
            FilteredEngines<int> filteredEngines = new FilteredEngines<int>(keyselector);
            var options = filteredEngines._engines.Select(eng => eng.Key).ToList();
            options.Sort();

            Console.WriteLine();
            int i = 0;
            foreach (var option in options)
                Console.WriteLine($"\t{i++} - {option}");

            int input = -1;
            while (input < 0 || input > options.Count - 1)
            {
                Console.WriteLine("\n[COMMAND] Please enter the number for the option you'd like to choose >>");
                try
                {
                    input = int.Parse(Console.ReadLine().Trim());
                }
                catch { }
            }
            var chosen = options.ElementAt(input);


        }

        public void SearchEnginesByPrice()
        {

        }
    }
}
