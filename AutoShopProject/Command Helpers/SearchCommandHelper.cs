using AutoShopProject.Application;
using AutoShopProject.Filters;
using AutoShopProject.Interfaces;
using Microsoft.VisualBasic.FileIO;

namespace AutoShopProject.Command_Helpers
{
    internal class SearchCommandHelper<TKey, TValue> : ISearchHelper
    {
        /// <summary>
        /// 
        /// gets a provided collection that is mapped to a generic key attribute...
        /// for example: collecrion = Filtered<string, Car> cars (cars mapped to all manufacturers)
        /// 
        /// after that the user deside by which options he wants to filter the collection...
        /// for example: 'nissan' -> returns an Enumerable of all the cars that their manufacturer is nissan.
        /// 
        /// finally, the user chooses an item from the filtered collection and has the option to extract it or just continue on.
        /// 
        /// NOTE: Action<TValue> RemoveItem - this action is responsible for removing the generic item outside of this class
        ///                                   because this class is only responsible for searching, not removing and it can't know
        ///                                   which type is the generic item that the user chose (Car/Engine) .
        /// 
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="RemoveItem"></param>
        /// <returns> returns the options that has been selected to store in the search history </returns>
        public string Search(Filtered<TKey, TValue> collection, Action<TValue> RemoveItem)
        {
            var options = collection.filteredLookUp.Select(x => x.Key).ToList(); // list of all the options of the spesific attribute (the generic key), example: all car manufacturers
            options.Sort();

            int input = ShowOptions(options.ConvertAll(o => o.ToString())); // shows all options in a string format        

            var chosenOption = options.ElementAt(input);
            var filteredCollection = collection.FilterBy(chosenOption); // filters the collection by the chosen attribute
            var chosen = Choose(filteredCollection);

            Console.WriteLine("\n[COMMAND] chosen item: \n");
            Console.WriteLine(chosen.ToString());

            char ans = GetUserDesition();

            if (ans == 'N' || ans == 'n')
            {
                Console.WriteLine("[MANAGER] Operation Cancelled. Exiting command...");
                return chosenOption.ToString();
            }
            RemoveItem(chosen); // let the provided action handle removing the item

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

        private TValue Choose(IEnumerable<TValue> collection)
        {
            Console.WriteLine();
            int i = 0;
            foreach (var option in collection)
            {
                Console.WriteLine($"\t{i++}:");
                Console.WriteLine(option.ToString());
                Console.WriteLine();
            }

            int input = -1;
            while (input < 0 || input > collection.Count() - 1)
            {
                Console.WriteLine("\n[COMMAND] Please enter the number for the item you'd like to choose >>");
                try
                {
                    input = int.Parse(Console.ReadLine().Trim());
                }
                catch { }
            }
            return collection.ElementAt(input);
        }

        private char GetUserDesition()
        {
            Console.WriteLine("[COMMAND] Do you wish to extract this item? (Y/N) >>");
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
        /// 
        /// searches foe a generic item in the user's provided price range...
        /// 
        /// NOTE: Func getPrice - anonymous function to get the price of the generic Value (Car/Engine)
        ///       
        ///       Action<TValue> RemoveItem - this action is responsible for removing the generic item outside of this class
        ///                                   because this class is only responsible for searching, not removing and it can't know
        ///                                   which type is the generic item that the user chose (Car/Engine) .
        /// 
        /// </summary>
        /// <param name="baseList"></param>
        /// <param name="getPrice"></param>
        /// <param name="RemoveItem"></param>
        public void SearchByPrice(List<TValue> baseList, Func<TValue, double> getPrice, Action<TValue> RemoveItem)
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
                lowerBound = higherBound;
                higherBound = tmp;
            }

            var inRange = InPriceRange(baseList, lowerBound, higherBound, getPrice);
            var chosen = Choose(inRange);

            Console.WriteLine("\n[COMMAND] chosen item: \n");
            Console.WriteLine(chosen.ToString());

            char ans = GetUserDesition();

            if (ans == 'N' || ans == 'n')
            {
                Console.WriteLine("[MANAGER] Operation Cancelled. Exiting command...");
                return;
            }
            RemoveItem(chosen);
        }

        /// <summary>
        /// 
        /// filters the base list by the provided price range
        /// 
        /// </summary>
        /// <param name="lst"></param>
        /// <param name="lowerBound"></param>
        /// <param name="higherBound"></param>
        /// <param name="getPrice"></param>
        /// <returns> returs a list of cars in the given price boundary </returns>
        private List<TValue> InPriceRange(List<TValue> lst, double lowerBound, double higherBound, Func<TValue, double> getPrice)
        {
            var inRange = new List<TValue>();
            Console.WriteLine();
            foreach (var item in lst)
            {
                double itemPrice = getPrice(item);
                if (itemPrice <= higherBound && itemPrice >= lowerBound)
                    inRange.Add(item);
            }
            return inRange;
        }
    }
}
