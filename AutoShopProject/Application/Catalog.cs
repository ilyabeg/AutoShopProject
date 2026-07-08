using AutoShopProject.Interfaces;
using AutoShopProject.Builders;
using AutoShopProject.Resources;
using AutoShopProject.Factories;
using System.Text.Json;
using System.Collections.Concurrent;

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
    }

    internal class JsonEngine
    {
        public string id { get; set; }
        public string Type { get; set; }
        public double Volume { get; set; }
        public int Horsepower { get; set; }
    }

    internal class Catalog
    {
        // Singleton instance
        private static Catalog _instance;
        private static readonly object _lock = new object(); // create lock to stop threads of creating a new instane

        private Catalog() 
        { }

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
        private readonly static List<JsonCar>? jsoncars = JsonSerializer.Deserialize<List<JsonCar>>(File.ReadAllText("car_catalog.json"));

        // engines from json file
        private readonly static List<JsonEngine>? jsonengines = JsonSerializer.Deserialize<List<JsonEngine>>(File.ReadAllText("engines.json"));

        // -----------------------------------------------------

        // actual car catalog
        public static List<Car>? catalog = new List<Car>();

        // actual engine catalog
        public static List<Engine>? engines = new List<Engine>();

        // -----------------------------------------------------

        private static ConcurrentDictionary<string, ICarFactory> _factories = new ConcurrentDictionary<string, ICarFactory>()
        {
            ["SPORT"] = new SportCarFactory(),
            ["RACE"] = new RaceCarFactory(),
            ["MUSCLE"] = new MuscleCarFactory(),
            ["DRAG"] = new MuscleCarFactory()
        };

        public static void InitCatalog()
        {
            InitCars();
            InitEngines();
        }
        
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
                    .SetEngine(car.EngineID)
                    .SetYear(car.Year)
                    .SetDrivetrain(car.Drivetrain)
                    .SetSeats(car.Seats)
                    .SetDoors(car.doors)
                    .SetPrice(car.Price);

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
                    .SetHorsepower(engine.Horsepower);

                engines.Add(eBuilder.Build());
            }
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
    }
}
