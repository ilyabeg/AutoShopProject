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
        public int Doors { get; set; }
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
        // car catalog list from json file
        private static List<JsonCar>? jsoncars = JsonSerializer.Deserialize<List<JsonCar>>(File.ReadAllText("car_catalog.json"));

        // engines from json file
        private static List<JsonEngine>? jsonengines = JsonSerializer.Deserialize<List<JsonEngine>>(File.ReadAllText("engines.json"));

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
                    .SetDoors(car.Doors)
                    .SetPrice(car.Price);

                // add car to the list
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

                // add engine to the list
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

        public void ShowCarCatalog()
        {
            Console.WriteLine("===========================================================");
            Console.WriteLine("                     Car Catalog:");
            Console.WriteLine("===========================================================");
            Console.WriteLine();

            foreach (Car car in catalog)
            {
                Console.WriteLine($"\t{"Car Type:",-22}         {car.CarType,15}");
                Console.WriteLine($"\t{"Car Manufacturer:",-22}  {car.Manufacturer,15}");
                Console.WriteLine($"\t{"Car Model:",-22}        {car.Model,15}");
                Console.WriteLine($"\t{"Car Engine ID:",-22}    {car.EngineID,15}");
                Console.WriteLine($"\t{"Car Year:",-22}         {car.Year,15}");
                Console.WriteLine($"\t{"Car Drivetrain:",-22}   {car.Drivetrain,15}");
                Console.WriteLine($"\t{"Car Seat number:",-22}  {car.Seats,15}");
                Console.WriteLine($"\t{"Car Door number:",-22}  {car.Doors,15}");
                Console.WriteLine($"\t{"Car Price:",-22}        {car.Price + "$",15}");
                Console.WriteLine();
            }
        }

        public void ShowEngineCatalog() 
        {
            Console.WriteLine("===========================================================");
            Console.WriteLine("                     Engine Catalog:");
            Console.WriteLine("===========================================================");
            Console.WriteLine();

            foreach (Engine engine in engines)
            {
                Console.WriteLine($"\t{"Engine ID:",-22}         {engine.id,15}");
                Console.WriteLine($"\t{"Engine Type:",-22}       {engine.Type,15}");
                Console.WriteLine($"\t{"Engine Volume:",-22}     {engine.Volume,15}");
                Console.WriteLine($"\t{"Engine Horsepower:",-22} {engine.Horsepower,15}");
                Console.WriteLine();
            }
        }
    }
}
