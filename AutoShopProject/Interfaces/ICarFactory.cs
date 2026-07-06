using System;
using System.Collections.Generic;
using System.Text;
using AutoShopProject.Cars;

namespace AutoShopProject.Interfaces
{
    internal interface ICarFactory
    {
        enum CarType
        {
            Race,
            Sport,
            Muscle
        }
        enum EngineType
        {
            Race,
            Sport,
            Drag
        }
        public Car CreateCar(CarType type);
        public Engine CreateEngine(EngineType type);
    }
}
