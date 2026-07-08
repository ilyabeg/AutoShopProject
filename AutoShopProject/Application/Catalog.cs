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
        public string EngineID { get; set; }
        public int Year { get; set; }
        public string Drivetrain { get; set; }
        public int Seats { get; set; }
        public int doors { get; set; }
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
            catalog = new List<Car>();
            engines = new List<Engine>();
            oosCars = new List<Car>();
            oosEngines = new List<Engine>();

            lock (_lock)
            {
                InitEngines();
                Thread.Sleep(100);
                InitCars();
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
                    .SetEngine(FindEngine(car.EngineID))
                    .SetYear(car.Year)
                    .SetDrivetrain(car.Drivetrain)
                    .SetSeats(car.Seats)
                    .SetDoors(car.doors)
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

        private static Engine FindEngine(string id)
        {
            foreach (Engine e in engines)
                if (e.id.Equals(id))
                    return e;
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

            foreach (Car car in catalog)
                Console.WriteLine(car.ToString());
        }

        public static void ShowEngineCatalog() 
        {
            Console.WriteLine("===========================================================");
            Console.WriteLine("                     Engine Catalog:");
            Console.WriteLine("===========================================================");
            Console.WriteLine();

            foreach (Engine engine in engines)
                Console.WriteLine(engine.ToString());
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
                foreach (Car car in oosCars)
                    Console.WriteLine(car.ToString());
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
                foreach (Engine engine in oosEngines)
                    Console.WriteLine(engine.ToString());
        }

        public static void SaveAllData()
        {
            string json_cars = JsonSerializer.Serialize(catalog);
            string json_engines = JsonSerializer.Serialize(engines);
            File.WriteAllText("car_catalog.json", json_cars);
            File.WriteAllText("engines.json", json_engines);
        }
    }
}
