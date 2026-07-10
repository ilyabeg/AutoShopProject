using AutoShopProject.Application;
using AutoShopProject.Command_Helpers;
using AutoShopProject.Filters;
using AutoShopProject.Interfaces;

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
                    var carTypeHelper = new SearchCommandHelper<string, Car>();
                    var carsByType = new Filtered<string, Car>(Catalog.catalog, car => car.CarType); // cars by car type

                    searchOption = "CarType ";
                    searchOption += carTypeHelper.Search(carsByType, Catalog.catalog, removeHelper.TryRemoveCar);
                    break;

                case "Manufacturers":
                    var manufacturerHelper = new SearchCommandHelper<string, Car>();
                    var carsByMan = new Filtered<string, Car>(Catalog.catalog, car => car.Manufacturer); // cars by manufacturer

                    searchOption = "manufacturer ";
                    searchOption += manufacturerHelper.Search(carsByMan, Catalog.catalog, removeHelper.TryRemoveCar);
                    break;

                case "Models":
                    var modelHelper = new SearchCommandHelper<string, Car>();
                    var carsByModel = new Filtered<string, Car>(Catalog.catalog, car => car.Model); // cars by model

                    searchOption = "Model ";
                    searchOption += modelHelper.Search(carsByModel, Catalog.catalog, removeHelper.TryRemoveCar);
                    break;

                case "Drivetrain":
                    var drivetrainHelper = new SearchCommandHelper<string, Car>();
                    var carsByDrive = new Filtered<string, Car>(Catalog.catalog, car => car.Drivetrain); // cars by drivetrain

                    searchOption = "Drivetrain ";
                    searchOption += drivetrainHelper.Search(carsByDrive, Catalog.catalog, removeHelper.TryRemoveCar);
                    break;

                case "Year":
                    var yearHelper = new SearchCommandHelper<int, Car>();
                    var carsByYear = new Filtered<int, Car>(Catalog.catalog, car => car.Year); // cars by year

                    searchOption = "Year ";
                    searchOption += yearHelper.Search(carsByYear, Catalog.catalog, removeHelper.TryRemoveCar);
                    break;

                case "Seats":
                    var seatsHelper = new SearchCommandHelper<int, Car>();
                    var carsBySeats = new Filtered<int, Car>(Catalog.catalog, car => car.Seats); // cars by seats

                    searchOption = "Seats ";
                    searchOption += seatsHelper.Search(carsBySeats, Catalog.catalog, removeHelper.TryRemoveCar);
                    break;

                case "Doors":
                    var doorsHelper = new SearchCommandHelper<int, Car>();
                    var carsByDoors = new Filtered<int, Car>(Catalog.catalog, car => car.Doors); // cars by doors

                    searchOption = "Doors ";
                    searchOption += doorsHelper.Search(carsByDoors, Catalog.catalog, removeHelper.TryRemoveCar);
                    break;

                case "Price":
                    var priceHelper = new SearchCommandHelper<double, Car>();
                    priceHelper.SearchByPrice(Catalog.catalog, car => car.Price, removeHelper.TryRemoveCar);
                    break;
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
                    var typeHelper = new SearchCommandHelper<string, Engine>();
                    var enginesByType = new Filtered<string, Engine>(Catalog.engines, e => e.Type); // engines by type

                    searchOption = "EngineType ";
                    searchOption += typeHelper.Search(enginesByType, Catalog.engines, removeHelper.TryRemoveEngine);
                    break;

                case "EngineID":
                    var idHelper = new SearchCommandHelper<string, Engine>();
                    var enginesByID = new Filtered<string, Engine>(Catalog.engines, e => e.id); // engines by id

                    searchOption = "EngineID ";
                    searchOption += idHelper.Search(enginesByID, Catalog.engines, removeHelper.TryRemoveEngine);
                    break;

                case "Volume":
                    var volHelper = new SearchCommandHelper<double, Engine>();
                    var enginesByVol = new Filtered<double, Engine>(Catalog.engines, e => e.Volume); // engines by volume

                    searchOption = "Volume ";
                    searchOption += volHelper.Search(enginesByVol, Catalog.engines, removeHelper.TryRemoveEngine);
                    break;

                case "Horsepower":
                    var hpHelper = new SearchCommandHelper<int, Engine>();
                    var enginesByHP = new Filtered<int, Engine>(Catalog.engines, e => e.Horsepower); // engines by horsepower

                    searchOption = "Horsepower ";
                    searchOption += hpHelper.Search(enginesByHP, Catalog.engines, removeHelper.TryRemoveEngine);
                    break;

                case "Price":
                    var priceHelper = new SearchCommandHelper<double, Engine>();
                    priceHelper.SearchByPrice(Catalog.engines, e => e.Price, removeHelper.TryRemoveEngine);
                    break;
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
