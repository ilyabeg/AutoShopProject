using System;
using System.Collections.Generic;
using System.Text;

namespace AutoShopProject.Interfaces
{
    internal interface ICarFactory
    {
        public Car CreateCar();
        public Engine CreateEngine();
    }
}
