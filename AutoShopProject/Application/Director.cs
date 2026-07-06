using AutoShopProject.Builders;
using AutoShopProject.Interfaces;

namespace AutoShopProject.Application
{
    internal class Director
    {
        private ICarFactory _factory;
        private ICarBuilder _cBuilder;
        private IEngineBuilder _eBuilder;

        public Director(ICarFactory factory)
        {
            _factory = factory;
        }

        public Car CreateSportCar()
        { 
            var engine = _factory.CreateEngine();
            _eBuilder = new EngineBuilder(engine);
            

            var product = _factory.CreateCar();

            return product;
        }

        public Car CreateRaceCar()
        {
            var product = _factory.CreateCar();
            var engine = _factory.CreateEngine();

            //

            return product;
        }

        public Car CreateMuscleCar()
        {
            var product = _factory.CreateCar();
            var engine = _factory.CreateEngine();

            //

            return product;
        }
    }
}
