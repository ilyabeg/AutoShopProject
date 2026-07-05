using AutoShopProject.Interfaces;
using AutoShopProject.Engines;
using AutoShopProject.Cars;
using System;
using System.Collections.Generic;
using System.Text;
using AutoShopProject.Resources;

namespace AutoShopProject.Factories
{
    internal class RaceCarFactory : ICarFactory
    {
        public Car CreateCar() 
        {
            CarBuilder _builder = new CarBuilder(new RaceCar());
            _builder.Init()
                .SetManufacturer("")
                .SetModel("")
                .SetEngine(this.CreateEngine())
                .SetYear(0)
                .SetDrivetrain("")
                .SetSeats(0)
                .SetDoors(0);

            return _builder.Build();
        }

        public Engine CreateEngine()
        {
            return new RaceEngine("V8", 2.0, 800);
        }
    }
}
