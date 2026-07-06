using System;
using System.Collections.Generic;
using System.Text;
using AutoShopProject.Cars;

namespace AutoShopProject.Interfaces
{
    internal interface ICarFactory
    {
        public Car CreateCar();
        public Engine CreateEngine();
    }
}
