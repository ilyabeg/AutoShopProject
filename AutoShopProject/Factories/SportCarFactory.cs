using AutoShopProject.Interfaces;
using AutoShopProject.Engines;
using AutoShopProject.Cars;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoShopProject.Factories
{
    internal class SportCarFactory : ICarFactory
    {
        public Car CreateCar()
        {
            return new SportCar();
        }

        public Engine CreateEngine()
        {
            return new SportEngine();
        }
    }
}
