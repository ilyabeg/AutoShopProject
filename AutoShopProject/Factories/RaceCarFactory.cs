using AutoShopProject.Cars;
using AutoShopProject.Engines;
using AutoShopProject.Interfaces;

namespace AutoShopProject.Factories
{
    internal class RaceCarFactory : ICarFactory
    {
        public Car CreateCar() => new RaceCar();
        public Engine CreateEngine() => new RaceEngine();
    }
}
