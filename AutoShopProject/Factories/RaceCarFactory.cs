using AutoShopProject.Cars;
using AutoShopProject.Engines;
using AutoShopProject.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using static AutoShopProject.Interfaces.ICarFactory;

namespace AutoShopProject.Factories
{
    internal class RaceCarFactory : ICarFactory
    {
        public Car CreateCar()
        {
            return new RaceCar();
        }

        public Engine CreateEngine()
        {
            return new RaceEngine();
        }
    }
}
