using AutoShopProject.Application;
using AutoShopProject.Filters;
using AutoShopProject.Interfaces;

namespace AutoShopProject.Command_Helpers
{
    internal class SearchCommandHelper
    {
        /// <summary>
        /// 
        /// filters the cars by a string keyselector, for example: 'manufacturers' -> returns only the manufacturers of all the cars in the catalog.
        /// after that the user deside by which options he wants to filter the cars even more,
        /// for example: 'nissan' -> returns an Enumerable of all the cars that their manufacturer is nissan.
        /// finally, the user chooses a car and we give him the option to extract it or just continue on.
        /// 
        /// </summary>
        /// <param name="keyselector"></param>
        /// <returns> returns the search option for search history </returns>
        public string SearchCarsString(Func<Car, string> keyselector)
        {
            FilteredCars<string> filteredCars = new FilteredCars<string>(keyselector);
            var options = filteredCars._cars.Select(car => car.Key).ToList();
            options.Sort();

            int input = ShowOptions(options.ConvertAll(o => o.ToString()));            

            var chosenOption = options.ElementAt(input);
            var cars = filteredCars.FilterBy(chosenOption);
            var chosenCar = ChooseCar(cars);

            Console.WriteLine("\n[COMMAND] chosen car: \n");
            Console.WriteLine(chosenCar.ToString());

            char ans = GetUserDesition();

            if (ans == 'N' || ans == 'n')
            {
                Console.WriteLine("[MANAGER] Operation Cancelled. Exiting command...");
                return chosenOption;
            }
            RemoveCommandHelper helper = new RemoveCommandHelper();
            helper.TryRemoveCar(chosenCar);

            return chosenOption; // return searched option for search history
        }

        // same explanation as the last summary, only here the keyselector returns an int value because of the generic type.
        public string SearchCarsInt(Func<Car, int> keyselector)
        {
            FilteredCars<int> filteredCars = new FilteredCars<int>(keyselector);
            var options = filteredCars._cars.Select(car => car.Key).ToList();
            options.Sort();

            int input = ShowOptions(options.ConvertAll(o => o.ToString()));

            var chosenOption = options.ElementAt(input);
            var cars = filteredCars.FilterBy(chosenOption);
            var chosenCar = ChooseCar(cars);

            Console.WriteLine("\n[COMMAND] chosen car: \n");
            Console.WriteLine(chosenCar.ToString());

            char ans = GetUserDesition();
            if (ans == 'N' || ans == 'n')
            {
                Console.WriteLine("[MANAGER] Operation Cancelled. Exiting command...");
                return chosenOption.ToString();
            }
            RemoveCommandHelper helper = new RemoveCommandHelper();
            helper.TryRemoveCar(chosenCar);

            return chosenOption.ToString(); // return searched option for search history
        }

        private int ShowOptions(List<string> options)
        {
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
            return input;
        }

        private Car ChooseCar(IEnumerable<Car> cars)
        {
            Console.WriteLine();
            int i = 0;
            foreach (var car in cars)
            {
                Console.WriteLine($"\t{i++}:");
                Console.WriteLine(car.ToString());
                Console.WriteLine();
            }

            int input = -1;
            while (input < 0 || input > cars.Count() - 1)
            {
                Console.WriteLine("\n[COMMAND] Please enter the number for the car you'd like to choose >>");
                try
                {
                    input = int.Parse(Console.ReadLine().Trim());
                }
                catch { }
            }
            return cars.ElementAt(input);
        }

        private Engine ChooseEngine(IEnumerable<Engine> engines)
        {
            Console.WriteLine();
            int i = 0;
            foreach (var engine in engines)
            {
                Console.WriteLine($"\t{i++}:");
                Console.WriteLine(engine.ToString());
                Console.WriteLine();
            }

            int input = -1;
            while (input < 0 || input > engines.Count() - 1)
            {
                Console.WriteLine("\n[COMMAND] Please enter the number for the engine you'd like to choose >>");
                try
                {
                    input = int.Parse(Console.ReadLine().Trim());
                }
                catch { }
            }
            return engines.ElementAt(input);
        }

        private char GetUserDesition()
        {
            Console.WriteLine("[COMMAND] Do you wish to extract this? (Y/N) >>");
            char ans = Console.ReadKey().KeyChar;
            Console.WriteLine();

            while (ans != 'Y' && ans != 'y' && ans != 'N' && ans != 'n')
            {
                Console.WriteLine("[MANAGER] Please enter valid answear >>");
                ans = Console.ReadKey().KeyChar;
                Console.WriteLine();
            }
            return ans;
        }

        /// <summary>
        ///     returs a list of cars in the given boundary
        /// </summary>
        /// <param name="lowerBound"></param>
        /// <param name="higherBound"></param>
        /// <returns></returns>
        private List<Car> CarsInPriceRange(double lowerBound, double higherBound)
        {
            List<Car> lst = new List<Car>();
            Console.WriteLine();
            foreach (var car in Catalog.catalog)
            {
                if (car.Price <= higherBound && car.Price >= lowerBound)
                    lst.Add(car);
            }
            return lst;
        }

        public void SearchCarsByPrice()
        {
            Console.WriteLine("[COMMAND] Enter price range first bound >>");
            double lowerBound = -1.0, higherBound = -1.0;

            while (lowerBound < 0)
            {
                Console.WriteLine("[COMMAND] >>");
                lowerBound = double.Parse(Console.ReadLine().Trim());
            }
            Console.WriteLine("[COMMAND] Enter price range second bound >>");
            while (higherBound < 0 || lowerBound == higherBound)
            {
                Console.WriteLine("[COMMAND] >>");
                higherBound = double.Parse(Console.ReadLine().Trim());
            }

            if (higherBound < lowerBound)
            {
                double tmp = lowerBound;
                higherBound = lowerBound;
                lowerBound = tmp;
            }

            var cars = CarsInPriceRange(lowerBound, higherBound);
            var chosenCar = ChooseCar(cars);

            Console.WriteLine("\n[COMMAND] chosen car: \n");
            Console.WriteLine(chosenCar.ToString());

            char ans = GetUserDesition();

            if (ans == 'N' || ans == 'n')
            {
                Console.WriteLine("[MANAGER] Operation Cancelled. Exiting command...");
                return;
            }
            RemoveCommandHelper helper = new RemoveCommandHelper();
            helper.TryRemoveCar(chosenCar);
        }

        /// <summary>
        /// 
        /// filters the cars by a string keyselector, for example: 'engine types' -> returns only the engine types of all the engines in the catalog.
        /// after that the user desides by which options he wants to filter the engines even more,
        /// for example: 'race' -> returns an Enumerable of all the engines that their engine type is Race.
        /// finally, the user chooses an engine and we give him the option to extract it or just continue on.        
        /// 
        /// </summary>
        /// <param name="keyselector"></param>
        /// <returns> returns the search option for saving the history. </returns>
        public string SearchEngineString(Func<Engine, string> keyselector)
        {
            FilteredEngines<string> filteredEngines = new FilteredEngines<string>(keyselector);
            var options = filteredEngines._engines.Select(e => e.Key).ToList();
            options.Sort();

            int input = ShowOptions(options.ConvertAll(o => o.ToString()));

            var chosenOption = options.ElementAt(input);
            var engines = filteredEngines.FilterBy(chosenOption);
            var chosenEngine = ChooseEngine(engines);            

            Console.WriteLine("\n[COMMAND] chosen Engine: \n");
            Console.WriteLine(chosenEngine.ToString());

            char ans = GetUserDesition();

            if (ans == 'N' || ans == 'n')
            {
                Console.WriteLine("[MANAGER] Operation Cancelled. Exiting command...");
                return chosenOption;
            }
            RemoveCommandHelper helper = new RemoveCommandHelper();
            helper.TryRemoveEngine(chosenEngine);

            return chosenOption; // return searched option for search history
        }

        // same summary but the keyselector return a double
        public string SearchEngineDouble(Func<Engine, double> keyselector)
        {
            FilteredEngines<double> filteredEngines = new FilteredEngines<double>(keyselector);
            var options = filteredEngines._engines.Select(e => e.Key).ToList();
            options.Sort();

            int input = ShowOptions(options.ConvertAll(o => o.ToString()));

            var chosenOption = options.ElementAt(input);
            var engines = filteredEngines.FilterBy(chosenOption);
            var chosenEngine = ChooseEngine(engines);

            Console.WriteLine("\n[COMMAND] chosen Engine: \n");
            Console.WriteLine(chosenEngine.ToString());

            char ans = GetUserDesition();

            if (ans == 'N' || ans == 'n')
            {
                Console.WriteLine("[MANAGER] Operation Cancelled. Exiting command...");
                return chosenOption.ToString();
            }
            RemoveCommandHelper helper = new RemoveCommandHelper();
            helper.TryRemoveEngine(chosenEngine);

            return chosenOption.ToString(); // return searched option for search history
        }

        // same explanation, but keyselector return int
        public string SearchEngineInt(Func<Engine, int> keyselector)
        {
            FilteredEngines<int> filteredEngines = new FilteredEngines<int>(keyselector);
            var options = filteredEngines._engines.Select(e => e.Key).ToList();
            options.Sort();

            int input = ShowOptions(options.ConvertAll(o => o.ToString()));

            var chosenOption = options.ElementAt(input);
            var engines = filteredEngines.FilterBy(chosenOption);
            var chosenEngine = ChooseEngine(engines);

            Console.WriteLine("\n[COMMAND] chosen Engine: \n");
            Console.WriteLine(chosenEngine.ToString());

            char ans = GetUserDesition();

            if (ans == 'N' || ans == 'n')
            {
                Console.WriteLine("[MANAGER] Operation Cancelled. Exiting command...");
                return chosenOption.ToString();
            }
            RemoveCommandHelper helper = new RemoveCommandHelper();
            helper.TryRemoveEngine(chosenEngine);

            return chosenOption.ToString(); // return searched option for search history
        }

        public void SearchEnginesByPrice()
        {
            Console.WriteLine("[COMMAND] Enter price range first bound >>");
            double lowerBound = -1.0, higherBound = -1.0;

            while (lowerBound < 0)
            {
                Console.WriteLine("[COMMAND] >>");
                lowerBound = double.Parse(Console.ReadLine().Trim());
            }
            Console.WriteLine("[COMMAND] Enter price range second bound >>");
            while (higherBound < 0 || lowerBound == higherBound)
            {
                Console.WriteLine("[COMMAND] >>");
                higherBound = double.Parse(Console.ReadLine().Trim());
            }

            if (higherBound < lowerBound)
            {
                double tmp = lowerBound;
                higherBound = lowerBound;
                lowerBound = tmp;
            }

            var engines = EnginesInPriceRange(lowerBound, higherBound);
            var chosenEngine = ChooseEngine(engines);

            Console.WriteLine("\n[COMMAND] chosen Engine: \n");
            Console.WriteLine(chosenEngine.ToString());

            char ans = GetUserDesition();
            if (ans == 'N' || ans == 'n')
            {
                Console.WriteLine("[MANAGER] Operation Cancelled. Exiting command...");
                return;
            }
            RemoveCommandHelper helper = new RemoveCommandHelper();
            helper.TryRemoveEngine(chosenEngine);
        }

        /// <summary>
        ///     returs a list of cars in the given boundary
        /// </summary>
        /// <param name="lowerBound"></param>
        /// <param name="higherBound"></param>
        /// <returns></returns>
        private List<Engine> EnginesInPriceRange(double lowerBound, double higherBound)
        {
            List<Engine> lst = new List<Engine>();
            Console.WriteLine();
            foreach (var engine in Catalog.engines)
            {
                if (engine.Price <= higherBound && engine.Price >= lowerBound)
                    lst.Add(engine);
            }
            return lst;
        }
    }
}
