using AutoShopProject.Application;
using AutoShopProject.Command_Helpers;
using AutoShopProject.Filters;
using AutoShopProject.Interfaces;
using static System.Net.WebRequestMethods;

namespace AutoShopProject.Commands
{
    internal class SearchCommand : ICommandUser
    {
        private Dictionary<int, string> _car_options = new Dictionary<int, string>()
        {
            [0] = "CarType",
            [1] = "Manufacturers",
            [2] = "Models",
            [3] = "Year",
            [4] = "Drivetrain",
            [5] = "Seats",
            [6] = "Doors",
            [7] = "Price",
        };

        private Dictionary<int, string> _engine_options = new Dictionary<int, string>()
        {
            [0] = "EngineType",
            [1] = "EngineID",
            [2] = "Volume",
            [3] = "Horsepower",
            [4] = "Price",
        };

        private Dictionary<string, IFiltered> _filters = new Dictionary<string, IFiltered>()
        {
            ["CarType"]       = new Filtered<string, Car>(Catalog.catalog, car => car.CarType),      // cars by car type
            ["Manufacturers"] = new Filtered<string, Car>(Catalog.catalog, car => car.Manufacturer), // cars by manufacturer
            ["Models"]        = new Filtered<string, Car>(Catalog.catalog, car => car.Model),        // cars by model
            ["Drivetrain"]    = new Filtered<string, Car>(Catalog.catalog, car => car.Drivetrain),   // cars by drivetrain

            ["Year"]  = new Filtered<int, Car>(Catalog.catalog, car => car.Year),  // cars by year
            ["Seats"] = new Filtered<int, Car>(Catalog.catalog, car => car.Seats), // cars by seats
            ["Doors"] = new Filtered<int, Car>(Catalog.catalog, car => car.Doors), // cars by doors

            ["EngineType"] = new Filtered<string, Engine>(Catalog.engines, e => e.Type), // engines by type
            ["EngineID"]   = new Filtered<string, Engine>(Catalog.engines, e => e.id),   // engines by id

            ["Volume"] = new Filtered<double, Engine>(Catalog.engines, e => e.Volume), // engines by volume

            ["Horsepower"] = new Filtered<int, Engine>(Catalog.engines, e => e.Horsepower) // engines by horsepower
        };

        private Dictionary<string, ISearchHelper> _helpers = new Dictionary<string, ISearchHelper>()
        {
            ["str c"]    = new SearchCommandHelper<string, Car>(),
            ["int c"]    = new SearchCommandHelper<int, Car>(),
            ["double c"] = new SearchCommandHelper<double, Car>(),

            ["str e"]    = new SearchCommandHelper<string, Engine>(),
            ["double e"] = new SearchCommandHelper<double, Engine>(),              
            ["int e"]    = new SearchCommandHelper<int, Engine>()                        
        };

        public void Execute() 
        {
            var print = new Dictionary<int, string>()
            {
                [0] = "[COMMAND] What do you wish to search? 'C' (Car) or 'E' (Engine) >> ",
                [1] = "[COMMAND] Invalid input, please re-enter: 'C' (Car) or 'E' (Engine) >> "
            };
            string input = "";
            int attempt = 0;

            while (input != "C" && input != "E")
            {
                Console.WriteLine(print[attempt]);
                input = Console.ReadLine().Trim().ToUpper();
                attempt++;

                attempt = (attempt > 0) ? 1 : 0; // leave 1 as 1 ...
            }

            string search;
            if (input == "C")
                search = CarSearch();
            else
                search = EngineSearch();

            SearchHistory.Handle(search); // add search to history
        }

        private string CarSearch()
        {
            int input = SelectOption(_car_options);
            var removeHelper = new RemoveCommandHelper();
            string searchOption = "";

            switch (_car_options[input])
            {
                case "CarType":
                    {
                        var shelper = (SearchCommandHelper<string, Car>)_helpers["str c"];
                        var filter = (Filtered<string, Car>)_filters[_car_options[input]];

                        searchOption = "CarType ";
                        searchOption += shelper.Search(filter, Catalog.catalog, removeHelper.TryRemoveCar);
                        break;
                    }

                case "Manufacturers":
                    {
                        var shelper = (SearchCommandHelper<string, Car>)_helpers["str c"];
                        var filter = (Filtered<string, Car>)_filters[_car_options[input]];

                        searchOption = "manufacturer ";
                        searchOption += shelper.Search(filter, Catalog.catalog, removeHelper.TryRemoveCar);
                        break;
                    }

                case "Models":
                    {
                        var shelper = (SearchCommandHelper<string, Car>)_helpers["str c"];
                        var filter = (Filtered<string, Car>)_filters[_car_options[input]];

                        searchOption = "Model ";
                        searchOption += shelper.Search(filter, Catalog.catalog, removeHelper.TryRemoveCar);
                        break;
                    }

                case "Drivetrain":
                    {
                        var shelper = (SearchCommandHelper<string, Car>)_helpers["str c"];
                        var filter = (Filtered<string, Car>)_filters[_car_options[input]];

                        searchOption = "Drivetrain ";
                        searchOption += shelper.Search(filter, Catalog.catalog, removeHelper.TryRemoveCar);
                        break;
                    }

                case "Year":
                    {
                        var shelper = (SearchCommandHelper<int, Car>)_helpers["int c"];
                        var filter = (Filtered<int, Car>)_filters[_car_options[input]];

                        searchOption = "Year ";
                        searchOption += shelper.Search(filter, Catalog.catalog, removeHelper.TryRemoveCar);
                        break;
                    }

                case "Seats":
                    {
                        var shelper = (SearchCommandHelper<int, Car>)_helpers["int c"];
                        var filter = (Filtered<int, Car>)_filters[_car_options[input]];

                        searchOption = "Seats ";
                        searchOption += shelper.Search(filter, Catalog.catalog, removeHelper.TryRemoveCar);
                        break;
                    }

                case "Doors":
                    {
                        var shelper = (SearchCommandHelper<int, Car>)_helpers["int c"];
                        var filter = (Filtered<int, Car>)_filters[_car_options[input]];

                        searchOption = "Doors ";
                        searchOption += shelper.Search(filter, Catalog.catalog, removeHelper.TryRemoveCar);
                        break;
                    }

                case "Price":
                    {
                        var shelper = (SearchCommandHelper<double, Car>)_helpers["double c"];
                        shelper.SearchByPrice(Catalog.catalog, car => car.Price, removeHelper.TryRemoveCar);
                        break;
                    }
            }
            Console.WriteLine("[COMMAND] Search complete.");
            return searchOption;
        }

        private string EngineSearch()
        {
            int input = SelectOption(_engine_options);
            var removeHelper = new RemoveCommandHelper();
            string searchOption = "";

            switch (_engine_options[input])
            {
                case "EngineType":
                    {
                        var shelper = (SearchCommandHelper<string, Engine>)_helpers["str e"];
                        var filter = (Filtered<string, Engine>)_filters[_engine_options[input]];

                        searchOption = "EngineType ";
                        searchOption += shelper.Search(filter, Catalog.engines, removeHelper.TryRemoveEngine);
                        break;
                    }                    

                case "EngineID":
                    {
                        var shelper = (SearchCommandHelper<string, Engine>)_helpers["str e"];
                        var filter = (Filtered<string, Engine>)_filters[_engine_options[input]];

                        searchOption = "EngineType ";
                        searchOption += shelper.Search(filter, Catalog.engines, removeHelper.TryRemoveEngine);
                        break;
                    }

                case "Volume":
                    {
                        var shelper = (SearchCommandHelper<double, Engine>)_helpers["double e"];
                        var filter = (Filtered<double, Engine>)_filters[_engine_options[input]];

                        searchOption = "EngineType ";
                        searchOption += shelper.Search(filter, Catalog.engines, removeHelper.TryRemoveEngine);
                        break;
                    }

                case "Horsepower":
                    {
                        var shelper = (SearchCommandHelper<int, Engine>)_helpers["int e"];
                        var filter = (Filtered<int, Engine>)_filters[_engine_options[input]];

                        searchOption = "EngineType ";
                        searchOption += shelper.Search(filter, Catalog.engines, removeHelper.TryRemoveEngine);
                        break;
                    }

                case "Price":
                    {
                        var shelper = (SearchCommandHelper<double, Engine>)_helpers["double e"];
                        shelper.SearchByPrice(Catalog.engines, e => e.Price, removeHelper.TryRemoveEngine);
                        break;
                    }
            }
            Console.WriteLine("[COMMAND] Search complete.");
            return searchOption;
        }

        private int SelectOption(Dictionary<int, string> options)
        {
            Console.WriteLine();
            for (int i = 0; i < options.Count; i++)
            {
                Console.WriteLine($"\t{i} - {options[i]}");
            }

            int input = -1;
            while (!options.ContainsKey(input))
            {
                Console.WriteLine("[COMMAND] What would you like to search for?");
                try
                {
                    input = int.Parse(Console.ReadLine());
                }
                catch { }
            }
            return input;
        }
    }
}
