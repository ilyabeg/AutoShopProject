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
            return new RaceCar();
        }

        public Engine CreateEngine()
        {
            return new RaceEngine();
        }
    }
}
