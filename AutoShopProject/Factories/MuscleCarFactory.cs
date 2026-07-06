using AutoShopProject.Cars;
using AutoShopProject.Engines;
using AutoShopProject.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using static AutoShopProject.Interfaces.ICarFactory;

namespace AutoShopProject.Factories
{
    internal class MuscleCarFactory : ICarFactory
    {
        public Car CreateCar()
        {
            return new MuscleCar();
        }

        public Engine CreateEngine()
        {
            return new DragEngine();
        }
    }
}
