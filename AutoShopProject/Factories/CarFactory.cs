using AutoShopProject.Cars;
using AutoShopProject.Engines;
using AutoShopProject.Interfaces;
using System;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using System.Text;
using static AutoShopProject.Interfaces.ICarFactory;

namespace AutoShopProject.Factories
{
    internal class CarFactory : ICarFactory
    {
        public Car CreateCar(CarType type)
        {
            Car car;
            switch (type)
            {
                case CarType.Race:
                    car = new RaceCar();
                    break;

                case CarType.Sport:
                    car = new SportCar();
                    break;

                case CarType.Muscle:
                    car = new MuscleCar();
                    break;

                default:
                    throw new Exception("Invalid car Type!");
            }
            return car;
        }

        public Engine CreateEngine(EngineType type)
        {
            Engine engine;
            switch (type)
            {
                case EngineType.Race:
                    engine = new RaceEngine();
                    break;

                case EngineType.Sport:
                    engine = new SportEngine();
                    break;

                case EngineType.Drag:
                    engine = new DragEngine();
                    break;

                default:
                    throw new Exception("Invalid engine Type!");
            }

            return engine;
        }
    }
}
