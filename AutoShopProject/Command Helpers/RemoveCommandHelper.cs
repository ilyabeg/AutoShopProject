using AutoShopProject.Application;
using AutoShopProject.Filters;
using System;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using System.Text;

namespace AutoShopProject.Command_Helpers
{
    internal class RemoveCommandHelper
    {
        private static readonly object _lock = new object();

        public void RemoveCar()
        {
            Car wantedCar = FindWantedCar();

            if (wantedCar == null)
            {
                Console.WriteLine("[COMMAND] The car was not found. Exiting command...");
                return;
            }

            lock (_lock)
            {
                TryRemoveCar(wantedCar);
            }
            Console.WriteLine("[COMMAND] Car successfuly picked out!");
        }

        public void RemoveEngine()
        {
            Engine wantedEngine = FindWantedEngine();

            if (wantedEngine == null)
            {
                Console.WriteLine("[COMMAND] The Engine was not found. Exiting command...");
                return;
            }

            lock (_lock)
            {
                TryRemoveEngine(wantedEngine);
            }
            Console.WriteLine("[COMMAND] Engine successfuly picked out!");
        }

        private Car FindWantedCar()
        {
            string manufacturer = GetManufacturer();
            string model = GetModel();
            int year = GetYear();
            string engineID = GetEngineID();
            return PullCar(manufacturer, model, year, engineID);
        }

        private Engine FindWantedEngine()
        {
            string engineType = GetEngineType();
            double volume = GetEngineVolume();
            string engineID = GetEngineID();
            return PullEngine(engineType, volume, engineID);
        }

        private Car PullCar(string manufacturer, string model, int year, string id)
        {
            foreach (Car car in Catalog.catalog)
            {
                if (car.Year == year && car.Model.Equals(model) && car.Manufacturer.Equals(manufacturer) &&
                    car.Engine.id.Equals(id))
                    return car;
            }
            return null;
        }

        private Engine PullEngine(string type, double vol, string id)
        {
            foreach (Engine engine in Catalog.engines)            
                if (engine.Type.Equals(type) && engine.Volume == vol && engine.id.Equals(id))
                    return engine;
            return null;
        }

        private void TryRemoveCar(Car car)
        {
            if (car.InStock == 1)
            {
                car.InStock--;
                Catalog.oosCars.Add(car);
                Catalog.catalog.Remove(car);
            }
            else
            {
                car.InStock--;
            }
        }

        private void TryRemoveEngine(Engine engine)
        {
            if (engine.InStock == 1)
            {
                engine.InStock--;
                Catalog.oosEngines.Add(engine);
                Catalog.engines.Remove(engine);
            }
            else
            {
                engine.InStock--;
            }
        }

        private string GetManufacturer()
        {
            FilteredCars<string> filteredCars = new FilteredCars<string>(car => car.Manufacturer);

            var manufacturers = filteredCars._cars.Select(car => car.Key).ToList();
            manufacturers.Sort();

            Console.WriteLine();
            int i = 0;
            foreach (var manufacturer in manufacturers)
                Console.WriteLine($"\t{i++} - {manufacturer}");

            int input = -1;
            while (input < 0 || input > manufacturers.Count - 1)
            {
                Console.WriteLine("\n[COMMAND] Please enter the number of a Manufacturer >>");                
                try
                {
                    input = int.Parse(Console.ReadLine().Trim());
                } catch { }
            }                                   
            return manufacturers.ElementAt(input);
        }

        private string GetModel()
        {
            FilteredCars<string> filteredCars = new FilteredCars<string>(car => car.Model);

            var models = filteredCars._cars.Select(car => car.Key).ToList();
            models.Sort();

            Console.WriteLine();
            int i = 0;
            foreach (var model in models)
                Console.WriteLine($"\t{i++} - {model}");

            int input = -1;
            while (input < 0 || input > models.Count - 1)
            {
                Console.WriteLine("\n[COMMAND] Please enter the number of a Model >>");                
                try
                {
                    input = int.Parse(Console.ReadLine().Trim());
                }
                catch { }
            }
            return models.ElementAt(input);
        }

        private int GetYear()
        {
            FilteredCars<int> filteredCars = new FilteredCars<int>(car => car.Year);

            var years = filteredCars._cars.Select(car => car.Key).ToList();
            years.Sort();

            Console.WriteLine();
            int i = 0;
            foreach (var year in years)
                Console.WriteLine($"\t{i++} - {year}");

            int input = -1;
            while (input < 0 || input > years.Count - 1)
            {
                Console.WriteLine("\n[COMMAND] Please enter the number of a Year >>");                
                try
                {
                    input = int.Parse(Console.ReadLine().Trim());
                }
                catch { }
            }
            return years.ElementAt(input);
        }

        private string GetEngineID()
        {
            FilteredEngines<string> filteredEngines = new FilteredEngines<string>(engine => engine.id);

            var ids = filteredEngines._engines.Select(eng => eng.Key).ToList();
            ids.Sort();

            Console.WriteLine();
            int i = 0;
            foreach (var id in ids)
                Console.WriteLine($"\t{i++} - {id}");

            int input = -1;
            while (input < 0 || input > ids.Count - 1)
            {
                Console.WriteLine("\n[COMMAND] Please enter the number of an Engine ID >>");
                try
                {
                    input = int.Parse(Console.ReadLine().Trim());
                }
                catch { }
            }
            return ids.ElementAt(input);
        }              

        private string GetEngineType()
        {
            FilteredEngines<string> filteredEngines = new FilteredEngines<string>(engine => engine.Type);

            var types = filteredEngines._engines.Select(eng => eng.Key).ToList();
            types.Sort();

            Console.WriteLine();
            int i = 0;
            foreach (var type in types)
                Console.WriteLine($"\t{i++} - {type}");

            int input = -1;
            while (input < 0 || input > types.Count - 1)
            {
                Console.WriteLine("\n[COMMAND] Please enter the number of an Engine Type >>");
                try
                {
                    input = int.Parse(Console.ReadLine().Trim());
                }
                catch { }
            }
            return types.ElementAt(input);
        }

        private double GetEngineVolume()
        {
            FilteredEngines<double> filteredEngines = new FilteredEngines<double>(engine => engine.Volume);

            var volumes = filteredEngines._engines.Select(eng => eng.Key).ToList();
            volumes.Sort();

            Console.WriteLine();
            int i = 0;
            foreach (var volume in volumes)
                Console.WriteLine($"\t{i++} - {volume}L");

            int input = -1;
            while (input < 0 || input > volumes.Count - 1)
            {
                Console.WriteLine("\n[COMMAND] Please enter the number of an Engine Volume >>");
                try
                {
                    input = int.Parse(Console.ReadLine().Trim());
                }
                catch { }
            }
            return volumes.ElementAt(input);
        }       
    }
}
