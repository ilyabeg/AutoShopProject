using AutoShopProject.Cars;
using AutoShopProject.Engines;
using AutoShopProject.Interfaces;

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
