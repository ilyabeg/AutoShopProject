using AutoShopProject.Cars;
using AutoShopProject.Engines;
using AutoShopProject.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using static AutoShopProject.Interfaces.ICarFactory;

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
