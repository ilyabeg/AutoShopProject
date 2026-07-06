using AutoShopProject.Cars;
using AutoShopProject.Engines;
using AutoShopProject.Interfaces;

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
