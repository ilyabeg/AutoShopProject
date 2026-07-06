using AutoShopProject.Interfaces;
using AutoShopProject.Builders;
using AutoShopProject.Resources;
using AutoShopProject.Factories;
using System.Text.Json;

namespace AutoShopProject.Application
{
    internal class Catalog
    {
        // car catalog list from json file
        public static List<Car>? catalog = JsonSerializer.Deserialize<List<Car>>(File.ReadAllText("car_catalog.json"));

        // engines from json file
        public static List<Engine>? engines = JsonSerializer.Deserialize<List<Engine>>(File.ReadAllText("engines.json"));
        
        //        ^
        //        |
        // static keyword to make changeable throughout the whole project

        public void ShowCarCatalog()
        {
            Console.WriteLine("===========================================================");
            Console.WriteLine("                     Car Catalog:");
            Console.WriteLine("===========================================================");
            Console.WriteLine();

            foreach (Car car in catalog)
            {
                Console.WriteLine("\tCar Type:"          + $"\t{car.CarType}.");
                Console.WriteLine("\tCar Manufacturer:"  + $"\t{car.Manufacturer}.");
                Console.WriteLine("\tCar Model:"         + $"\t{car.Model}.");
                Console.WriteLine("\tCar Engine ID:"     + $"\t{car.EngineID}.");
                Console.WriteLine("\tCar Year:"          + $"\t{car.Year}.");
                Console.WriteLine("\tCar Drivetrain:"    + $"\t{car.Drivetrain}.");
                Console.WriteLine("\tCar Seat number:"   + $"\t{car.Seats}.");
                Console.WriteLine("\tCar Door number:"   + $"\t{car.Doors}.");
                Console.WriteLine("\tCar Price:"         + $"\t{car.Price}$.");
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
                Console.WriteLine("\tEngine ID:"          + $"\t{engine.id}");
                Console.WriteLine("\tEngine Type:"        + $"\t{engine.Type}");
                Console.WriteLine("\tEngine Volume:"      + $"\t{engine.Volume}");
                Console.WriteLine("\tEngine Horsepower:"  + $"\t{engine.Horsepower}");
                Console.WriteLine();
            }
        }
    }
}
