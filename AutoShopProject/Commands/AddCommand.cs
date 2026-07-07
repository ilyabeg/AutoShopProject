using AutoShopProject.Application;
using AutoShopProject.Builders;
using AutoShopProject.Factories;
using AutoShopProject.Interfaces;
using AutoShopProject.Resources;
using System.Collections.Concurrent;

namespace AutoShopProject.Commands
{
    internal class AddCommand : ICommandUser
    {
        private ConcurrentDictionary<string, ICarFactory> _factories = new ConcurrentDictionary<string, ICarFactory>()
        {
            ["SPORT"] = new SportCarFactory(),
            ["RACE"] = new RaceCarFactory(),
            ["MUSCLE"] = new MuscleCarFactory()
        };

        public void Execute()
        {
            // initializing every property...

            Console.WriteLine("[MANAGER] Step 1: Enter Car Type >>");
            string type = GetCarType();

            Console.WriteLine("[MANAGER] Step 2: Enter Car Manufacturer >>");
            string manufacturer = GetManufacturer();

            Console.WriteLine("[MANAGER] Step 3: Enter Car Model >>");
            string model = GetModel();

            Console.WriteLine("[MANAGER] Step 4: Enter Car Year >>");
            int year = GetYear();

            Console.WriteLine("[MANAGER] Step 5: Enter Car Drivetrain >>");
            string drivetrain = GetDriveTrain();

            Console.WriteLine("[MANAGER] Step 6: Enter Car Seat Number >>");
            int seats = GetSeats();

            Console.WriteLine("[MANAGER] Step 7: Enter Car Door Number >>");
            int doors = GetDoors();

            Console.WriteLine("[MANAGER] Step 8: Enter Car Price >>");
            double price = GetPrice();

            Console.WriteLine("[MANAGER] Step 9: Enter Engine Type >>");
            string engineType = GetEngineType();

            Console.WriteLine("[MANAGER] Step 10: Enter Engine Volume >>");
            double volume = GetEngineVolume();

            Console.WriteLine("[MANAGER] Step 11: Enter Engine Horsepower >>");
            int horsepower = GetEngineHorsepower();

            Console.WriteLine("[MANAGER] Final Step: Enter Engine ID >>");
            string engineID = GetEngineID();

            // ------------------------------------------------------------

            // building the car and engine with builder and factory:
            ICarFactory factory = CreateFactory(type);

            CarBuilder cBuilder = new CarBuilder(factory.CreateCar());
            EngineBuilder eBuilder = new EngineBuilder(factory.CreateEngine());

            cBuilder.SetType(type)
                .SetManufacturer(manufacturer)
                .SetModel(model)
                .SetEngine(engineID)
                .SetYear(year)
                .SetDrivetrain(drivetrain)
                .SetSeats(seats)
                .SetDoors(doors)
                .SetPrice(price);

            eBuilder.SetID(engineID)
                .SetType(engineType)
                .SetVolume(volume)
                .SetHorsepower(horsepower);

            TryAddCar(cBuilder.Build());
            TryAddEngine(eBuilder.Build());

            Console.WriteLine("[MANAGER] Adding process complete.");
        }

        //---------------------------------------------------------------------------------------------------

        // Helper methods...

        private string GetCarType()
        {
            string type = Console.ReadLine().Trim();

            if (!ValidType(type))
            {
                do 
                {
                    Console.WriteLine("[MANAGER] Invalid Type, please re-enter (Race/Sport/Muscle) >>");
                    type = Console.ReadLine().Trim();
                }
                while (!ValidType(type));
            }

            return type;
        }

        private bool ValidType(string type)
        {
            return type.Equals("Race", StringComparison.OrdinalIgnoreCase) || 
                type.Equals("Sport", StringComparison.OrdinalIgnoreCase) || 
                type.Equals("Muscle", StringComparison.OrdinalIgnoreCase);
        }

        private string GetManufacturer()
        {
            string input = Console.ReadLine().Trim();

            if (input == null || input.IsWhiteSpace())
            {
                do
                {
                    Console.WriteLine("[COMMAND] Invalid input, please re-enter >>");
                    input = Console.ReadLine().Trim();
                }
                while (input == null || input.IsWhiteSpace());
            }

            return input;
        }

        private string GetModel()
        {
            string input = Console.ReadLine().Trim();

            if (input == null || input.IsWhiteSpace())
            {
                do
                {
                    Console.WriteLine("[COMMAND] Invalid input, please re-enter >>");
                    input = Console.ReadLine().Trim();
                }
                while (input == null || input.IsWhiteSpace());
            }

            return input;
        }

        private int GetYear()
        {
            int year = 0;

            try
            {
                year = int.Parse(Console.ReadLine().Trim());
            }
            catch { }

            if (year > DateTime.Now.Year || year < 1930)
            {
                do
                {
                    Console.WriteLine("[MANAGER] Invalid year input, please re-enter >>");
                    try
                    {
                        year = int.Parse(Console.ReadLine().Trim());
                    }
                    catch { }
                }
                while (year > DateTime.Now.Year || year < 1930);
            }

            return year;
        }

        private string GetDriveTrain()
        { 
            string drivetrain = Console.ReadLine().Trim();

            if (!ValidDrivetrain(drivetrain))
            {
                do
                {
                    Console.WriteLine("[MANAGER] Invalid Drivetrain, please re-enter (RWD/FWD/AWD) >>");
                    drivetrain = Console.ReadLine().Trim();
                }
                while (!ValidDrivetrain(drivetrain));
            }

            return drivetrain;
        }

        private bool ValidDrivetrain(string drive)
        {
            return drive.Equals("RWD", StringComparison.OrdinalIgnoreCase) ||
                drive.Equals("FWD", StringComparison.OrdinalIgnoreCase) ||
                drive.Equals("AWD", StringComparison.OrdinalIgnoreCase);
        }

        private int GetSeats()
        {
            int seats = 0;

            try
            {
                seats = int.Parse(Console.ReadLine().Trim());
            }
            catch { }

            if (seats < 2 || seats > 5)
            {
                do
                {
                    Console.WriteLine("[MANAGER] Invalid Seat number, please re-enter >>");
                    try
                    {
                        seats = int.Parse(Console.ReadLine().Trim());
                    }
                    catch { }
                }
                while (seats < 2 || seats > 5);
            }

            return seats;
        }

        private int GetDoors()
        {
            int doors = 0;

            try
            {
                doors = int.Parse(Console.ReadLine().Trim());
            }
            catch { }

            if (doors != 2 && doors != 4)
            {
                do
                {
                    Console.WriteLine("[MANAGER] Invalid Doors number, please re-enter >>");
                    try
                    {
                        doors = int.Parse(Console.ReadLine().Trim());
                    }
                    catch { }
                }
                while (doors != 2 && doors != 4);
            }

            return doors;
        }

        private double GetPrice()
        {
            double price = -1.0;

            try
            {
                price = double.Parse(Console.ReadLine().Trim());
            }
            catch { }

            if (price < 0.0 || price > Double.MaxValue)
            {
                do
                {
                    Console.WriteLine("[MANAGER] Invalid Price, please re-enter >>");
                    try
                    {
                        price = double.Parse(Console.ReadLine().Trim());
                    }
                    catch { }
                }
                while (price < 0.0 || price > Double.MaxValue);
            }

            return price;
        }

        private string GetEngineType()
        {
            string input = Console.ReadLine().Trim();

            if (input == null || input.IsWhiteSpace())
            {
                do
                {
                    Console.WriteLine("[COMMAND] Invalid input, please re-enter >>");
                    input = Console.ReadLine().Trim();
                }
                while (input == null || input.IsWhiteSpace());
            }

            return input;
        }

        private double GetEngineVolume()
        {
            double vol = -1.0;

            try
            {
                vol = double.Parse(Console.ReadLine().Trim());
            }
            catch { }

            if (vol < 1.0 || vol > 10.0)
            {
                do
                {
                    Console.WriteLine("[MANAGER] Invalid Engine Volume, please re-enter >>");
                    try
                    {
                        vol = double.Parse(Console.ReadLine().Trim());
                    }
                    catch { }
                }
                while (vol < 1.0 || vol > 10.0);
            }

            return vol;
        }

        private int GetEngineHorsepower()
        {
            int horsepower = 0;

            try
            {
                horsepower = int.Parse(Console.ReadLine().Trim());
            }
            catch { }

            if (horsepower < 100 || horsepower > 1850)
            {
                do
                {
                    Console.WriteLine("[MANAGER] Invalid Seat number, please re-enter >>");
                    try
                    {
                        horsepower = int.Parse(Console.ReadLine().Trim());
                    }
                    catch { }
                }
                while (horsepower < 100 || horsepower > 1850);
            }

            return horsepower;
        }

        private string GetEngineID()
        {
            string input = Console.ReadLine().Trim();

            if (input == null || input.IsWhiteSpace())
            {
                do
                {
                    Console.WriteLine("[COMMAND] Invalid input, please re-enter >>");
                    input = Console.ReadLine().Trim();
                }
                while (input == null || input.IsWhiteSpace());
            }

            return input;
        }

        private ICarFactory CreateFactory(string type)
        {
            if (_factories.ContainsKey(type.ToUpper()))
                return _factories[type.ToUpper()];
            return null;
        }

        private void TryAddCar(Car newCar)
        {
            foreach (Car car in Catalog.catalog)
            {
                if (car.Manufacturer.Equals(newCar.Manufacturer, StringComparison.OrdinalIgnoreCase) &&
                    car.Model.Equals(newCar.Model, StringComparison.OrdinalIgnoreCase) &&
                    car.EngineID.Equals(newCar.EngineID, StringComparison.OrdinalIgnoreCase) &&
                    car.Year == newCar.Year)
                {
                    Console.WriteLine("[MANAGER] Car not added - it already exits in the car catalog.");
                    return;
                }
            }
            Catalog.catalog.Add(newCar);
            Console.WriteLine("[MANAGER] Car Added Successfuly.");
        }

        private void TryAddEngine(Engine newEngine)
        {
            foreach (Engine engine in Catalog.engines)
            {
                if (engine.id.Equals(newEngine.id, StringComparison.OrdinalIgnoreCase) &&
                    engine.Type.Equals(newEngine.Type, StringComparison.OrdinalIgnoreCase) &&
                    engine.Volume == newEngine.Volume)
                {
                    Console.WriteLine("[MANAGER] Engine not added - it already exits in the engine catalog.");
                    return;
                }
            }
            Catalog.engines.Add(newEngine);
            Console.WriteLine("[MANAGER] Engine Added Successfuly.");
        }
    }
}
