using AutoShopProject.Application;
using AutoShopProject.Factories;
using AutoShopProject.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace AutoShopProject
{
    internal class AddCommandHelper
    {
        public string GetCarType()
        {
            var carType = new Dictionary<int, string>()
            {
                [1] = "Race",
                [2] = "Sport",
                [3] = "Muscle"
            };

            int type = 0;
            while (!carType.ContainsKey(type))
            {
                Console.WriteLine("[MANAGER] Step 1: Enter Car Type (1-Race / 2-Sport / 3-Muscle) >>");
                try
                {
                    type = int.Parse(Console.ReadLine().Trim());
                }
                catch { }
            }
            return carType[type];
        }

        public string GetManufacturer()
        {
            string input = "";
            while (input == null || input.IsWhiteSpace())
            {
                Console.WriteLine("[MANAGER] Step 2: Enter Car Manufacturer >>");
                input = Console.ReadLine().Trim();
            }
            return input;
        }

        public string GetModel()
        {
            string input = "";
            while (input == null || input.IsWhiteSpace())
            {
                Console.WriteLine("[MANAGER] Step 3: Enter Car Model >>");
                input = Console.ReadLine().Trim();
            }
            return input;
        }

        public int GetYear()
        {
            int year = 0;
            while (year > DateTime.Now.Year || year < 1900)
            {
                Console.WriteLine($"[MANAGER] Step 4: Enter Car Year (1990 - {DateTime.Now.Year}) >>");
                try
                {
                    year = int.Parse(Console.ReadLine().Trim());
                }
                catch { }
            }
            return year;
        }

        public string GetDriveTrain()
        {
            var drv = new Dictionary<int, string>()
            {
                [1] = "RWD",
                [2] = "FWD",
                [3] = "AWD"
            };

            int drivetrain = 0;
            while (!drv.ContainsKey(drivetrain))
            {
                Console.WriteLine("[MANAGER] Step 5: Enter Car Drivetrain (1-RWD / 2-FWD / 3-AWD) >>");
                try
                {
                    drivetrain = int.Parse(Console.ReadLine().Trim());
                }
                catch { }
            }
            return drv[drivetrain];
        }

        public int GetSeats()
        {
            int seats = 0;
            while (seats != 2 && seats != 4)
            {
                Console.WriteLine("[MANAGER] Step 6: Enter Car Seat Number (2 or 4) >>");
                try
                {
                    seats = int.Parse(Console.ReadLine().Trim());
                }
                catch { }
            }
            return seats;
        }

        public int GetDoors()
        {
            int doors = 0;
            while (doors != 2 && doors != 4)
            {
                Console.WriteLine("[MANAGER] Step 7: Enter Car Door Number (2 or 4) >>");
                try
                {
                    doors = int.Parse(Console.ReadLine().Trim());
                }
                catch { }
            }
            return doors;
        }

        public double GetPrice()
        {
            double price = -1.0;
            while (price < 0.0 || price > Double.MaxValue)
            {
                Console.WriteLine("[MANAGER] Step 8: Enter Car Price >>");
                try
                {
                    price = double.Parse(Console.ReadLine().Trim());
                }
                catch { }

                if (price > -1.0 && price < Double.MaxValue)
                {
                    Console.WriteLine($"[MANAGER] Are you sure you'd like to save this the price - ${price}? (Y/N) >>");
                    char ans = Console.ReadKey().KeyChar;

                    while (ans != 'Y' && ans != 'y' && ans != 'N' && ans != 'n')
                    {
                        Console.WriteLine("[MANAGER] Please enter valid answear >>");
                        ans = Console.ReadKey().KeyChar;
                    }

                    if (ans == 'N' || ans == 'n') price = -1.0;
                }
            }
            return price;
        }

        public string GetEngineType()
        {
            var engineTypes = new Dictionary<int, string>()
            {
                [1] = "Race",
                [2] = "Sport",
                [3] = "Drag"
            };

            int input = 0;
            while (!engineTypes.ContainsKey(input))
            {
                Console.WriteLine("[MANAGER] Step 9: Enter Engine Type (1-Race / 2-Sport / 3-Drag) >>");
                try
                {
                    input = int.Parse(Console.ReadLine().Trim());
                }
                catch { }
            }
            return engineTypes[input];
        }

        public double GetEngineVolume()
        {
            double vol = -1.0;
            while (vol < 1.0 || vol > 10.0)
            {
                Console.WriteLine("[MANAGER] Step 10: Enter Engine Volume >>");
                try
                {
                    vol = double.Parse(Console.ReadLine().Trim());
                }
                catch { }
            }
            return vol;
        }

        public int GetEngineHorsepower()
        {
            int horsepower = 0;
            while (horsepower < 100 || horsepower > 1500)
            {
                Console.WriteLine("[MANAGER] Step 11: Enter Engine Horsepower >>");
                try
                {
                    horsepower = int.Parse(Console.ReadLine().Trim());
                }
                catch { }
            }
            return horsepower;
        }

        public double GetEnginePrice()
        {
            double price = -1.0;
            while (price < 0.0 || price > Double.MaxValue)
            {
                Console.WriteLine("[MANAGER] Step 12: Enter Engine Price >>");
                try
                {
                    price = double.Parse(Console.ReadLine().Trim());
                }
                catch { }

                if (price > -1.0 && price < Double.MaxValue)
                {
                    Console.WriteLine($"[MANAGER] Are you sure you'd like to save this the price - ${price}? (Y/N) >>");
                    char ans = Console.ReadKey().KeyChar;

                    while (ans != 'Y' && ans != 'y' && ans != 'N' && ans != 'n')
                    {
                        Console.WriteLine("[MANAGER] Please enter valid answear >>");
                        ans = Console.ReadKey().KeyChar;
                    }

                    if (ans == 'N' || ans == 'n') price = -1.0;
                }
            }
            return price;
        }

        public string GetEngineID()
        {
            string input = "";
            while (input == null || input.IsWhiteSpace())
            {
                Console.WriteLine("[MANAGER] Final Step: Enter Engine ID >>");
                input = Console.ReadLine().Trim();
            }
            return input;
        }

        public void TryAddCar(Car newCar)
        {
            foreach (Car car in Catalog.catalog)
            {
                if (car.Equals(newCar))
                {
                    car.InStock++;
                    return;
                }
            }
            Catalog.catalog.Add(newCar);
            Console.WriteLine("[MANAGER] Car Added Successfuly.");
        }

        public void TryAddEngine(Engine newEngine)
        {
            foreach (Engine engine in Catalog.engines)
            {
                if (engine.Equals(newEngine))
                {
                    engine.InStock++;
                    return;
                }
            }
            Catalog.engines.Add(newEngine);
            Console.WriteLine("[MANAGER] Engine Added Successfuly.");
        }
    }
}
