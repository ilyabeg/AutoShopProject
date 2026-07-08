using AutoShopProject.Filters;
using AutoShopProject.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

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

            if (input == "C")
                CarSearch();
            else
                EngineSearch();            
        }

        private void CarSearch()
        {
            Console.WriteLine();
            for (int i = 0; i < _car_options.Count; i++)
            {
                Console.WriteLine($"\t{i} - {_car_options[i]}");
            }

            int input = -1;
            while (!_car_options.ContainsKey(input))
            {
                Console.WriteLine("[COMMAND] What would you like to search for?");
                try
                {
                    input = int.Parse(Console.ReadLine());
                }
                catch { }
            }

            FilteredCars<string> filteredCars1;
            FilteredCars<int> filteredCars2;

            switch (_car_options[input])
            {
                case "CarType":
                    filteredCars1 = new FilteredCars<string>(car => car.CarType);
                    break;

                case "Manufacturers":
                    filteredCars1 = new FilteredCars<string>(car => car.Manufacturer);
                    break;

                case "Model":
                    filteredCars1 = new FilteredCars<string>(car => car.Model);
                    break;

                case "Drivetrain":
                    filteredCars1 = new FilteredCars<string>(car => car.Drivetrain);
                    break;

                case "Year":
                    filteredCars2 = new FilteredCars<int>(car => car.Year);
                    break;

                case "Seats":
                    filteredCars2 = new FilteredCars<int>(car => car.Seats);
                    break;

                case "Doors":
                    filteredCars2 = new FilteredCars<int>(car => car.Doors);
                    break;

                case "Price":

                    break;
            }
        }

        private void EngineSearch()
        {
            Console.WriteLine();
            for (int i = 0; i < _engine_options.Count; i++)
            {
                Console.WriteLine($"\t{i} - {_engine_options[i]}");
            }

            int input = -1;
            while (!_engine_options.ContainsKey(input))
            {
                Console.WriteLine("[COMMAND] What would you like to search for?");
                try
                {
                    input = int.Parse(Console.ReadLine());
                }
                catch { }
            }

            FilteredEngines<string> filteredEngines1;
            FilteredEngines<double> filteredEngines2;
            FilteredEngines<int> filteredEngines3;

            switch (_car_options[input])
            {
                case "EngineType":
                    filteredEngines1 = new FilteredEngines<string>(engine => engine.Type);
                    break;

                case "EngineID":
                    filteredEngines1 = new FilteredEngines<string>(engine => engine.id);
                    break;

                case "Volume":
                    filteredEngines2 = new FilteredEngines<double>(engine => engine.Volume);
                    break;

                case "Horsepower":
                    filteredEngines3 = new FilteredEngines<int>(engine => engine.Horsepower);
                    break;

                case "Price":

                    break;
            }
        }

    }
}
