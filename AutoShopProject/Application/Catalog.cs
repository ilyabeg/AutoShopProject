using AutoShopProject.Builders;
using AutoShopProject.Factories;
using AutoShopProject.Interfaces;
using AutoShopProject.Resources;
using System.Collections.Concurrent;
using System.Runtime.ConstrainedExecution;
using System.Text.Json;

namespace AutoShopProject.Application
{
    internal class JsonCar
    {
        public string CarType { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public JsonEngine Engine { get; set; }
        public int Year { get; set; }
        public string Drivetrain { get; set; }
        public int Seats { get; set; }
        public int Doors { get; set; }
        public double Price { get; set; }
        public int InStock { get; set; }
    }

    internal class JsonEngine
    {
        public string id { get; set; }
        public string Type { get; set; }
        public double Volume { get; set; }
        public int Horsepower { get; set; }
        public double Price { get; set; }
        public int InStock { get; set; }
    }

    internal class Catalog
    {
        // Singleton instance
        private static Catalog _instance;
        private static readonly object _lock = new object(); // create lock to stop threads of creating a new instane

        private Catalog() 
        {
            jsoncars = JsonSerializer.Deserialize<List<JsonCar>>(File.ReadAllText("car_catalog.json"));
            jsonengines = JsonSerializer.Deserialize<List<JsonEngine>>(File.ReadAllText("engines.json"));
            jsonOosCars = JsonSerializer.Deserialize<List<JsonCar>>(File.ReadAllText("oos_cars.json"));
            jsonOosEngines = JsonSerializer.Deserialize<List<JsonEngine>>(File.ReadAllText("oos_engines.json"));
            catalog = new List<Car>();
            engines = new List<Engine>();
            oosCars = new List<Car>();
            oosEngines = new List<Engine>();

            lock (_lock)
            {
                InitEngines();
                Thread.Sleep(100);
                InitCars();
                InitOosEngines();
                Thread.Sleep(100);
                InitOosCars();                
            }
        }

        public static Catalog GetInstance()
        {
            if (_instance == null) 
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new Catalog();

                    }
                }
            }                
            return _instance;
        }

        // -----------------------------------------------------

        // car catalog list from json file
        private static List<JsonCar>? jsoncars;

        // engines from json file
        private static List<JsonEngine>? jsonengines;

        // cars oos to json file
        private static List<JsonCar>? jsonOosCars;

        // engines oos to json file
        private static List<JsonEngine>? jsonOosEngines;

        // -----------------------------------------------------

        // actual car catalog
        public static List<Car>? catalog;

        // actual engine catalog
        public static List<Engine>? engines;

        // -----------------------------------------------------

        // cars that are currently out of stock (OOS)
        public static List<Car>? oosCars;

        // engines that are currently out of stock (OOS)
        public static List<Engine>? oosEngines;

        // -----------------------------------------------------

        private static ConcurrentDictionary<string, ICarFactory> _factories = new ConcurrentDictionary<string, ICarFactory>()
        {
            ["SPORT"] = new SportCarFactory(),
            ["RACE"] = new RaceCarFactory(),
            ["MUSCLE"] = new MuscleCarFactory(),
            ["DRAG"] = new MuscleCarFactory()
        };
        
        private static void InitCars()
        {
            CarBuilder cBuilder;
            ICarFactory factory;

            foreach (JsonCar car in jsoncars)
            {
                // get corresponding factory to type of car
                factory = CreateFactory(car.CarType);

                // build right car
                cBuilder = new CarBuilder(factory.CreateCar());

                cBuilder.SetType(car.CarType)
                    .SetManufacturer(car.Manufacturer)
                    .SetModel(car.Model)
                    .SetEngine(FindEngine(car.Engine.id))
                    .SetYear(car.Year)
                    .SetDrivetrain(car.Drivetrain)
                    .SetSeats(car.Seats)
                    .SetDoors(car.Doors)
                    .SetPrice(car.Price)
                    .SetStock(car.InStock);

                catalog.Add(cBuilder.Build());
            }
        }

        private static void InitEngines()
        {
            EngineBuilder eBuilder;
            ICarFactory factory;

            foreach (JsonEngine engine in jsonengines)
            {
                // get corresponding factory to type of engine
                factory = CreateFactory(engine.Type);

                // build right engine
                eBuilder = new EngineBuilder(factory.CreateEngine());

                eBuilder.SetType(engine.Type)
                    .SetID(engine.id)
                    .SetVolume(engine.Volume)
                    .SetHorsepower(engine.Horsepower)
                    .SetPrice(engine.Price)
                    .SetStock(engine.InStock);

                engines.Add(eBuilder.Build());
            }
        }

        private static void InitOosCars()
        {
            CarBuilder cBuilder;
            ICarFactory factory;

            foreach (JsonCar car in jsonOosCars)
            {
                // get corresponding factory to type of car
                factory = CreateFactory(car.CarType);

                // build right car
                cBuilder = new CarBuilder(factory.CreateCar());

                cBuilder.SetType(car.CarType)
                    .SetManufacturer(car.Manufacturer)
                    .SetModel(car.Model)
                    .SetEngine(FindEngine(car.Engine.id))
                    .SetYear(car.Year)
                    .SetDrivetrain(car.Drivetrain)
                    .SetSeats(car.Seats)
                    .SetDoors(car.Doors)
                    .SetPrice(car.Price)
                    .SetStock(car.InStock);

                oosCars.Add(cBuilder.Build());
            }
        }

        private static void InitOosEngines()
        {
            EngineBuilder eBuilder;
            ICarFactory factory;

            foreach (JsonEngine engine in jsonOosEngines)
            {
                // get corresponding factory to type of engine
                factory = CreateFactory(engine.Type);

                // build right engine
                eBuilder = new EngineBuilder(factory.CreateEngine());

                eBuilder.SetType(engine.Type)
                    .SetID(engine.id)
                    .SetVolume(engine.Volume)
                    .SetHorsepower(engine.Horsepower)
                    .SetPrice(engine.Price)
                    .SetStock(engine.InStock);

                oosEngines.Add(eBuilder.Build());
            }
        }

        private static Engine FindEngine(string id)
        {
            foreach (var engine in engines)
            {
                if (engine.id.Equals(id))
                    return engine;
            }
            return null;
        }

        private static ICarFactory CreateFactory(string type)
        {
            if (_factories.ContainsKey(type.ToUpper()))
                return _factories[type.ToUpper()];
            return null; // <- never actualy happens...
        }

        public static void ShowCarCatalog()
        {
            Console.WriteLine("===========================================================");
            Console.WriteLine("                     Car Catalog:");
            Console.WriteLine("===========================================================");
            Console.WriteLine();

            int i = 0;
            foreach (Car car in catalog)
            {
                Console.WriteLine($"\t{i++}:\n");
                Console.WriteLine(car.ToString());
            }
        }

        public static void ShowEngineCatalog() 
        {
            Console.WriteLine("===========================================================");
            Console.WriteLine("                     Engine Catalog:");
            Console.WriteLine("===========================================================");
            Console.WriteLine();

            int i = 0;
            foreach (Engine engine in engines)
            {
                Console.WriteLine($"\t{i++}:\n");
                Console.WriteLine(engine.ToString());
            }
        }

        public static void ShowOOSCatalog()
        {
            Console.WriteLine("===========================================================");
            Console.WriteLine("                  Out Of Stock Catalog:");
            Console.WriteLine("===========================================================");
            Console.WriteLine();

            if (oosCars.Count == 0)
                Console.WriteLine("None.");
            else
            {
                int i = 0;
                foreach (Car car in oosCars)
                {
                    Console.WriteLine($"\t{i++}:\n");
                    Console.WriteLine(car.ToString());
                }
            }
        }

        public static void ShowOOSEngines()
        {
            Console.WriteLine("===========================================================");
            Console.WriteLine("                  Out Of Stock Engines:");
            Console.WriteLine("===========================================================");
            Console.WriteLine();

            if (oosEngines.Count == 0)
                Console.WriteLine("None.");
            else
            {
                int i = 0;
                foreach (Engine engine in oosEngines)
                {
                    Console.WriteLine($"\t{i++}:\n");
                    Console.WriteLine(engine.ToString());
                }
            }
        }

        public static void SaveAllData()
        {
            string json_cars = JsonSerializer.Serialize(catalog);            
            File.WriteAllText("car_catalog.json", json_cars);

            string json_engines = JsonSerializer.Serialize(engines);
            File.WriteAllText("engines.json", json_engines);

            string json_oos_c = JsonSerializer.Serialize(oosCars);
            File.WriteAllText("oos_cars.json", json_oos_c);

            string json_oos_e = JsonSerializer.Serialize(oosEngines);
            File.WriteAllText("oos_engines.json", json_oos_e);
        }
    }
}
