using AutoShopProject.Cars;
using AutoShopProject.Engines;
using AutoShopProject.Interfaces;

namespace AutoShopProject.Factories
{
    internal class MuscleCarFactory : ICarFactory
    {
        public Car CreateCar() => new MuscleCar();
        public Engine CreateEngine() => new DragEngine();
    }
}
