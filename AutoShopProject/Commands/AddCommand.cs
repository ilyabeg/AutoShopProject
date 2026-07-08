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
        private static readonly object _lock = new object();
        private readonly AddCommandHelper _helper = new AddCommandHelper();

        private string? carType, manufacturer, model, drivetrain, engineType, engineID;
        private int year, seats, doors, horsepower;
        private double cPrice, volume, ePrice;

        private ConcurrentDictionary<string, ICarFactory> _factories = new ConcurrentDictionary<string, ICarFactory>()
        {
            ["SPORT"] = new SportCarFactory(),
            ["RACE"] = new RaceCarFactory(),
            ["MUSCLE"] = new MuscleCarFactory(),
            ["DRAG"] = new MuscleCarFactory() // <- for engines only
        };

        public void Execute()
        {
            // initializing every property...
            carType = _helper.GetCarType();
            manufacturer = _helper.GetManufacturer();
            model = _helper.GetModel();
            year = _helper.GetYear();
            drivetrain = _helper.GetDriveTrain();
            seats = _helper.GetSeats();
            doors = _helper.GetDoors();
            cPrice = _helper.GetPrice();
            engineType = _helper.GetEngineType();
            volume = _helper.GetEngineVolume();
            horsepower = _helper.GetEngineHorsepower();
            ePrice = _helper.GetEnginePrice();
            engineID = _helper.GetEngineID();

            Console.WriteLine("\n[COMMAND] You are about to add a new car with it's engine! Are you sure you want to proceed? Press any key to continue or 'N' to abort >>");
            if (Console.ReadKey().KeyChar == 'n' || Console.ReadKey().KeyChar == 'N') return;
            
            Add();
        }

        private void Add()
        {
            // building the car and engine with builder and factory:
            ICarFactory factory = CreateFactory(carType);
            CarBuilder cBuilder = new CarBuilder(factory.CreateCar());

            factory = CreateFactory(engineType);
            EngineBuilder eBuilder = new EngineBuilder(factory.CreateEngine());

            eBuilder.SetID(engineID)
                .SetType(engineType)
                .SetVolume(volume)
                .SetHorsepower(horsepower)
                .SetPrice(ePrice);
            var eng = eBuilder.Build();

            cBuilder.SetType(carType)
                .SetManufacturer(manufacturer)
                .SetModel(model)
                .SetEngine(eng)
                .SetYear(year)
                .SetDrivetrain(drivetrain)
                .SetSeats(seats)
                .SetDoors(doors)
                .SetPrice(cPrice + eng.Price);
            var car = cBuilder.Build();

            lock (_lock)
            {
                _helper.TryAddCar(car);
                _helper.TryAddEngine(eng);
            }
            Console.WriteLine("[MANAGER] Adding process complete.");
        }

        public ICarFactory CreateFactory(string type)
        {
            if (_factories.ContainsKey(type.ToUpper()))
                return _factories[type.ToUpper()];
            return null;
        }
    }
}
