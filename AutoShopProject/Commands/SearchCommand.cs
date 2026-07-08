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

        private SearchCommandHelper _helper = new SearchCommandHelper();

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

            if (input == "C")
                CarSearch();
            else
                EngineSearch();            
        }

        private void CarSearch()
        {
            int input = SelectOption(_car_options);

            switch (_car_options[input])
            {
                case "CarType":
                    _helper.SearchCarsString(car => car.CarType);
                    break;

                case "Manufacturers":
                    _helper.SearchCarsString(car => car.Manufacturer);
                    break;

                case "Model":
                    _helper.SearchCarsString(car => car.Model);
                    break;

                case "Drivetrain":
                    _helper.SearchCarsString(car => car.Drivetrain);
                    break;

                case "Year":
                    _helper.SearchCarsInt(car => car.Year);
                    break;

                case "Seats":
                    _helper.SearchCarsInt(car => car.Seats);
                    break;

                case "Doors":
                    _helper.SearchCarsInt(car => car.Doors);
                    break;

                case "Price":
                    _helper.SearchCarsByPrice();
                    break;
            }
            Console.WriteLine("[COMMAND] Search complete.");
        }

        private void EngineSearch()
        {
            int input = SelectOption(_engine_options);

            switch (_engine_options[input])
            {
                case "EngineType":
                    _helper.SearchEngineString(engine => engine.Type);
                    break;

                case "EngineID":
                    _helper.SearchEngineString(engine => engine.id);
                    break;

                case "Volume":
                    _helper.SearchEngineDouble(engine => engine.Volume);
                    break;

                case "Horsepower":
                    _helper.SearchEngineInt(engine => engine.Horsepower);
                    break;

                case "Price":
                    _helper.SearchEnginesByPrice();
                    break;
            }
            Console.WriteLine("[COMMAND] Search complete.");
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
