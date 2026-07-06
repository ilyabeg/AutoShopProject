using AutoShopProject.Builders;
using AutoShopProject.Interfaces;
using AutoShopProject.Resources;

namespace AutoShopProject.Application
{
    internal class Director
    {
        private CarBuilder _cBuilder;
        private EngineBuilder _eBuilder;

        public Director(CarBuilder carBuilder, EngineBuilder engineBuilder)
        {
            _cBuilder = carBuilder;
            _eBuilder = engineBuilder;
        }

        public Car CreateSportCar()
        {
            _cBuilder.SetType("Sport")
                    .SetManufacturer()
                    .SetModel()
                    .SetEngine()
                    .SetYear()
                    .SetDrivetrain()
                    .SetSeats()
                    .SetDoors()
                    .SetPrice();

            return _cBuilder.Build();
        }

        public Engine CreateSportEngine()
        {
            _eBuilder.SetID()
                .SetType()
                .SetVolume()
                .SetHorsepower();

            return _eBuilder.Build();
        }

        public Car CreateRaceCar()
{       {
            _cBuilder.SetType("Race")
                    .SetManufacturer()
                    .SetModel()
                    .SetEngine()
                    .SetYear()
                    .SetDrivetrain()
                    .SetSeats()
                    .SetDoors()
                    .SetPrice();

            return _cBuilder.Build();
        }

        public Engine CreateRaceEngine()
        {
            _eBuilder.SetID()
                .SetType()
                .SetVolume()
                .SetHorsepower();

            return _eBuilder.Build();
        }

        public Car CreateMuscleCar()
        {
            _cBuilder.SetType("Muscle")
                    .SetManufacturer()
                    .SetModel()
                    .SetEngine()
                    .SetYear()
                    .SetDrivetrain()
                    .SetSeats()
                    .SetDoors()
                    .SetPrice();

            return _cBuilder.Build();
        }

        public Engine CreateDragEngine()
        {
            _eBuilder.SetID()
                .SetType()
                .SetVolume()
                .SetHorsepower();

            return _eBuilder.Build();
        }
    }
}
