using AutoShopProject.Interfaces;
using AutoShopProject.Builders;
using AutoShopProject.Resources;
using AutoShopProject.Factories;
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

        private Catalog() { } // private constructor

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

        //        ^
        //        |
        // static keyword to make changeable throughout the whole project

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
            switch (type.ToUpper())
            {
                case "RACE":
                    return new RaceCarFactory();

                case "SPORT":
                    return new SportCarFactory();
                               // DRAG -> only for engines
                case "MUSCLE" or "DRAG":
                    return new MuscleCarFactory();
            }
            return null; // <- never actualy happens...
        }

        public static void ShowCarCatalog()
        {
            Console.WriteLine("===========================================================");
            Console.WriteLine("                     Car Catalog:");
            Console.WriteLine("===========================================================");
            Console.WriteLine();

            foreach (Car car in catalog)
            {
                Console.WriteLine($"\t{"Car Type:",-20} {car.CarType}");
                Console.WriteLine($"\t{"Car Manufacturer:",-20} {car.Manufacturer}");
                Console.WriteLine($"\t{"Car Model:",-20} {car.Model}");
                Console.WriteLine($"\t{"Car Engine ID:",-20} {car.EngineID}");
                Console.WriteLine($"\t{"Car Year:",-20} {car.Year}");
                Console.WriteLine($"\t{"Car Drivetrain:",-20} {car.Drivetrain}");
                Console.WriteLine($"\t{"Car Seat number:",-20} {car.Seats}");
                Console.WriteLine($"\t{"Car Door number:",-20} {car.Doors}");
                Console.WriteLine($"\t{"Car Price:",-20} {car.Price + "$"}");
                Console.WriteLine();
            }
        }

        public static void ShowEngineCatalog() 
        {
            Console.WriteLine("===========================================================");
            Console.WriteLine("                     Engine Catalog:");
            Console.WriteLine("===========================================================");
            Console.WriteLine();

            foreach (Engine engine in engines)
            {
                Console.WriteLine($"\t{"Engine ID:",-20} {engine.id}");
                Console.WriteLine($"\t{"Engine Type:",-20} {engine.Type}");
                Console.WriteLine($"\t{"Engine Volume:",-20} {engine.Volume}");
                Console.WriteLine($"\t{"Engine Horsepower:",-20} {engine.Horsepower}");
                Console.WriteLine();
            }
        }
    }
}
